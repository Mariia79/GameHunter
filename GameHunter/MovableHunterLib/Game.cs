using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Media;
using Animals;

namespace GameHunter
{
    public enum GameEndTypes { None, AmmoLost, HunterIsDead, Win };
    public static class Game
    {
        public static GameEndTypes GameEndType = GameEndTypes.None;
        public static int BorderAlarmWidth = 200;
        public static Label lblInfo;
        public static bool IsRun = false;
        public static Form GameField;
        public static int MinCount = 3;
        public static int MaxCount = 5;

      

        static public Hunter hunter;

      
        static bool CheckEndGame()
        {
          
            return false;
        }

        public static Point GetRandomPosition()
        {
            int delta = 200;
            Point p = new Point(0, 0);
            Random random = new Random();
            p.X = random.Next(0 + delta, GameField.Width - delta);
            p.Y = random.Next(0 + delta, GameField.Height - delta);
            return p;
        }

        static Point GetHunterPosition()
        {

            Point p = new Point(0, 0);

            p.X = GameField.Width / 2;
            p.Y = GameField.Height - 100;
            return p;
        }





        public static void InitAnimalsGame(Form p)
        {


            GameField = p;
            GameField.Controls.Clear();

            hunter = new Hunter(GetHunterPosition());
            hunter.PreloadSkinImages();
            hunter.ApplySkin();

            IsRun = true;


        }

    }
}
