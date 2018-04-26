using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TimeTrial
{
    class Player
    {
        //gameForm rectangle
        private Rectangle canvas;
        //movement speed in x direction
        private int xVelocity = 20;
        //movement speed in y direction
        private int yVelocity = 0;
        //inital jump velocity
        private int jumpVelocity = 55;
        //rate that gravity affects jumping
        private int gravity = 3;
        //the players y location at last draw event
        private int previousY;
        //initial y velocity after a collision
        private int collisionVelocity = 0;
        //is the player jumping or on a surface
        private bool jumping = false;

        /// <summary>
        /// posible moves7
        /// </summary>
        public enum Direction {  Left, Right, Up };

        /// <summary>
        /// The players display rectangle
        /// </summary>
        public Rectangle displayArea;

        /// <summary>
        /// The players colected points
        /// </summary>
        public int points = 0;

        /// <summary>
        /// Player constructor
        /// </summary>
        /// <param name="canvas">gameForm rectangel</param>
        public Player(Rectangle canvas)
        {
            this.canvas = canvas;
            //set  the width and height
            displayArea.Width = 20;
            displayArea.Height = 50;
            previousY = 0;

            //intital placement of Player
            displayArea.Y = canvas.Bottom - displayArea.Height;
            displayArea.X = (canvas.Width / 2) - (displayArea.Width / 2);
        }

        /// <summary>
        /// Draw player object
        /// </summary>
        /// <param name="graphics"></param>
        public void Draw(Graphics graphics)
        {
            graphics.FillRectangle(Brushes.Goldenrod, displayArea);
        }

        /// <summary>
        /// Move player object
        /// </summary>
        /// <param name="direction">Direction from key press</param>
        public void Move(Direction direction)
        {
            switch(direction)
            {
                case Direction.Left:
                    {
                        displayArea.X = (displayArea.X <= 20 ? canvas.Left : displayArea.X - xVelocity);
                        break;
                    }
                case Direction.Right:
                    {
                        int maxVal = canvas.Width - displayArea.Width;
                        displayArea.X = (displayArea.X >= maxVal ? maxVal : displayArea.X + xVelocity);
                        break;
                    }
                case Direction.Up:
                    {
                        if(!jumping)
                        {
                            yVelocity = jumpVelocity;

                            jumping = true;
                        }
                        break;
                    }
            }
        }

        /// <summary>
        /// change player vertical velocity
        /// </summary>
        public void Jump()
        {
            //set previous y to the current y before falling or raising
            previousY = displayArea.Y + displayArea.Height;

            if (jumping)
            {
                
                yVelocity -= gravity;
                displayArea.Y -= yVelocity;
            }
            else
            {
                
                yVelocity = 0;
            }
            
        }

        /// <summary>
        /// Check for player collision with a surface
        /// </summary>
        /// <param name="plats">Platform rectangles</param>
        public void CheckSurface(HashSet<Platform> plats)
        {
            //if the player is in the x range of the platform check to see if it is on top of it
            foreach(Platform plat in plats)
            {
                if(displayArea.IntersectsWith(plat.displayArea))
                {
                    if(previousY - plat.displayArea.Height < plat.displayArea.Y)
                    {
                        displayArea.Y = plat.displayArea.Y - displayArea.Height ;
                        jumping = false;
                    }
                    else
                    {
                        displayArea.Y = plat.displayArea.Y + plat.displayArea.Height;
                        yVelocity = collisionVelocity;
                    }

                    break;
                }
                //if not on a plat set jumping to true to allow player to fall
                jumping = true;

            }
            //if still jumping after foreach
            if(jumping)
            {
                //check for colision with the top of the screen
                if(displayArea.Y <= canvas.Top)
                {
                    displayArea.Y = canvas.Top + 1; // +1 to add a buffer space so thsi check is not triggered again
                    yVelocity = collisionVelocity;
                }
                if (displayArea.Y >= canvas.Bottom - displayArea.Height)
                {
                    displayArea.Y = canvas.Bottom - displayArea.Height;
                    jumping = false;
                }
                else
                {
                    jumping = true;
                }
            }
        }

        /// <summary>
        /// Getter for jump bool
        /// </summary>
        /// <returns>jumping bool</returns>
        public bool Get_Jumping()
        {
            return jumping;
        }
    }
}
