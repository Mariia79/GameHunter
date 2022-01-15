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


namespace GameHunter
{
    public enum GameEndTypes { None, AmmoLost, HunterIsDead, Win };
    public static class Game
    {

        public static Label lblInfo;
        public static bool IsRun = false;
        public static Form GameField;
        static public Hunter hunter;

        public static Form Form;


        public static void InitAnimalsGame(Form p)
        {


            GameField = p;
            GameField.Controls.Clear();

            hunter = new Hunter(GetHunterPosition());
            hunter.PreloadSkinImages();
            hunter.ApplySkin();

            IsRun = true;

        }


        public static Point GetHunterPosition()
        {

            Point p = new Point(0, 0);

            p.X = GameField.Width / 2;
            p.Y = GameField.Height - 100;
            return p;
        }


    }
}
