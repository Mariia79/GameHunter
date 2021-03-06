using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Windows.Forms;
using Animals;

namespace GameHunter
{
    public class Hunter : AutoMovedSprite
    {
        System.Media.SoundPlayer player;
        public int Ammo = 100;
        public int HitCount = 0;

        public bool IsCanMove = false;
        public Hunter(Point p) : base(p)
        {
            timer.Stop();
            Width = 40;
            Height = 40;
            ImageFolder = "hunter";
            Direction = MoveDirection.Up;
            player = new System.Media.SoundPlayer(Environment.CurrentDirectory + "\\sounds\\shoot.wav");
        }

        public override void Run()
        {
            if (IsAbroad)
                ChangeDirection();

            if (IsCanMove)
            {
                ApplySkin();
                base.Run();

                if (GameAnimals.Hunter != null)
                {
                    GameAnimals.Hunter.Pos.X = Game.hunter.Center.X;
                    GameAnimals.Hunter.Pos.Y = Game.hunter.Center.Y;
                }
            }

        }
        public override void Die()
        {
            Game.GameField.Controls.Remove(this);
            Game.hunter = null;
            base.Die();
        }

        public virtual void ChangeDirection()
        {
            switch (Direction)
            {
                case MoveDirection.Down:
                    Direction = MoveDirection.Up;
                    Top = Game.GameField.Height - Height - 1;
                    break;
                case MoveDirection.Up:
                    Direction = MoveDirection.Down;
                    Top = 1;
                    break;

                case MoveDirection.Left:
                    Direction = MoveDirection.Right;
                    Left = 1;
                    break;
                case MoveDirection.Right:
                    Direction = MoveDirection.Left;
                    Top = Game.GameField.Width - Width - 1;
                    break;
            }

            ApplySkin();
        }
        public void MoveHunter(KeyPressEventArgs e)
        {

            if (e.KeyChar == 'w')
            {
                Direction = MoveDirection.Up;
                IsCanMove = true;
                Run();

            }
            if (e.KeyChar == 'a')
            {
                Direction = MoveDirection.Left;
                IsCanMove = true;
                Run();


            }
            if (e.KeyChar == 'd')
            {
                Direction = MoveDirection.Right;
                IsCanMove = true;
                Run();

            }
            if (e.KeyChar == 's')
            {
                Direction = MoveDirection.Down;
                IsCanMove = true;
                Run();

            }
            if (e.KeyChar == 'b')
            {
                Shoot();
            }

            if (e.KeyChar == 'x')
            {
                Game.hunter.IsCanMove = false;
            }

        }

        public void Shoot()
        {

            if (Ammo > 0)
            {
                Ammo--;
                player.Play();

                Point p = new Point();
                p.X = Left + Width / 2;
                p.Y = Top + Height / 2;
                Bullet Bullet = new Bullet(p);
                Bullet.ApplySkin();
                Bullet.Direction = this.Direction;
                Bullet.Run();
            }
        }

    }
}
