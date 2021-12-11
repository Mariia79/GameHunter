using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.ComponentModel;

namespace GameHunter
{
    public enum TargetTypes { Deer, Rabbit, Wolf };
    public class Target : AutoMovedSprite, IHit
    {
        public int LifeSteps = 100; 
        System.Media.SoundPlayer player;
        TargetTypes targetType;
        public AutoMovedSprite ClosestEnemy = null;
        public TargetTypes GetTargetType()
        {
            return targetType;
        }

        public Target(TargetTypes type, Point p) : base(p)
        {
            targetType = type;
            player = new System.Media.SoundPlayer(Environment.CurrentDirectory + "\\sounds\\screem.wav");
        }

        public int LengthToEnemy = 100;

        public override void CheckEnvironment()
        {
            if (IsAbroad) 
            {
                Die();
                Game.TargetsAbroadCount++; 
            }
          
        }

        public bool IsFindEnemy(Sprite other)
        {
            if (other != null)
            {
                int length = Convert.ToInt32(GetLength(this.Center, other.Center));

                if (length < LengthToEnemy)
                    return true;
            }

            return false;

        }

        public override void Die()
        {
            player.Play();
            base.Die();
            Game.Targets.Remove(this);
            Game.OnChangeGame();
        }

        public override void CalcDirection(bool forced)
        {
            Random ran = new Random();
            int dir = ran.Next(1, 6);

            switch (Direction)
            {
                case MoveDirection.Left:
                    if (forced || (Left + Width <= Game.BorderAlarmWidth))
                    {

                        switch (dir)
                        {
                            case 1:
                                Direction = MoveDirection.Up;
                                break;
                            case 2:
                                Direction = MoveDirection.Down;
                                break;
                            default:
                                Direction = MoveDirection.Right;
                                break;
                        }
                        ApplySkin();
                    }

                    break;
                case MoveDirection.Right:

                    if ((forced || Left >= Game.GameField.Width - Game.BorderAlarmWidth))
                    {
                        switch (dir)
                        {

                            case 1:
                                Direction = MoveDirection.Up;
                                break;
                            case 2:
                                Direction = MoveDirection.Down;
                                break;
                            default:
                                Direction = MoveDirection.Left;
                                break;
                        }
                        ApplySkin();
                    }

                    break;
                case MoveDirection.Up:

                    if (forced || (Top + Height <= Game.BorderAlarmWidth))
                    {
                        switch (dir)
                        {
                            case 1:
                                Direction = MoveDirection.Left;
                                break;
                            case 2:
                                Direction = MoveDirection.Right;
                                break;
                            default:
                                Direction = MoveDirection.Down;
                                break;
                        }
                        ApplySkin();
                    }
                    break;
                case MoveDirection.Down:

                    if (forced || (Top >= Game.GameField.Height - Game.BorderAlarmWidth))
                    {
                        switch (dir)
                        {
                            case 1:
                                Direction = MoveDirection.Left;
                                break;
                            case 2:
                                Direction = MoveDirection.Right;
                                break;
                            default:
                                Direction = MoveDirection.Up;
                                break;

                        }
                        ApplySkin();
                    }

                    break;

                case MoveDirection.None:

                    {
                        switch (dir)
                        {
                            case 1:
                                Direction = MoveDirection.Left;
                                break;
                            case 2:
                                Direction = MoveDirection.Right;
                                break;
                            case 3:
                                Direction = MoveDirection.Down;
                                break;
                            default:
                                Direction = MoveDirection.Up;
                                break;

                        }
                        ApplySkin();
                        break;
                    }

            }
        }


       
    }
}
