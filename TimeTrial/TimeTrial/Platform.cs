using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TimeTrial
{
    class Platform
    {
        //The gameForm Rectangle
        private Rectangle canvas;

        /// <summary>
        /// Playform Display Rectangle
        /// </summary>
        public Rectangle displayArea;
        
    
        /// <summary>
        /// Playfrom constructor
        /// </summary>
        /// <param name="canvas">gameForm rectangel</param>
        /// <param name="posX">x spawn location</param>
        /// <param name="posY">y spawn location</param>
        public Platform(Rectangle canvas, double posX, double posY )
        {
            this.canvas = canvas;

            //set  the width and height
            displayArea.Width = 160;
            displayArea.Height = 15;

            double x = canvas.Width * (posX / 4);
            double y = canvas.Height * (posY / 3);

            //intital placement of Platform
            displayArea.Y = (canvas.Bottom - (int)y);
            displayArea.X = (canvas.Left + (int)x) - (int)(displayArea.Width / 2);
            
        }

        /// <summary>
        /// Draw platform rectangle
        /// </summary>
        /// <param name="graphics">gameForms graphics propertie</param>
        public void Draw(Graphics graphics)
        {
            graphics.FillRectangle(Brushes.BurlyWood, displayArea);
        }
    }
}
