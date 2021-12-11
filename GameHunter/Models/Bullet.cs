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

            Target killedAnimal = null;
            foreach (Target t in Game.Targets)
            {
                if (this.IsIntersection(t))
                {
                    killedAnimal = t; 
                    break;    
                }
            }

            if (killedAnimal != null)
            {
                killedAnimal.Die();
                if (Game.hunter != null)
                    Game.hunter.HitCount++;

                this.Die();
            }
        }


    }
}
