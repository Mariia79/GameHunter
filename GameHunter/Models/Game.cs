﻿using System;
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
        public static GameEndTypes GameEndType = GameEndTypes.None;
        public static int BorderAlarmWidth = 200;
        public static Label lblInfo;
        public static bool IsRun = false;
        public static Form GameField;

        public static int DeersCount = 0;//12;
        public static int MinCount = 3;
        public static int MaxCount = 5;
        static int WolvesCount = 0;//2;

        static int RabbitsCount = 10;
        public static int TargetsAbroadCount = 0;

        public static List<Target> Targets;



        static public Hunter hunter;

        public static Form Form;


        public static void InitAnimalsGame(Form p)
        {
            Restart();


            GameField = p;
            GameField.Controls.Clear();

            hunter = new Hunter(GetHunterPosition());
            hunter.PreloadSkinImages();
            hunter.ApplySkin();


            IsRun = true;

            OnChangeGame();

        }

        public static void InitGame(Form p, Label l)
        {
            Restart();

            lblInfo = l;

            GameField = p;
            GameField.Controls.Clear();

            hunter = new Hunter(GetHunterPosition());
            hunter.PreloadSkinImages();
            hunter.ApplySkin();

            //Targets = new List<Target>();
            //for (int i = 0; i < RabbitsCount; i++)
            //    Targets.Add(new Rabbit(TargetTypes.Rabbit, GetRandomPosition()));
            //for (int i = 0; i < WolvesCount; i++)
            //    Targets.Add(new Wolf(TargetTypes.Wolf, GetRandomPosition()));

            //int deersLost = DeersCount;
            //int RealCount;

            //while (deersLost > 0)
            //{
            //    Random rnd = new Random();
            //    RealCount = rnd.Next(MinCount, MaxCount);

            //    DeersGroup dg = new DeersGroup();
            //    dg.CreateGroup(RealCount);
            //    deersLost -= RealCount;
            //}

            //foreach (IHit t in Targets)
            //{
            //    t.PreloadSkinImages();
            //    t.ApplySkin();
            //}

            IsRun = true;

            OnChangeGame();

        }

        public static int GetWolfsLife()
        {
            foreach (Target t in Targets)
            {
                if (t.GetTargetType() == TargetTypes.Wolf)
                {
                    return t.LifeSteps;
                }
            }

            return 0;
        }

        static bool CheckEndGame()
        {
            //string myMessage ="";
            //if (hunter != null && Targets != null)
            //{
            //    myMessage = "Ammo = " + hunter.Ammo.ToString() + ", Hits = " 
            //                + hunter.HitCount.ToString() + ", T. count = " 
            //                + Targets.Count.ToString() + " T. down = " + TargetsAbroadCount.ToString()
            //    + " wolf[0].life = "
            //        + Game.GetWolfsLife().ToString();
            //    lblInfo.Text = myMessage;
            //}


            //if (hunter == null)
            //{
            //    GameEndType = GameEndTypes.HunterIsDead;
            //    lblInfo.Text = myMessage + ". Game is over! Hunter is dead!";
            //    return true;
            //}
            //else
            //{

            //    if (hunter.Ammo == 0)
            //    {
            //        GameEndType = GameEndTypes.AmmoLost;
            //        lblInfo.Text = myMessage + ". Game is over! Ammo is Lost!";
            //        return true;
            //    }

            //    if (Targets.Count == 0)
            //    {
            //        lblInfo.Text = myMessage + ". Game is over! Hunter Win!!!";
            //        GameEndType = GameEndTypes.Win;
            //        return true;
            //    }

            //}


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

        public static Point GetHunterPosition()
        {

            Point p = new Point(0, 0);

            p.X = GameField.Width / 2;
            p.Y = GameField.Height - 100;
            return p;
        }


        public static void Restart()
        {
            //if (Game.hunter != null)
            //    Game.hunter.Die();

            if (Targets != null)
                while (Targets.Count > 0)
                {
                    Targets[0].Die();
                }
            if (Game.GameField != null)
                while (Game.GameField.Controls.Count > 1)
                {
                    Game.GameField.Controls.Remove(Game.GameField.Controls[1]);
                }

        }

        public static void OnChangeGame()  
        {
            IsRun = !CheckEndGame();

            if (!IsRun)
            {
                Restart();
            }

        }


    }
}
