using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Media;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Timer = System.Windows.Forms.Timer;

namespace SnowFallMidili
{
    public partial class Form1 : Form
    {
        private readonly IList<Snow> snowflakes;
        private readonly Timer timer;
        private readonly Graphics buffer;
        private readonly Bitmap fonFile;
        private readonly Bitmap imgSnow;
        private readonly Bitmap fon;
        private readonly Bitmap mb;
        private readonly int speed = 5;
        public Form1()
        {
            InitializeComponent();
            snowflakes = new List<Snow>();
            fonFile = new Bitmap("fon.jpg");
            fon = new Bitmap(fonFile, Screen.PrimaryScreen.WorkingArea.Width,
                Screen.PrimaryScreen.WorkingArea.Height);
            imgSnow = new Bitmap("snow.png");
            mb = new Bitmap(Screen.PrimaryScreen.WorkingArea.Width,
                Screen.PrimaryScreen.WorkingArea.Height);
            buffer = Graphics.FromImage(mb);
            CreateSnow();
            timer = new Timer
            {
                Interval = 1
            };
            timer.Tick += Step;
        }
        private void Form1_Click(object sender, EventArgs e)
        {
            if (timer.Enabled)
            {
                timer.Stop();
            }
            else
            {
                timer.Start();
            }
        }
        private void Step(object sender, EventArgs e)
        {
            timer.Stop();
            for (var i = 0; i < snowflakes.Count; i++)
            {

                if (snowflakes[i].Y > Screen.PrimaryScreen.WorkingArea.Height)
                {
                    snowflakes[i].Y = -snowflakes[i].Sev;
                }
                else
                {
                    snowflakes[i].Y += speed + snowflakes[i].Sev;
                }
            }
            Draw();
            timer.Start();
        }

        private void CreateSnow()
        {
            var rnd = new Random();
            for (var i = 0; i < 100; i++)
            {
                snowflakes.Add(new Snow
                {
                    X = rnd.Next(Screen.PrimaryScreen.WorkingArea.Width),
                    Y = -rnd.Next(Screen.PrimaryScreen.WorkingArea.Height),
                    Sev = rnd.Next(10, 40)
                });
            }
        }

        private void Draw()
        {
            buffer.DrawImage(fon, new Rectangle(0, 0,
            Screen.PrimaryScreen.WorkingArea.Width,
            Screen.PrimaryScreen.WorkingArea.Height));
            for (var i = 0; i < snowflakes.Count; i++)
            {
                if (snowflakes[i].Y > 0)
                {
                    buffer.DrawImage(imgSnow, new Rectangle(
                        snowflakes[i].X,
                        snowflakes[i].Y,
                        snowflakes[i].Sev,
                        snowflakes[i].Sev));
                }
            }
            var g = CreateGraphics();
            g.DrawImage(mb, 0, 0);
        }
        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            Draw();
        }

    }
}
