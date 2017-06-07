using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DoAn_Game
{
    public partial class frmAdventure : Form
    {
        int time = 0;
        int gioihan = 444;
        int nextchar = 0;
        int nextWord = 0;
        int levels = 1;
        string[] nWord = new string[200];//so tu trong 1 bai.
        int tg = 0;//thoi gian cua bai tap.(tinh bang giay).
        string tgchuoi = "";//bieu dien thoi gian theo dang chuoi.
        int loi = 0;//dem so loi sai.
        int dem = 0;//dem so lan cho.
        int nLesson = 0;//so luong chu de cua level
        string content = "";//noi dung chu de

        List<CLASS.ChuDe> dsbaitap = new List<CLASS.ChuDe>();
        private CLASS.ChuDe classChuDe = new CLASS.ChuDe();

        //Function to get random number
        private static readonly Random getrandom = new Random();
        private static readonly object syncLock = new object();
        public static int GetRandomNumber(int min, int max)
        {
            lock (syncLock)
            { // synchronize
                return getrandom.Next(min, max);
            }
        }
        void PlaySound(string path)
        {
            byte[] result = System.IO.File.ReadAllBytes(path);

            System.IO.MemoryStream ms = new System.IO.MemoryStream(result);

            System.Media.SoundPlayer pl = new System.Media.SoundPlayer(ms);

            pl.Play();

        }
        void AntiFlicker()
        {
            SetStyle(ControlStyles.UserPaint, true);
            SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            SetStyle(ControlStyles.DoubleBuffer, true);
        }

        public frmAdventure()
        {
            InitializeComponent();
        }

        void PlayTypeRightSound(string s)
        {
            try
            {
                PlaySound(s);
            }
            catch { }

        }

        void PlayTypeWrongSound()
        {
            try
            {
                PlaySound("Sound/cached_wrong.wav");
            }
            catch { }
        }

        void doitaodo(RichTextBox a)
        {
            if (gioihan <= 0)
            {
                timer1.Stop();
                LoadLesson();
            }

            if (a.Location.Y <= gioihan - a.Size.Height)
            {

                Point newlocation = new Point();
                newlocation.X = a.Location.X;
                newlocation.Y = a.Location.Y + 2;
                a.Location = newlocation;
                pictureBox1.Location = new Point(newlocation.X - 53, newlocation.Y) ;
            }
            else
            {
                timer1.Stop();
                timer3.Stop();
                MessageBox.Show("Game Over!");
                ///////////////////////////////////////////////////////
                PictureBox p = new PictureBox();
                p.Name = Guid.NewGuid().ToString();
                p.Size = new Size(242, 38);
                p.ImageLocation = "images/tuongGach21.jpg";
                p.Location = new Point(-1, gioihan - a.Size.Height);
                //pnlColumn2.Controls.Add(p);
                PlayTypeRightSound("Sounds/cached_type.wav");

                if (nextWord == nWord.Length)
                {
                    timer1.Stop();
                    MessageBox.Show("lesson ended!!!");
                    LoadLesson();

                }
                else
                {
                    Point newlocation = new Point();
                    newlocation.X = 195;
                    newlocation.Y = -38;
                    a.Location = newlocation;
                    a.Text = nWord[nextWord];
                    nextWord++;
                    nextchar = 0;
                    a.SelectAll();
                    a.SelectionColor = Color.Black;
                    a.SelectionAlignment = HorizontalAlignment.Center;
                    timer1.Start();
                }
            }

        }
        private void timer1_Tick(object sender, EventArgs e)
        {
            doitaodo(lblText);
        }

        void LoadLesson()
        {
            timer1.Stop();
            timer3.Stop();
            // timer2.Stop();

            time = 0;
            gioihan = 444;
            nextchar = 0;
            nextWord = 0;
            int so = 0;

            do
            {
                levels++;
                if (levels > 4)
                        break;
                    dsbaitap = classChuDe.GetLessonListByLevel(levels);
                    so = dsbaitap.Count;
                }
            while (so == 0);
        }


        string countTime()
        {
            int mi = tg / 60;
            int se = tg % 60;
            string chuoi = mi.ToString() + ":" + se.ToString();
            return chuoi;
        }

        private void frmAdventure_KeyPress(object sender, KeyPressEventArgs e)
        {
            lblStart.Visible = false;
            timer3.Start();
            timer1.Start();

                if (e.KeyChar == lblText.Text[nextchar])
                {
                    PlayTypeRightSound("Sounds/cached_type.wav");
                    int n = nextchar + 1;
                    lblText.Select(0, n);
                    lblText.SelectionColor = Color.Blue;
                    lblText.SelectionFont = new Font(lblText.Font, FontStyle.Regular);
                    lblText.Select(n, 1);
                    lblText.SelectionFont = new Font(lblText.Font, FontStyle.Underline);
                    nextchar++;

                    if (nextchar < lblText.Text.Length)
                    {
                    }
                    else
                    {
                        timer1.Stop();
                        int toadoY = lblText.Location.Y;
                        timer2.Start();
                    }
                }
                else
                {
                    if (nextchar < lblText.Text.Length)
                    {

                        PlayTypeWrongSound();
                    }
                }
        }

        private void timer2_Tick(object sender, EventArgs e)
        {

            dem++;
            if (dem == 3)
            {
                PlayTypeRightSound("Sounds/BreakEgg.wav");
                //pnlColumn2.Controls.RemoveByKey("temp");
                if (nextWord == nWord.Length)
                {
                    nextchar = nextchar - 1;

                    timer3.Stop();
                    timer1.Stop();
                }
                else
                {
                    lblText.Text = nWord[nextWord];
                    lblText.Location = new Point(GetRandomNumber(0,600), -36);
                    lblText.SelectAll();
                    lblText.SelectionAlignment = HorizontalAlignment.Center;
                    lblText.SelectionColor = Color.Black;
                    nextWord++;
                    nextchar = 0;
                    timer1.Start();
                }
                timer2.Stop();
                dem = 0;

            }
        }


        private void timer3_Tick(object sender, EventArgs e)
        {
            time += 1;
            if (tg == 0)
            {
                timer3.Stop();
                timer2.Stop();
                timer1.Stop();
                LoadLesson();
            }
            else
            {
                tg--;
                tgchuoi = countTime();
                lblTime.Text = tgchuoi;
            }
        }

        private void richBrick_Enter(object sender, EventArgs e)
        {
            button1.Focus();
        }

        private void frmAdventure_MouseLeave(object sender, EventArgs e)
        {
            timer1.Stop();
            timer3.Stop();
        }

        private void frmAdventure_Load(object sender, EventArgs e)
        {
            lblStart.BackColor = System.Drawing.Color.Transparent;
            pictureBox1.BackColor = System.Drawing.Color.Transparent;
            lblText.Location = new Point(GetRandomNumber(195, 600), -100);
            pictureBox1.Location = new Point(lblText.Location.X - 195, -100);
            AntiFlicker();
            timer1.Stop();
            timer3.Stop();
            timer2.Stop();

                dsbaitap = classChuDe.GetLessonListByLevel(levels);
                nLesson = dsbaitap.Count;

                ////////////////////////////////////////////////////////////////////////
                if (nLesson > 0)
                {

                    content = dsbaitap[0].NoiDung;
                    nWord = content.Split(' ');
                    int sotu = 0;
                    for (int i = 0; i < nWord.Length; i++)
                    {
                        sotu += nWord[i].Length;
                    }
                    lblLessonName.Text = dsbaitap[0].TenChuDe;
                    lblText.Text = nWord[nextWord];

                    lblText.Select(0, nextchar);
                    lblText.SelectionColor = Color.Blue;
                    lblText.SelectionAlignment = HorizontalAlignment.Center;
                    rtxtDescription.Text = dsbaitap[0].MoTa;
                    tg = dsbaitap[0].ThoiGian * 60;
                    tgchuoi = countTime();
                    lblTime.Text = tgchuoi;
                    nextWord++;

                }
                else
                {
                    MessageBox.Show("Do not find exersices.");
                    this.Close();
                }
        }
    }
}
