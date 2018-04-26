using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TimeTrial
{
    class Goal
    {
        //The gameForm Rectangle
        private Rectangle canvas;
        //The numerical value of the current goal location (1-9)
        private int curentLoc = 2;
        //Goal rectangle dimentions
        private int size = 21;

        //distance from a patform
        private int floatHight = 30;

        //to randomly assign the goals location
        private Random rnd = new Random();

        /// <summary>
        /// Goals display area
        /// </summary>
        public Rectangle displayArea;

        /// <summary>
        /// Goal Constructor
        /// </summary>
        /// <param name="canvas"></param>
        public Goal(Rectangle canvas)
        {
            this.canvas = canvas;
            displayArea.Height = size;
            displayArea.Width = size;

            //set the initial location
            chaneLocation();
        }

        /// <summary>
        /// Change location after player collision
        /// </summary>
        public void chaneLocation()
        {
            bool notMoved = true;

            while(notMoved)
            {
                //get a random number between 1 and 9
                int pos = rnd.Next(1, 10);

                //make sure it is not the same location
                if(pos != curentLoc)
                {
                    notMoved = false;
                }

                curentLoc = pos;
            }
        }

        /// <summary>
        /// Draw Goal rectangle
        /// </summary>
        /// <param name="graphics"></param>
        public void Draw(Graphics graphics)
        {

            switch (curentLoc)
            {
                case 1:
                    {
                        double x = canvas.Width * ((double)1 / 4);

                        //intital placement of Platform
                        displayArea.Y = (canvas.Bottom - floatHight);
                        displayArea.X = (canvas.Left + (int)x) - (int)(displayArea.Width / 2);
                        break;
                    }
                case 2:
                    {
                        double x = canvas.Width * ((double)2 / 4);

                        //intital placement of Platform
                        displayArea.Y = (canvas.Bottom - floatHight);
                        displayArea.X = (canvas.Left + (int)x) - (int)(displayArea.Width / 2);
                        break;
                    }
                case 3:
                    {
                        double x = canvas.Width * ((double)3 / 4);

                        //intital placement of Platform
                        displayArea.Y = (canvas.Bottom - floatHight);
                        displayArea.X = (canvas.Left + (int)x) - (int)(displayArea.Width / 2);
                        break;
                    }
                case 4:
                    {
                        double x = canvas.Width * ((double)1 / 4);
                        double y = canvas.Height * ((double)1 / 3);

                        //intital placement of Platform
                        displayArea.Y = (canvas.Bottom - (int)y - floatHight);
                        displayArea.X = (canvas.Left + (int)x) - (int)(displayArea.Width / 2);
                        break;
                    }
                case 5:
                    {
                        double x = canvas.Width * ((double)2 / 4);
                        double y = canvas.Height * ((double)1 / 3);

                        //intital placement of Platform
                        displayArea.Y = (canvas.Bottom - (int)y - floatHight);
                        displayArea.X = (canvas.Left + (int)x) - (int)(displayArea.Width / 2);
                        break;
                    }
                case 6:
                    {
                        double x = canvas.Width * ((double)3 / 4);
                        double y = canvas.Height * ((double)1 / 3);

                        //intital placement of Platform
                        displayArea.Y = (canvas.Bottom - (int)y - floatHight);
                        displayArea.X = (canvas.Left + (int)x) - (int)(displayArea.Width / 2);
                        break;
                    }
                case 7:
                    {
                        double x = canvas.Width * ((double)1 / 4);
                        double y = canvas.Height * ((double)2 / 3);

                        //intital placement of Platform
                        displayArea.Y = (canvas.Bottom - (int)y - floatHight);
                        displayArea.X = (canvas.Left + (int)x) - (int)(displayArea.Width / 2);
                        break;
                    }
                case 8:
                    {
                        double x = canvas.Width * ((double)2 / 4);
                        double y = canvas.Height * ((double)2 / 3);

                        //intital placement of Platform
                        displayArea.Y = (canvas.Bottom - (int)y - floatHight);
                        displayArea.X = (canvas.Left + (int)x) - (int)(displayArea.Width / 2);
                        break;
                    }
                case 9:
                    {
                        double x = canvas.Width * ((double)3 / 4);
                        double y = canvas.Height * ((double)2 / 3);

                        //intital placement of Platform
                        displayArea.Y = (canvas.Bottom - (int)y - floatHight);
                        displayArea.X = (canvas.Left + (int)x) - (int)(displayArea.Width / 2);
                        break;
                    }
            }

            graphics.FillRectangle(Brushes.DeepPink, displayArea);
        }
    }
}
