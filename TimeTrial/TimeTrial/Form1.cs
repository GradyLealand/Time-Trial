using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TimeTrial
{

    public partial class gameForm : Form
    {
        //player object
        private Player player;
        //Goal object
        private Goal goal;
        //hashset of platform objects
        private HashSet<Platform> platforms = new HashSet<Platform>();
        // Number of frames persecond - 1 because of zero index
        private int perSecond = 41;
        //used by timmer tick to tell if a second has passed
        private int timerCount;
        //Time remaining before game over
        private int timeLeft = 30;
        //Number of goals colected by game over
        private int finalPoints;
        //Game has finished
        private bool gameOver = false; 

        /// <summary>
        /// Constructor for gameForm
        /// </summary>
        public gameForm()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Load the gameForm
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void gameForm_Load(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Maximized;

            //initialize player & goal
            player = new Player(this.DisplayRectangle);
            goal = new Goal(this.DisplayRectangle);

            //initialize platforms
            platforms.Add(new Platform(this.DisplayRectangle, 1, 1));
            platforms.Add(new Platform(this.DisplayRectangle, 2, 2));
            platforms.Add(new Platform(this.DisplayRectangle, 3, 1));

            timerCount = 0;
        }

        /// <summary>
        /// Draw all elemetns in the gameForm
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void gameForm_Paint(object sender, PaintEventArgs e)
        {
            foreach(Platform plat in platforms)
            {
                plat.Draw(e.Graphics);
            }
            
            if(gameOver)
            {
                DisplayGameStats(e.Graphics);
            }
            else
            {
                player.Draw(e.Graphics);
                goal.Draw(e.Graphics);
                DisplayCurrentPoints(e.Graphics);
                DisplayTimeLeft(e.Graphics);
            }
        }

        /// <summary>
        /// Draw the players score
        /// </summary>
        /// <param name="graphics">gameForms graphics property</param>
        private void DisplayCurrentPoints(Graphics graphics)
        {
            string message = String.Format("Points: {0}", player.points);

            Font font = new Font(FontFamily.GenericSansSerif, 20);

            graphics.DrawString(message, font, Brushes.White, 20, 45);
        }

        /// <summary>
        /// Draw the time clock
        /// </summary>
        /// <param name="graphics">gameForms graphics property</param>
        private void DisplayTimeLeft(Graphics graphics)
        {
            string message = String.Format("Time Reamaining: {0}", this.timeLeft);

            Font font = new Font(FontFamily.GenericSansSerif, 20);

            graphics.DrawString(message, font, Brushes.White, 20, 20);
        }

        /// <summary>
        /// Draw the end game stats
        /// </summary>
        /// <param name="graphics">gameForms graphics property</param>
        private void DisplayGameStats(Graphics graphics)
        {
            string message = String.Format("Finished with {0} Point!!! \nPress space to restart game.", this.finalPoints);

            Font font = new Font(FontFamily.GenericSansSerif, 30);

            graphics.DrawString(message, font, Brushes.DeepPink, (int)(this.DisplayRectangle.Width / 2) - 250 , (int)(this.DisplayRectangle.Height / 2));
        }

        /// <summary>
        /// Listen for a key stroke
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e">key code</param>
        private void gameForm_KeyDown(object sender, KeyEventArgs e)
        {
            switch(e.KeyCode)
            {
                case Keys.Left:
                    {
                        if (animationTimer.Enabled)
                        {
                            player.Move(Player.Direction.Left);
                        }
                        break;
                    }
                case Keys.Right:
                    {
                        if (animationTimer.Enabled)
                        {
                            player.Move(Player.Direction.Right);
                        }
                        break;
                    }
                case Keys.Up:
                    {
                        if (animationTimer.Enabled)
                        {
                            player.Move(Player.Direction.Up);
                        }
                        break;
                    }
                case Keys.Space:
                    {
                        if(gameOver)
                        {
                            //reset player and goal
                            player = new Player(this.DisplayRectangle);
                            goal = new Goal(this.DisplayRectangle);
                            gameOver = false;

                            animationTimer.Start();
                        }
                        if (!animationTimer.Enabled)
                        {
                            animationTimer.Start();
                        }
                        else
                        {
                            animationTimer.Stop();
                        }
                        break;
                    }
            }
        }

        /// <summary>
        /// Check to see if the player rectange and goal rectangle have intersected
        /// </summary>
        public void CheckPointGain()
        {
            if(player.displayArea.IntersectsWith(goal.displayArea))
            {
                player.points += 1;
                goal.chaneLocation();
            }
        }

        /// <summary>
        /// Check to see if the timer has hit zero
        /// </summary>
        public void CheckGameOver()
        {
            if(timeLeft == 0)
            {
                //assign final points
                finalPoints = player.points;

                //reset game 
                player.points = 0;
                timeLeft = 30;
                timerCount = 0;
                gameOver = true;
            }
        }

        /// <summary>
        /// perfrom updates every timer tick
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AnimationTimer_Tick(object sender, EventArgs e)
        {
            //update game time
            timerCount++;
            if (timerCount == 42)
            {
                timerCount = 0;
                timeLeft--;
            }

            CheckGameOver();

            if (!gameOver)
            {
                //check to see if the player gained a point
                CheckPointGain();

                //do jump movement
                player.Jump();

                //check if the player landed on a platform
                player.CheckSurface(platforms);
            }
            else
            {
                animationTimer.Stop();
            }
            //redraw screen
            Invalidate();

        }
    }
}
