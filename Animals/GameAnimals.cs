using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Animals
{
    public static class GameAnimals
    {
        public static List<Animal> animals;
        public static List<Rabbit> rabbits;

        public static Hunter Hunter;
        public static Form GameField;
        public static Form Form;

        public static System.Drawing.SolidBrush RabbitBrush = new System.Drawing.SolidBrush(System.Drawing.Color.Gray);
        public static System.Drawing.SolidBrush WolfBrush = new System.Drawing.SolidBrush(System.Drawing.Color.Black);
        public static System.Drawing.SolidBrush HunterBrush = new System.Drawing.SolidBrush(System.Drawing.Color.Transparent);

    }
}
