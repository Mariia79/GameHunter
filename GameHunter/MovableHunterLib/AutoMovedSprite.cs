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
    public class AutoMovedSprite : MovedSprite 
    {
        public System.Windows.Forms.Timer timer;
     

        public AutoMovedSprite(Point p) : base(p)
        {
            timer = new System.Windows.Forms.Timer();
            timer.Interval = 100;
            timer.Tick += Timer_Tick;
            timer.Start();
        }

        public virtual void Die() 
        {
            if (timer != null)
            {
                timer.Stop();
                timer = null;
            }
            Game.GameField.Controls.Remove(this);
          
        }

        private void Timer_Tick(object? sender, EventArgs e)
        {
            if (Game.IsRun)
                Run();
            else
                timer.Stop();
        }

        public override void Run()
        {
            CheckEnvironment();
            Random rnd = new Random();
            bool forsedChangeDirection = rnd.Next(1, 5) == 5 ;
            CalcDirection(forsedChangeDirection);
            base.Run();
        }

        public virtual void CalcDirection(bool forced) 
        {

        }


    }
}
