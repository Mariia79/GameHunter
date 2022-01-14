using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GameHunter
{
    public enum MoveDirection { None, Up, Down, Left, Right };
    public class Sprite : PictureBox, ISkin
    {
        protected Bitmap imageLeft = null;
        protected Bitmap imageRight = null;
        protected Bitmap imageUp = null;
        protected Bitmap imageDown = null;

        public string Directory;
        public string ImageFolder;
        public MoveDirection Direction;



        Point center;
        public Point Center
        {
            get
            {
                center.X = Left + Width / 2;
                center.Y = Top + Height / 2;
                return center;
            }
        }
               
        public Sprite(Point p)
        {
            center = p;
            Direction = MoveDirection.None;
            Left = p.X;
            Top = p.Y;
            Width = 100;
            Height = 100;
            BackColor = System.Drawing.Color.Transparent;
            BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            Game.GameField.Controls.Add(this);

        }

        public virtual void PreloadSkinImages()
        {
            if (imageLeft == null) 
            {
                Directory = Environment.CurrentDirectory + "\\images\\" + ImageFolder + "\\";
                imageLeft = new Bitmap(Directory + "\\left.png");
                imageRight = new Bitmap(Directory + "\\right.png");
                imageUp = new Bitmap(Directory + "\\Up.png");
                imageDown = new Bitmap(Directory + "\\Down.png");
            }
        }

        public virtual void ApplySkin()
        {
           
            switch (Direction)
            {
                case MoveDirection.Left:
                    BackgroundImage = imageLeft;
                    break;
                case MoveDirection.Right:
                    BackgroundImage = imageRight;
                    break;
                case MoveDirection.Up:
                    BackgroundImage = imageUp;
                    break;
                case MoveDirection.Down:
                    BackgroundImage = imageDown;
                    break;
                case MoveDirection.None:
                    BackgroundImage = imageUp;
                    break;
            }
            Invalidate();
        }
        public bool IsIntersection(Sprite other)
        {
            if (other == null)
                return false;

            if (Width < other.Width)
            {
                if (this.Left > other.Left && this.Left < other.Left + other.Width
                && this.Top > other.Top && this.Top < other.Top + other.Height)
                {
                    return true;
                }
            }
            else
            {
                if (other.Left > this.Left && other.Left < this.Left + this.Width
                    && other.Top > this.Top && other.Top < this.Top + this.Height)
                {
                    return true;
                }

            }
            return false;

        }

        public bool IsIntersectionNew(Sprite other)
        {

            if (GetLength(this.Center, other.Center) <= this.Width/2)
                return true;
            return false;

        }


        public double GetLength(Point A, Point B)
        {
            double fx = Math.Pow(Convert.ToDouble(B.X) - Convert.ToDouble(A.X), 2);
            double fy = Math.Pow(Convert.ToDouble(B.Y) - Convert.ToDouble(A.Y), 2);

            return Math.Sqrt(Math.Abs(fx - fy));

        }
     
    }
}

