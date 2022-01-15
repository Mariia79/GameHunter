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
        public static List<Deer> deers;

        public static Hunter Hunter;
        public static Form GameField;
        public static Form Form;

        public static System.Drawing.SolidBrush RabbitBrush = new System.Drawing.SolidBrush(System.Drawing.Color.Gray);
        public static System.Drawing.SolidBrush WolfBrush = new System.Drawing.SolidBrush(System.Drawing.Color.Black);
        public static System.Drawing.SolidBrush HunterBrush = new System.Drawing.SolidBrush(System.Drawing.Color.Transparent);
        public static System.Drawing.SolidBrush DeerBrush = new System.Drawing.SolidBrush(System.Drawing.Color.SaddleBrown);


        public static bool DeleteDiyngAnimalsAndCheckEndGame()
        {
            for (int i = 0; i < GameAnimals.rabbits.Count; i++)
            {
                if (GameAnimals.rabbits[i].ToEat == true)
                {

                    if (GameAnimals.rabbits[i] is Animals.Hunter)
                    {
                       
                        return true;
                    }

                    GameAnimals.rabbits.RemoveAt(i);
                }
            }


            for (int i = 0; i < GameAnimals.deers.Count; i++)
            {
                if (GameAnimals.deers[i].ToEat == true)
                {

                    GameAnimals.deers.RemoveAt(i);
                }
            }

            for (int i = 0; i < GameAnimals.animals.Count; i++)
            {
                if (GameAnimals.animals[i].ToEat == true)
                {
                    GameAnimals.animals.RemoveAt(i);
                }
            }
            return false;
        }
    }
}
