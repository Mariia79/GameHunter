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
        int FindTargetRadius = 200;
        int LifeScore = 200;
        public Wolf(Size fieldSize) : base(fieldSize)
        {

            WolfSpeed = Speed / 2f;
            Speed = WolfSpeed;
            InterationRadius = 50f;

        }

        public override void Move()
        {
            LifeScore--;
            if (LifeScore <= 0)
            {

                this.ToEat = true;
                return;
            }


            var target = (from p in GameAnimals.animals
                          let distance = Vector2.Distance(Pos, p.Pos)

                          //where p.ToEat == false && distance < InterationRadius
                          where p.IsFoodToWolfs && p.ToEat == false && distance < FindTargetRadius
                          orderby distance
                          select p).FirstOrDefault();


            if (target != null)
            {

                if (!target.ToEat && Vector2.Distance(Pos, target.Pos) < 40)
                {
                    Thread.Sleep(100);
                    target.ToEat = true;

                    if (target is Deer)
                        this.LifeScore += 50;

                    if (target is Rabbit)
                        this.LifeScore += 20;
                }

                Speed = WolfSpeed * 1.5f;
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
