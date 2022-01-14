using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Animals;

namespace GameHunter
{
    public partial class MainWindow : Form
    {
        private const int WindowWidth = 1000;
        private const int WindowHeight = 800;

        private const int NumRabbits = 30;
        private const int NumWolfs = 3;


        private Timer _timer;
        private Stopwatch _sw = new Stopwatch();
        private long _renderTime;
        private Font _font = new Font("Arial", 12);


        Image image;
        TextureBrush tBrush;

        public MainWindow()
        {
            //image = new Bitmap("1.png");
            //tBrush = new TextureBrush(image);

            InitializeComponent();

            BackColor = Color.ForestGreen;
            GameAnimals.GameField = this;
            // Window setup
            Text = "Animals";
            SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.DoubleBuffer, true);
            StartPosition = FormStartPosition.CenterScreen;
            ClientSize = new Size(WindowWidth, WindowHeight);


        }

        private void OnTick(object sender, EventArgs e)
        {
            if (Game.IsRun == false)
                return;

            _sw.Restart();


            Parallel.ForEach(GameAnimals.animals, (animal) =>
            {
                animal.Move();



            });


            for (int i = 0; i < GameAnimals.rabbits.Count; i++)
            {
                if (GameAnimals.rabbits[i].ToEat == true)
                {

                    if (GameAnimals.rabbits[i] is Animals.Hunter)
                    {
                        _timer.Stop();
                        Game.IsRun = false;
                        MessageBox.Show("Game over! Hunter is dead!");
                        return;
                    }

                    GameAnimals.rabbits.RemoveAt(i);


                }
            }

            for (int i = 0; i < GameAnimals.animals.Count; i++)
            {
                if (GameAnimals.animals[i].ToEat == true)
                {
                    GameAnimals.animals.RemoveAt(i);
                }
            }

            _sw.Stop();
            _renderTime = _sw.ElapsedMilliseconds;


            Invalidate();

        }

        protected override void OnPaint(PaintEventArgs e)
        {
            if (Game.IsRun == false)
                return;

            if (_renderTime > 0)
            {
                e.Graphics.DrawString(
                    string.Concat(1000 / _renderTime, " fps"),
                    _font, Brushes.Black, 10, 10);
            }

            foreach (var animal in GameAnimals.animals)
            {
                int size = animal is Animals.Wolf ? 30 : 20;

                if (animal is Animals.Hunter)
                    size = 40;

                float angle;
                if (animal.Vect.X == 0)
                    angle = 90f;
                else
                    angle = (float)(Math.Atan(animal.Vect.Y / animal.Vect.X) * (180 / Math.PI));

                if (animal.Vect.X < 0) angle += 180f;

                var matrix = new Matrix();

                matrix.RotateAt((float)angle, new PointF(animal.Pos.X, animal.Pos.Y));
                e.Graphics.Transform = matrix;


                if (animal is Animals.Rabbit && animal is not Animals.Hunter)
                {

                    e.Graphics.FillEllipse(GameAnimals.RabbitBrush,
                        new Rectangle(Convert.ToInt32(animal.Pos.X), Convert.ToInt32(animal.Pos.Y), size, size));
                }


                if (animal is Animals.Wolf)
                {
                    e.Graphics.FillEllipse(GameAnimals.WolfBrush,
                        new Rectangle(Convert.ToInt32(animal.Pos.X), Convert.ToInt32(animal.Pos.Y), size, size));
                }

                if (animal is Animals.Hunter)
                {
                    e.Graphics.FillEllipse(GameAnimals.HunterBrush,
                        new Rectangle(Convert.ToInt32(animal.Pos.X), Convert.ToInt32(animal.Pos.Y), 40, 40));
                }

            }
        }

        private void MainWindow_KeyDown(object sender, KeyEventArgs e)
        {

        }

        private void MainWindow_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Game.hunter != null)
                Game.hunter.MoveHunter(e);
        }

        private void newGameToolStripMenuItem_Click(object sender, EventArgs e)
        {
            StartGame();
        }


        void StartGame()
        {


            // Animal setup
            GameAnimals.rabbits = new List<Animals.Rabbit>();
            GameAnimals.animals = new List<Animals.Animal>();

            for (int i = 0; i < NumRabbits; i++)
                GameAnimals.rabbits.Add(new Animals.Rabbit(ClientSize));

            GameAnimals.animals.AddRange(GameAnimals.rabbits);

            for (int i = 0; i < NumWolfs; i++)
                GameAnimals.animals.Add(new Animals.Wolf(ClientSize));




            Game.InitAnimalsGame(this);

            GameAnimals.Hunter = new Animals.Hunter(ClientSize);
            GameAnimals.Hunter.Pos.X = Game.hunter.Center.X;
            GameAnimals.Hunter.Pos.Y = Game.hunter.Center.Y;
            
            GameAnimals.rabbits.Add(GameAnimals.Hunter);
            GameAnimals.animals.Add(GameAnimals.Hunter);



            _timer = new Timer();
            _timer.Tick += new EventHandler(OnTick);
            _timer.Interval = 75;
            _timer.Start();

            Game.IsRun = true;

        }
    }
}
