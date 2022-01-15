using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.ComponentModel;
using Animals;
using System.Numerics;

namespace GameHunter
{
    public class Bullet : AutoMovedSprite
    {
        int LifeSteps = 35;

        protected static Bitmap image = null;
        public Bullet(Point p) : base(p)
        {
            timer.Interval = 20;
            Width = 20;
            Height = Width;
            ImageFolder = "bullet";
            Step = 20;
        }

        private void Timer_Tick(object? sender, EventArgs e)
        {
            Run();
        }

        public override void ApplySkin()
        {
            if (image == null)
            {
                image = new Bitmap(Environment.CurrentDirectory + "\\images\\bullet\\bullet.png");
               
            }
            BackgroundImage = image;
            Invalidate();
        }

        public override void Run()
        {
            LifeSteps--;
            if (IsAbroad || LifeSteps <= 0)
            {
                Die();
                return;
            }
            base.Run();

        }

        public override void CheckEnvironment()
        {

            Animal Target = null;
           

            if (GameAnimals.animals == null)
                return;

            foreach (Animal t in GameAnimals.animals)
            {
                if (t is Hunter)
                    continue;

                if (Vector2.Distance(t.Pos, new Vector2(this.Center.X, this.Center.Y)) < 50)
                {
                    Target = t; 
                    break;    
                }
            }

            if (Target != null)
            {
               
                if (Game.hunter != null)
                    Game.hunter.HitCount++;

                GameAnimals.animals.Remove(Target);
                if (Target is Animals.Rabbit)
                {
                    GameAnimals.rabbits.Remove((Animals.Rabbit)Target);
                }

                this.Die();
            }
        }


        //public override void CheckEnvironment()
        //{

        //    Target killedAnimal = null;

        //    if (Game.Targets == null)
        //        return;

        //    foreach (Target t in Game.Targets)
        //    {
        //        if (this.IsIntersection(t))
        //        {
        //            killedAnimal = t;
        //            break;
        //        }
        //    }

        //    if (killedAnimal != null)
        //    {
        //        killedAnimal.Die();
        //        if (Game.hunter != null)
        //            Game.hunter.HitCount++;

        //        this.Die();
        //    }
        //}
    }
}
