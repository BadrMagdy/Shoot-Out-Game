using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Shoot_Out_Game
{
    public partial class Form1 : Form
    {
        bool goLeft, goRight, goUp, goDown,gameOver;
        string facing = "up";
        int playerHealth = 100;
        int speed = 10;
        int ammo = 10;
        int zombieSpeed = 3;
        int score;
        Random randNum= new Random();  
        List<PictureBox> zombiesList =new List<PictureBox>(); 
        public Form1()
        {
            InitializeComponent();
            RestartGame();
        }

        private void Pleayer_Click(object sender, EventArgs e)
        {

        }

        private void MainTimerEvent(object sender, EventArgs e)
        {
            //شرط طول ما الصحه اكبر من 1 اللعبه تشتغل اقل اللعبه تقف 
            if(playerHealth > 1)
            {
                healthBar.Value = playerHealth;
            }
            else
            {
                gameOver = true;
                player.Image = Properties.Resources.dead;
                GameTimer.Stop();
            }
            //وضع قيمه لعدد الاصبات وعدد الرصاص
            txtAmo.Text = "Ammo:" + ammo;
            txtScore.Text = "Kills:" + score;
            //شرط اذا كان الاعب يتحرك في الشمال ولم يخرج عن حدود الشاشه يتم اتجاه الاعب الي اليسار  
            if(goLeft == true && player.Left > 0)
            {
                player.Left -= speed;
            }
            //شرط اذا كان الاعب يتحرك في اليمين ولم يخرج عن حدود الشاشه يتم اتجاه الاعب الي اليمين   
            if (goRight==true && player.Left +player.Width <this.ClientSize.Width)
            {
                player.Left += speed;
            }
            //شرط اذا كان الاعب يتحرك في الاعلي  ولم يخرج عن حدود الشاشه يتم اتجاه الاعب الي الاعلي   
            if (goUp == true && player.Top > 34)
            {
                player.Top -= speed;    
            }
            //شرط اذا كان الاعب يتحرك في الاسفل ولم يخرج عن حدود الشاشه يتم اتجاه الاعب الي الاسفل  
            if (goDown == true && player.Top +player.Height < this.ClientSize.Height)
            {
                player.Top += speed;    
            }


            //هذه حلقه يتم فيها التأكد اذا كان صوره الرصاص تقع داخل الاعب سيقوم سيحذف صوره الرصاص وتذويد الرصاص الي 5 اذا كان 0
            foreach(Control x in this.Controls)
            {
                if(x is PictureBox && (string)x.Tag == "ammo")
                {
                    if (player.Bounds.IntersectsWith(x.Bounds))
                    {
                        this.Controls.Remove(x);
                        ((PictureBox)x).Dispose();
                        ammo += 5;
                    }
                }

                //شرط اذا كان الاكس هو الزومبي وتداخل في اللاعب سوف ينقص من الصحه 1
                if (x is PictureBox && (string)x.Tag == "zombie")
                {

                    if (player.Bounds.IntersectsWith(x.Bounds))
                    {
                        playerHealth -= 1;
                    }


                    // مجموعه شروط تجعل الزومبي يتحرك في اتجاه اللاعب 
                    if (x.Left > player.Left)
                    {
                        x.Left -= zombieSpeed;
                        ((PictureBox)x).Image = Properties.Resources.zleft ;
                    }
                    if (x.Left < player.Left)
                    {
                        x.Left += zombieSpeed;
                        ((PictureBox)x).Image = Properties.Resources.zright;
                    }


                    if (x.Top < player.Top)
                    {
                        x.Top += zombieSpeed;
                        ((PictureBox)x).Image = Properties.Resources.zdown;
                    }
                    if (x.Top > player.Top)
                    {
                        x.Top -= zombieSpeed;
                        ((PictureBox)x).Image = Properties.Resources.zup ;
                    }
                }


                //حلقه للوصول الي جميع عناصر التحكم وتخزين كل عنصر يتم  الوصول اليه داخل متعير j
                foreach(Control j in this.Controls)
                {
                    //شرط اذا كان (j) هو الرصاص و (x) هو الزومبي يقوم التحقق من الشرط التالي 
                    if(j is PictureBox && (string)j.Tag=="bullet" && x is PictureBox && (string)x.Tag == "zombie")
                    {
                        //الشرط هو اذا اصبت الرصاصه بالزومبي يقوم بزياده الاسكور 1 واخفاء الزومبي والرصاصه ثم ظهور زومبي اخر بدلا من الذي قتل
                        if (x.Bounds.IntersectsWith(j.Bounds))
                        {
                            score++;


                            this.Controls.Remove(j);
                            ((PictureBox)j).Dispose();
                            this.Controls.Remove(x);
                            ((PictureBox)x).Dispose();
                            zombiesList.Remove(((PictureBox)x));
                            MakeZombies();
                        }
                    }
                }

            }





        }

        private void KeyIsDown(object sender, KeyEventArgs e)
        {
            //شرط اذا انتهت اللعبه بموت اللاعب لايتم فعل اي شئ في الكود 
            if(gameOver == true)
            {
                return;
            }

            //مجموعه شرود هي عندما الضغط علي اتجاه معين فأنه يقوم بجلب صوره الاتجاه الذي نضغط عليه وتغير facing
            if(e.KeyCode == Keys.Left)
            {
                goLeft = true;
                facing = "left";
                player.Image = Properties.Resources.left;
            }
            if (e.KeyCode == Keys.Right)
            {
                goRight = true;
                facing = "right";
                player.Image = Properties.Resources.right;
            }
            if (e.KeyCode == Keys.Up)
            {
                goUp = true;
                facing = "up";
                player.Image = Properties.Resources.up;
            }
            if (e.KeyCode == Keys.Down)
            {
                goDown = true;
                facing = "down";
                player.Image = Properties.Resources.down;
            }

        }

        private void KeyIsUp(object sender, KeyEventArgs e)
        {
            //مجموعه من الشروط للتحقق من ان الضغط علي الاتجاه صحيح ام لا
            if (e.KeyCode == Keys.Left)
            {
                goLeft = false;

            }
            if (e.KeyCode == Keys.Right)
            {
                goRight = false;
            }
            if (e.KeyCode == Keys.Down)
            {
                goDown = false;

            }
            if (e.KeyCode == Keys.Up)
            {
                goUp = false;

            }

            //شرط عن الضغط علي المسطر وعدد الرصاص اكبر من 0 و لم تنتهي العبه 
            //يقوم بنقص الرصاص و جعل اتجاه الرصاص في اتجاه الذي حدده اللاعب
            if (e.KeyCode == Keys.Space && ammo > 0 && gameOver == false)
            {
                ammo--;
                ShootBulltet(facing);

                if(ammo < 1)
                {
                    DropAmmo();
                }

            }


            // شر اذا كانت اللعبه قد انتهت و عند اضغط علي زرار الانير يقوم بأعاده تشغيل اللعبه
            if(gameOver==true && e.KeyCode == Keys.Enter)
            {
                RestartGame();
            }
        }

        private void ShootBulltet(string direction)
        {
            Bullet ShootBulltet =new Bullet();
            ShootBulltet.direction = direction;
            ShootBulltet.bullteLeft = player.Left +(player.Width / 2);
            ShootBulltet.bulltTop = player.Top + (player.Height / 2);
            ShootBulltet.makeBullte(this);
        }

        private void MakeZombies()
        {
            PictureBox zombie = new PictureBox();
            zombie.Tag = "zombie";
            zombie.Image = Properties.Resources.zdown;
            zombie.Left = randNum.Next(0,900);
            zombie.Top = randNum.Next(0, 800);
            zombie.SizeMode = PictureBoxSizeMode.AutoSize;
            zombiesList.Add(zombie);
            this.Controls.Add(zombie);
            player.BringToFront();
        }

        private void DropAmmo()
        {
            PictureBox ammo = new PictureBox();
            ammo.Image = Properties.Resources.ammo_Image;
            ammo.SizeMode = PictureBoxSizeMode.AutoSize;
            ammo.Left = randNum.Next(10, this.ClientSize.Width - ammo.Width);
            ammo.Top = randNum.Next(10, this.ClientSize.Height - ammo.Height); ;
            ammo.Tag = "ammo";
            this.Controls.Add(ammo);

            ammo.BringToFront();
            player.BringToFront();


        }

        private void RestartGame()
        {

            player.Image = Properties.Resources.up;
            foreach(PictureBox i in zombiesList)
            {
                this.Controls.Remove(i);
            }
            zombiesList.Clear();
            for(int i =0; i < 3; i++)
            {
                MakeZombies();
            }
            goDown = false;
            goUp = false;
            goLeft = false;
            goRight = false;
            gameOver = false;

            playerHealth = 100;
            score = 0;
            ammo = 10;

            GameTimer.Start();

        }
    }
}
