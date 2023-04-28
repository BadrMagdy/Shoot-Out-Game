using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows.Forms;

namespace Shoot_Out_Game
{
    internal class Bullet
    {
        public string direction;
        public int bullteLeft;
        public int bulltTop;

        private int speed = 20;
        private PictureBox bullte = new PictureBox();
        private Timer bullteTimer = new Timer();

        public void makeBullte(Form form)
        {
            bullte.BackColor = Color.White;
            bullte.Size = new Size(5,5);
            bullte.Tag = "bullet";
            bullte.Left = bullteLeft;
            bullte.Top = bulltTop;
            bullte.BringToFront();

            form.Controls.Add(bullte);

            bullteTimer.Interval = speed;
            bullteTimer.Tick += new EventHandler(BullteTimerEvent);
            bullteTimer.Start();
        }

        private void BullteTimerEvent(Object sender, EventArgs e)
        {
            if(direction == "left")
            {
                bullte.Left -= speed;
            }
            if(direction == "right") 
            {
                bullte.Left += speed;
            }
            if(direction=="up") 
            {
                bullte.Top -= speed;
            }
            if(direction == "down")
            {
                bullte.Top += speed;
            }


        }

    }
}
