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

    public class MovedSprite : Sprite 
    {
        public int Step = 8; 
        public bool IsAbroad 
        {
            get
            {
                return  Left < 0 || Left + Width > Game.GameField.Width
                || Top < 0 || Top + Height > Game.GameField.Height;
            }
        }

        public MovedSprite(Point p) : base(p)
        {

        }

        public virtual void CheckEnvironment() 
        {

        }

        public virtual void Run() 
        {
            switch (Direction)
            {
                case MoveDirection.Left:
                    Left = Left - Step;
                    break;
                case MoveDirection.Right:
                    Left = Left + Step;
                    break;
                case MoveDirection.Up:
                    Top = Top - Step;
                    break;
                case MoveDirection.Down:
                    Top = Top + Step;
                    break;
                case MoveDirection.None:
                    break;
            }

        }
    }
}
