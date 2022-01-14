using System;
using System.Collections.Generic;
using System.Drawing;
using System.Numerics;

namespace Animals
{
    public class Rabbit : Animal
    {
        private const Single FlockSpacing = 50f;// розбігатись на дистанцію
        private const Single JumpFactor = 50f;


        public Rabbit(Size fieldSize) : base(fieldSize)
        {
        }

        public override void Move()
        {
            foreach (var animal in GameAnimals.animals)
            {
                if (animal == this) continue;

                var d = Vector2.Distance(Pos, animal.Pos);

                // Avoid Wolfs (and rotate clockwise by 1 degree)
                if (animal is Wolf)
                {
                    if (d < InterationRadius) Vect += RotateRadians((Pos - animal.Pos), 1) * JumpFactor;
                    continue;
                }

                // Keep space between animals (separation)
                if (d < FlockSpacing)
                {
                    Vect += (Pos - animal.Pos) * 15;
                }
                // Flock together (cohesion)
                else if (d < InterationRadius)
                {
                    Vect += (animal.Pos - Pos) * 0.1f;
                }
                // Align direction (alignment)
                if (d < InterationRadius)
                {
                    Vect += animal.Vect * 0.5f;
                }
            }

            base.Move();
        }

        public static Vector2 RotateRadians(Vector2 v, double radians)
        {
            Single ca = (Single)Math.Cos(radians);
            Single sa = (Single)Math.Sin(radians);
            return new Vector2(ca * v.X - sa * v.Y, sa * v.X + ca * v.Y);
        }
    }
}
