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
          

        }

        protected override void OnPaint(PaintEventArgs e)
        {
            
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
