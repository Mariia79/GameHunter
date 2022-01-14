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
//using System.Windows.Media;

namespace GameHunter
{
    public partial class FormGameField : Form
    {
        System.Media.SoundPlayer player;

        // MediaPlayer player = new MediaPlayer();
        public FormGameField()
        {

            InitializeComponent();
       
        }

        private void FormGameField_Load(object sender, EventArgs e)
        {

            Game.InitGame(panelGameField, lblInfo);
       
        }

        private void Game_OnGameChange(object? sender, EventArgs e)
        {
            lblInfo.Text = "Ammo = " + Game.hunter.Ammo.ToString() + ", Hits = "
                + Game.hunter.HitCount.ToString() + ", Targets count = " + Game.Targets.Count.ToString();
        }

        private void FormGameField_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Game.hunter != null)
                Game.hunter.MoveHunter(e);
        }



        private void button1_Click(object sender, EventArgs e)
        {
            MessageBox.Show("wrong Index");
        }

        private void btnStart_Click(object sender, EventArgs e)
        {

            Game.InitGame(panelGameField, lblInfo);

        }

        private void button1_Click_1(object sender, EventArgs e)
        {

        }

        private void FormGameField_Deactivate(object sender, EventArgs e)
        {
            //Game.IsRun = false;
        }

        private void FormGameField_KeyUp(object sender, KeyEventArgs e)
        {
            //if (e.KeyCode != Keys.B)
            //{
            //    Game.hunter.IsCanMove = false;
            //}
        }

        private void button1_Click_2(object sender, EventArgs e)
        {
            player.Stop();
        }

        private void lblInfo_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            player.Play();
        }
    }
}
