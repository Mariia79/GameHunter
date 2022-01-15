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
        private const int NumDeers = 4;
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


            if (GameAnimals.DeleteDiyngAnimalsAndCheckEndGame())
            {
                _timer.Stop();
                Game.IsRun = false;
                MessageBox.Show("Game over! Hunter is dead!");
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
                int size;

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

                    size = 20;
                    e.Graphics.FillEllipse(GameAnimals.RabbitBrush,
                        new Rectangle(Convert.ToInt32(animal.Pos.X), Convert.ToInt32(animal.Pos.Y), size, size));
                }


                if (animal is Animals.Wolf)
                {
                    size = 30;
                    e.Graphics.FillEllipse(GameAnimals.WolfBrush,
                        new Rectangle(Convert.ToInt32(animal.Pos.X), Convert.ToInt32(animal.Pos.Y), size, size));
                }

                if (animal is Animals.Hunter)
                {
                    size = 40;
                    e.Graphics.FillEllipse(GameAnimals.HunterBrush,
                        new Rectangle(Convert.ToInt32(animal.Pos.X), Convert.ToInt32(animal.Pos.Y), size, size));
                }


                if (animal is Animals.Deer)
                {
                    size = 40;
                    e.Graphics.FillEllipse(GameAnimals.DeerBrush,
                        new Rectangle(Convert.ToInt32(animal.Pos.X), Convert.ToInt32(animal.Pos.Y), size, size));
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
            GameAnimals.deers = new List<Animals.Deer>();
            GameAnimals.rabbits = new List<Animals.Rabbit>();

            GameAnimals.animals = new List<Animals.Animal>();

            for (int i = 0; i < NumDeers; i++)
                GameAnimals.deers.Add(new Animals.Deer(ClientSize));

            for (int i = 0; i < NumRabbits; i++)
                GameAnimals.rabbits.Add(new Animals.Rabbit(ClientSize));

            GameAnimals.animals.AddRange(GameAnimals.deers);
            GameAnimals.animals.AddRange(GameAnimals.rabbits);


            for (int i = 0; i < NumWolfs; i++)
                GameAnimals.animals.Add(new Animals.Wolf(ClientSize));


            Game.InitAnimalsGame(this); // створюємо Hunter з неймспейсу GameHunter, що керований клавіатурою і стріляє


            // створюємо невидмиго персонажа Хантер з бібліотеки GameAnimals що підтримує sterring 
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
