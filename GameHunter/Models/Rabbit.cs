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
    
    public class Rabbit : Target, IHit
    {
     
        public Rabbit(TargetTypes type, Point p) : base(type, p)
        {
            LengthToEnemy = 40;
            Width = 30;
            Height = 30;
            ImageFolder = "rabbit";
            Step = 15;
        }

        public override void CheckEnvironment()
        {
            base.CheckEnvironment();

            FindEnemiesToRunAway();

            if (ClosestEnemy != null)
            {
                ChangeDirectionFromClosestEnemy();
            }

            
        }


        public virtual void FindEnemiesToRunAway()
        {

            ClosestEnemy = null;
            foreach (Target t in Game.Targets)
            {
                if (t != this && IsFindEnemy(t))
                {
                    ClosestEnemy = t;
                    break;
                }
            }

            if (IsFindEnemy(Game.hunter))
            {
                ClosestEnemy = Game.hunter;
            }
        }

        public virtual void ChangeDirectionFromClosestEnemy()
        {
            if (ClosestEnemy == null)
                return;

            if (ClosestEnemy.Left > this.Left)
            {
                Direction = MoveDirection.Left;
                Left = Left - LengthToEnemy / 2;
            }
            if (ClosestEnemy.Left < this.Left)
            {
                Direction = MoveDirection.Right;
                Left = Left + LengthToEnemy / 2;
            }

            if (ClosestEnemy.Top < this.Top)
            {
                Direction = MoveDirection.Down;
                Top = Top + LengthToEnemy / 2;
            }

            if (ClosestEnemy.Top > this.Top)
            {
                Direction = MoveDirection.Up;
                Top = Top - LengthToEnemy / 2;
            }

            ApplySkin();
        }

    }
}
