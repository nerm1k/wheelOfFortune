using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Resources;


namespace wheelOfFortune
{
    public partial class Form1 : Form
    {
        private bool wheelIsMoved;
        private float wheelTimes;
        private Timer wheelTimer;
        private WheelOfFortune wheel;

        private int bet_x1 = 0;
        private int bet_x2 = 0;
        private int bet_x5 = 0;
        private int bet_x10 = 0;
        private int bet_x20 = 0;
        private int bet_x40 = 0;

        private Dictionary<int, int> bets;


        public Form1()
        {
            InitializeComponent();
            wheelTimer = new Timer();
            wheelTimer.Interval = 50; // как часто вызывается метод ниже
            wheelTimer.Tick += wheelTimer_Tick;
            wheel = new WheelOfFortune();

        }

        public class WheelOfFortune
        {
            public Bitmap wheelPic;
            public Bitmap tempWheelPic;
            public float angle;
            public int[] states;
            public int state;

            public WheelOfFortune()
            {
                tempWheelPic = new Bitmap(Properties.Resources.wheel);
                wheelPic = new Bitmap(Properties.Resources.wheel);
                states = new int[] { 1, 2, 20, 1, 5, 2, 1, 10, 1, 2, 1, 5, 1, 40, 1, 2, 1, 2, 1, 5, 1, 10, 1, 2, 1, 2, 1, 5, 1, 20, 1, 2, 1, 2, 1, 10, 2, 5, 1, 2, 40, 2, 1, 2, 1, 5, 1, 2, 1, 10, 1, 5, 1, 2 };
                angle = 0.0f;
            }

            public Bitmap RotateImage(Image image, float angle, PointF offset = default(PointF))
            {
                if (image == null)
                {
                    throw new ArgumentNullException("image");
                }

                Bitmap rotatedBmp = new Bitmap(image.Width, image.Height);
                rotatedBmp.SetResolution(image.HorizontalResolution, image.VerticalResolution);
                Graphics g = Graphics.FromImage(rotatedBmp);

                if (offset == default(PointF))
                {
                    offset = new PointF((float)image.Width / 2, (float)image.Height / 2);
                }

                g.TranslateTransform(offset.X, offset.Y);
                g.RotateTransform(angle);
                g.TranslateTransform(-offset.X, -offset.Y);
                g.DrawImage(image, new PointF(0, 0));

                return rotatedBmp;
            }
        }


        private void wheelTimer_Tick(object sender, EventArgs e)
        {
            if (wheelIsMoved && wheelTimes > 0)
            {
                wheel.angle += wheelTimes / 10;
                wheel.angle = wheel.angle % 360;
                Bitmap rotatedImage = wheel.RotateImage(wheel.wheelPic, wheel.angle);
                pictureBox1.Image = rotatedImage;
                wheelTimes--;

                wheel.state = Convert.ToInt32(Math.Ceiling(wheel.angle / 6.666666666666667));

                if (wheel.state == 0)
                {
                    wheel.state = 0;
                }
                else
                {
                    wheel.state -= 1;
                }


                label1.Text = Convert.ToString(wheel.states[wheel.state]);
            }
            else
            {
                wheelIsMoved = false;
                int result = wheel.states[wheel.state];

                foreach(var bet in bets)
                {
                    if (bet.Key == result)
                    {
                        label8.Text = "Вы выиграли: " + bet.Value * result;
                    }
                }

                bet_x1 = 0;
                bet_x2 = 0;
                bet_x5 = 0;
                bet_x10 = 0;
                bet_x20 = 0;
                bet_x40 = 0;

                label2.Text = "Ставка на х1: ";
                label3.Text = "Ставка на х2: ";
                label4.Text = "Ставка на х5: ";
                label5.Text = "Ставка на х10: ";
                label6.Text = "Ставка на х20: ";
                label7.Text = "Ставка на х40: ";

                bets.Clear();
            }

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            wheelIsMoved = true;
            Random rand = new Random();
            wheelTimes = rand.Next(50, 100); // кол-во кручений
            wheelTimer.Start();
            bets = new Dictionary<int, int>
            {
                {1, bet_x1},
                {2, bet_x2},
                {5, bet_x5},
                {10, bet_x10},
                {20, bet_x20},
                {40, bet_x40}
            };
        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            char button = e.KeyChar;

            if (!Char.IsDigit(button) && button != (char)8)
            {
                e.Handled = true;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(textBox1.Text))
            {
                bet_x1 += int.Parse(textBox1.Text);
                label2.Text = "Ставка на х1: " + bet_x1;
                textBox1.Clear();
            }
            else
            {
                MessageBox.Show("Please enter a number in the TextBox.");
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(textBox1.Text))
            {
                bet_x2 += int.Parse(textBox1.Text);
                label3.Text = "Ставка на х2: " + bet_x2;
                textBox1.Clear();
            }
            else
            {
                MessageBox.Show("Please enter a number in the TextBox.");
            }
        }

      
        private void button4_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(textBox1.Text))
            {
                bet_x5 += int.Parse(textBox1.Text);
                label4.Text = "Ставка на х5: " + bet_x5;
                textBox1.Clear();
            }
            else
            {
                MessageBox.Show("Please enter a number in the TextBox.");
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(textBox1.Text))
            {
                bet_x10 += int.Parse(textBox1.Text);
                label5.Text = "Ставка на х10: " + bet_x10;
                textBox1.Clear();
            }
            else
            {
                MessageBox.Show("Please enter a number in the TextBox.");
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(textBox1.Text))
            {
                bet_x20 += int.Parse(textBox1.Text);
                label6.Text = "Ставка на х20: " + bet_x20;
                textBox1.Clear();
            }
            else
            {
                MessageBox.Show("Please enter a number in the TextBox.");
            }
        }

        private void button7_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(textBox1.Text))
            {
                bet_x40 += int.Parse(textBox1.Text);
                label7.Text = "Ставка на х40: " + bet_x40;
                textBox1.Clear();
            }
            else
            {
                MessageBox.Show("Please enter a number in the TextBox.");
            }
        }
    }

}