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

    public class Wolf : Target, IHit
    {


        private int MeatAddLifeSteps = 100;
        public Wolf(TargetTypes type, Point p) : base(type, p)
        {
            LengthToEnemy = 100;
            Width = 40;
            Height = 40;
            ImageFolder = "wolf";
            Step = 10;
            LifeSteps = 100;
        }

        void CheckWolfBite()
        {
            Target killedAnimal = null;
            foreach (Target t in Game.Targets)
            {

                if (t != this && t.GetTargetType() != TargetTypes.Wolf && this.IsIntersection(t))
                {
                    killedAnimal = t;
                    break;
                }
                    
            }
            if (killedAnimal != null)
            {
                killedAnimal.Die();
                LifeSteps += MeatAddLifeSteps;
                return;
            }

            if (this.IsIntersection(Game.hunter))
            {
                Game.hunter.Die();
            }
        }

        public override void CheckEnvironment()
        {
            if (LifeSteps <= 0)
            {
                this.Die();
            }

            LifeSteps--;
            Game.OnChangeGame();
            
            CheckWolfBite();

            base.CheckEnvironment();

            FindEnemiesToEat();
            if (ClosestEnemy != null)
            {
                ChangeDirectionToClosestEnemy();
            }
        }

        public virtual void FindEnemiesToEat()
        {

            ClosestEnemy = null;
            foreach (Target t in Game.Targets)
            {
                if (t != this && t.GetTargetType()!= TargetTypes.Wolf && IsFindEnemy(t))
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
        public override void CalcDirection(bool forced)
        {
            bool changeDir = forced;


            base.CalcDirection(changeDir);

        }


        public virtual void ChangeDirectionToClosestEnemy()
        {
            if (ClosestEnemy == null)
                return;



            if (ClosestEnemy.Left > this.Left)
            {
                Direction = MoveDirection.Right;
                Left += Step;
            }

            if (ClosestEnemy.Left < this.Left)
            {
                Direction = MoveDirection.Left;
                Left -= Step;
            }

            if (ClosestEnemy.Top < this.Top)
            {
                Direction = MoveDirection.Up;
                Top -= Step;
            }

            if (ClosestEnemy.Top > this.Top)
            {
                Direction = MoveDirection.Down;
                Top += Step;
            }

            ApplySkin();
        }

        
    }
}
