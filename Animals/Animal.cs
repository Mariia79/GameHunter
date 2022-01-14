using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Numerics;
using System.Drawing;
using System.Windows.Forms;

namespace Animals
{
    public abstract class Animal
    {
        public Vector2 Vect;
        public Vector2 Pos;

        public bool ToEat = false;

        protected const int BorderPadding = 50;
        protected Single Speed = 10f;
        protected Single InterationRadius = 50f;// суспільни рух у напрямку

        private Size _fieldSize;
        private static Random _rnd = new Random();

        public PictureBox but;

        public Animal(Size fieldSize)
        {
            _fieldSize = fieldSize;

            Pos = new Vector2(
                _rnd.Next(_fieldSize.Width),
                _rnd.Next(_fieldSize.Height));


            Complex c = Complex.FromPolarCoordinates(_rnd.NextDouble() * Speed,
                _rnd.NextDouble() * Math.PI * 2);

            Vect.X = (float)c.Real;
            Vect.Y = (float)c.Imaginary;


        }


        protected void CheckFieldBorder()
        {

            if (Pos.X < BorderPadding)
                Vect.X += BorderPadding - Pos.X;

            if (Pos.Y < BorderPadding) 
                Vect.Y += BorderPadding - Pos.Y;

            if (Pos.X > _fieldSize.Width - BorderPadding)
                Vect.X += _fieldSize.Width - BorderPadding - Pos.X;

            if (Pos.Y > _fieldSize.Height - BorderPadding)
                Vect.Y += _fieldSize.Height - BorderPadding - Pos.Y;

        }

        virtual public void Move()
        {

            CheckFieldBorder();
            // Speed check
            var length = Vect.Length();
            if (length > Speed)
                Vect *= Speed / length;

            Pos += Vect;
      

        }
    }
}
