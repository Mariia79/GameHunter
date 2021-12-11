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

    public class DeersGroup
    {
        private List<Deer> Deers;


        public DeersGroup()
        {
           
            
        }

        public void CreateGroup(int realCount)
        {
            Point groupPos = Game.GetRandomPosition();
            Deer newDeer = new Deer(TargetTypes.Deer, groupPos);
            Game.Targets.Add(newDeer);

            Point nextPos;
            
            for (int i = 1; i < realCount; i++)
            {
                nextPos = new Point();
                nextPos.Y = groupPos.Y;
                nextPos.X = groupPos.X + i * newDeer.Width;
                newDeer = new Deer(TargetTypes.Deer, nextPos);
               
                Game.Targets.Add(newDeer);
            }
        }

    }

    public class Deer : Target, IHit
    {
        Target ClosestDeer = null;
        public Deer(TargetTypes type, Point p) : base(type, p)
        {
            LengthToEnemy = 110;
            Width = 60;
            Height = 60;
            ImageFolder = "deer";
            Step = 5;
        }

        public override void CheckEnvironment()
        {
            base.CheckEnvironment();

            FindEnemiesToRunAway();

            if (ClosestEnemy != null)
            {
                ChangeDirectionFromClosestEnemy();
            }

            FindOtherDeers();
            if (ClosestDeer != null)
            {
                ChangeDirectionToClosestDeer();
            }
        }

        public virtual void FindOtherDeers()
        {

            ClosestDeer = null;
            foreach (Target t in Game.Targets)
            {
                if (t != this && t.GetTargetType() == TargetTypes.Deer && IsFindEnemy(t))
                {
                    ClosestDeer = t;
                    break;
                }
            }
          
        }


        public virtual void ChangeDirectionToClosestDeer()
        {
            if (ClosestDeer == null)
                return;

            if (ClosestDeer.Left > this.Left)
            {
                Direction = MoveDirection.Right;
            
            }
            if (ClosestDeer.Left < this.Left)
            {
                Direction = MoveDirection.Left;
      
            }

            if (ClosestDeer.Top < this.Top)
            {
                Direction = MoveDirection.Up;

            }

            if (ClosestDeer.Top > this.Top)
            {
                Direction = MoveDirection.Down;

            }

            ApplySkin();
        }

        public virtual void FindEnemiesToRunAway()
        {

            ClosestEnemy = null;
            foreach (Target t in Game.Targets)
            {
                if (t != this && t.GetTargetType() == TargetTypes.Wolf && IsFindEnemy(t))
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
