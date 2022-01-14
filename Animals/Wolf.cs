using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Animals
{
    public class Wolf : Animal
    {

        float WolfSpeed;
        
        public Wolf(Size fieldSize) : base(fieldSize)
        {

            WolfSpeed = Speed / 2f;
            Speed = WolfSpeed;
            InterationRadius = 50f;
        }

        public override void Move()
        {
            var target = (from p in GameAnimals.rabbits 
                        let distance = Vector2.Distance(Pos, p.Pos)
                        where p.ToEat == false && distance < InterationRadius
                        orderby distance
                        select p).FirstOrDefault();

           

            if (target != null)
            {

                if (!target.ToEat && Vector2.Distance(Pos, target.Pos) < 40)
                {
                    Thread.Sleep(100);
                    target.ToEat = true;
                }

                Speed = WolfSpeed * 2;
                Vect += target.Pos - Pos;
            }
            else
            {
                Speed = WolfSpeed;
            }

            base.Move();


        }
    }
}
