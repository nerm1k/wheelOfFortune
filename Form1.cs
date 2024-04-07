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

        private int currentPlayerIndex = 0;

        //private int bet_x1 = 0;
        //private int bet_x2 = 0;
        //private int bet_x5 = 0;
        //private int bet_x10 = 0;
        //private int bet_x20 = 0;
        //private int bet_x40 = 0;

        //private Dictionary<int, int> bets;



        public Form1()
        {
            InitializeComponent();
        }

        public class WheelOfFortune
        {
            public Bitmap wheelPic;
            public Bitmap tempWheelPic;
            public float angle;
            public int[] states;
            public int state;
            public int playersCount;
            public List<Player> playersList;
            public int turnsCount;
            //List<Player> playersList = new List<Player>();

            public WheelOfFortune(int playersCount, int turnsCount)
            {
                tempWheelPic = new Bitmap(Properties.Resources.wheel);
                wheelPic = new Bitmap(Properties.Resources.wheel);
                states = new int[] { 1, 2, 20, 1, 5, 2, 1, 10, 1, 2, 1, 5, 1, 40, 1, 2, 1, 2, 1, 5, 1, 10, 1, 2, 1, 2, 1, 5, 1, 20, 1, 2, 1, 2, 1, 10, 2, 5, 1, 2, 40, 2, 1, 2, 1, 5, 1, 2, 1, 10, 1, 5, 1, 2 };
                angle = 0.0f;
                this.playersCount = playersCount;
                this.turnsCount = turnsCount;
                playersList = new List<Player>();
                for (int i = 0; i < this.playersCount; i++)
                {
                    Player player = new Player();
                    playersList.Add(player);
                }
                //Player playerWithId1 = playersList.Find(p => p.id == 1);
                //playerWithId1.isActive = true;
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

        public class Player
        {
            public static int nextId = 1;

            public int id { get; }
            public int balance { get; set; }
            //public bool isActive { get; set; }

            //public int bet_x1 = 0;
            //public int bet_x2 = 0;
            //public int bet_x5 = 0;
            //public int bet_x10 = 0;
            //public int bet_x20 = 0;
            //public int bet_x40 = 0;

            public Dictionary<int, int> bets;

            public Player()
            {
                id = nextId;
                nextId++;
                balance = 1000;
                bets = new Dictionary<int, int>{
                    { 1, 0},
                    { 2, 0},
                    { 5, 0},
                    { 10, 0},
                    { 20, 0},
                    { 40, 0}
                };
                //isActive = false;
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


                label1.Text = "Победное число: " + Convert.ToString(wheel.states[wheel.state]);
            }
            else
            {
                wheelTimer.Stop();
                wheelIsMoved = false;
                wheel.turnsCount--;

                //Player currentPlayer = wheel.playersList[0];
                //label11.Text = "ставка на x2: " + currentPlayer.bets[2].ToString();

                int result = wheel.states[wheel.state];

                label8.Text = "Выиграл сектор: " + result;

                foreach (Player player in wheel.playersList)
                {
                    foreach(var bet in player.bets)
                    {
                        if(bet.Key == result)
                        {
                            player.balance += bet.Value * result;
                        }
                    }
                    label10.Text += "id: " + player.id + " .money: " + player.balance.ToString() + "\n";
                    foreach (var key in player.bets.Keys.ToList())
                    {
                        player.bets[key] = 0;
                    }
                }

                if (wheel.turnsCount == 0)
                {
                    int winnerId = -1;
                    int winnerBalance = -1;

                    foreach (Player player in wheel.playersList)
                    {
                        if (player.balance > winnerBalance)
                        {
                            winnerId = player.id;
                            winnerBalance = player.balance;
                        }
                    }
                
                    label14.Text = $"winner: {winnerId}\n. balance: {winnerBalance}\n. test. ИГРА ОКОНЧЕНА";

                    button10.Visible = true;

                    label1.Visible = false;
                    label2.Visible = false;
                    label3.Visible = false;
                    label4.Visible = false;
                    label5.Visible = false;
                    label6.Visible = false;
                    label7.Visible = false;
                    label8.Visible = false;
                    label11.Visible = false;
                    label12.Visible = false;
                    textBox1.Visible = false;
                    button1.Visible = false;
                    button2.Visible = false;
                    button3.Visible = false;
                    button4.Visible = false;
                    button5.Visible = false;
                    button6.Visible = false;
                    button7.Visible = false;
                    button9.Visible = false;
                    pictureBox1.Visible = false;
                    pictureBox2.Visible = false;

                    return;
                }

                //Player currentPlayer = wheel.playersList[0];

                //foreach(var bet in bets)
                //{
                //    if (bet.Key == result)
                //    {
                //        label8.Text = "Вы выиграли: " + bet.Value * result;
                //    }
                //}

                //bet_x1 = 0;
                //bet_x2 = 0;
                //bet_x5 = 0;
                //bet_x10 = 0;
                //bet_x20 = 0;
                //bet_x40 = 0;

                label2.Text = "Ставка на х1: ";
                label3.Text = "Ставка на х2: ";
                label4.Text = "Ставка на х5: ";
                label5.Text = "Ставка на х10: ";
                label6.Text = "Ставка на х20: ";
                label7.Text = "Ставка на х40: ";

                currentPlayerIndex = 0;

                button1.Visible = false;
                label2.Visible = true;
                label3.Visible = true;
                label4.Visible = true;
                label5.Visible = true;
                label6.Visible = true;
                label7.Visible = true;
                textBox1.Visible = true;
                button2.Visible = true;
                button3.Visible = true;
                button4.Visible = true;
                button5.Visible = true;
                button6.Visible = true;
                button7.Visible = true;
                button9.Visible = true;


                //bets.Clear();
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
            //bets = new Dictionary<int, int>
            //{
            //    {1, bet_x1},
            //    {2, bet_x2},
            //    {5, bet_x5},
            //    {10, bet_x10},
            //    {20, bet_x20},
            //    {40, bet_x40}
            //};
        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            char button = e.KeyChar;

            if (!Char.IsDigit(button) && button != (char)8)
            {
                e.Handled = true;
            }
        }

        //private void textBox2_KeyPress(object sender, KeyPressEventArgs e)
        //{
        //    char button = e.KeyChar;

        //    if (!Char.IsDigit(button) && button != (char)8)
        //    {
        //        e.Handled = true;
        //    }
        //}

        //private void textBox3_KeyPress(object sender, KeyPressEventArgs e)
        //{
        //    char button = e.KeyChar;

        //    if (!Char.IsDigit(button) && button != (char)8)
        //    {
        //        e.Handled = true;
        //    }
        //}

        private void button2_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(textBox1.Text))
            {
                Player currentPlayer = wheel.playersList[currentPlayerIndex];
                int bet = int.Parse(textBox1.Text);
                if (currentPlayer.balance < bet)
                {
                    label12.Text = "Недостаточно денег на балансе";
                    return;
                }
                currentPlayer.bets[1] += bet;
                currentPlayer.balance -= bet;
                label2.Text = "Ставка на х1: " + currentPlayer.bets[1];
                textBox1.Clear();
                label12.Text = "";
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
                Player currentPlayer = wheel.playersList[currentPlayerIndex];
                int bet = int.Parse(textBox1.Text);
                if (currentPlayer.balance < bet)
                {
                    label12.Text = "Недостаточно денег на балансе";
                    return;
                }  
                currentPlayer.bets[2] += bet;
                currentPlayer.balance -= bet;
                label3.Text = "Ставка на х2: " + currentPlayer.bets[2];
                textBox1.Clear();
                label12.Text = "";
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
                Player currentPlayer = wheel.playersList[currentPlayerIndex];
                int bet = int.Parse(textBox1.Text);
                if (currentPlayer.balance < bet)
                {
                    label12.Text = "Недостаточно денег на балансе";
                    return;
                }
                currentPlayer.bets[5] += bet;
                currentPlayer.balance -= bet;
                label4.Text = "Ставка на х5: " + currentPlayer.bets[5];
                textBox1.Clear();
                label12.Text = "";
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
                Player currentPlayer = wheel.playersList[currentPlayerIndex];
                int bet = int.Parse(textBox1.Text);
                if (currentPlayer.balance < bet)
                {
                    label12.Text = "Недостаточно денег на балансе";
                    return;
                }
                currentPlayer.bets[10] += bet;
                currentPlayer.balance -= bet;
                label5.Text = "Ставка на х10: " + currentPlayer.bets[10];
                textBox1.Clear();
                label12.Text = "";
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
                Player currentPlayer = wheel.playersList[currentPlayerIndex];
                int bet = int.Parse(textBox1.Text);
                if (currentPlayer.balance < bet)
                {
                    label12.Text = "Недостаточно денег на балансе";
                    return;
                }
                currentPlayer.bets[20] += bet;
                currentPlayer.balance -= bet;
                label6.Text = "Ставка на х20: " + currentPlayer.bets[20];
                textBox1.Clear();
                label12.Text = "";
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
                Player currentPlayer = wheel.playersList[currentPlayerIndex];
                int bet = int.Parse(textBox1.Text);
                if (currentPlayer.balance < bet)
                {
                    label12.Text = "Недостаточно денег на балансе";
                    return;
                }
                currentPlayer.bets[40] += bet;
                currentPlayer.balance -= bet;
                label7.Text = "Ставка на х40: " + currentPlayer.bets[40];
                textBox1.Clear();
                label12.Text = "";
            }
            else
            {
                MessageBox.Show("Please enter a number in the TextBox.");
            }
        }

        private void button8_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(textBox2.Text) && !string.IsNullOrEmpty(textBox3.Text))
            {
                int playersCount = int.Parse(textBox2.Text);
                int turnsCount = int.Parse(textBox3.Text);

                wheelTimer = new Timer();
                wheelTimer.Interval = 50; // как часто вызывается метод ниже
                wheelTimer.Tick += wheelTimer_Tick;
                wheel = new WheelOfFortune(playersCount, turnsCount);

                label9.Visible = false;
                label13.Visible = false;
                textBox2.Visible = false;
                textBox3.Visible = false;
                button8.Visible = false;

                label1.Visible = true;
                label2.Visible = true;
                label3.Visible = true;
                label4.Visible = true;
                label5.Visible = true;
                label6.Visible = true;
                label7.Visible = true;
                label8.Visible = true;
                label10.Visible = true;
                label11.Visible = true;
                label12.Visible = true;
                label14.Visible = true;
                textBox1.Visible = true;
                button2.Visible = true;
                button3.Visible = true;
                button4.Visible = true;
                button5.Visible = true;
                button6.Visible = true;
                button7.Visible = true;
                button9.Visible = true;
                pictureBox1.Visible = true;
                pictureBox2.Visible = true;
            }
        }

        private void button9_Click(object sender, EventArgs e)
        {
            //Player currentPlayer = wheel.playersList[currentPlayerIndex];
            //currentPlayer.bets = new Dictionary<int, int>{
            //    { 1, currentPlayer.bet_x1},
            //    { 2, currentPlayer.bet_x2},
            //    { 5, currentPlayer.bet_x5},
            //    { 10, currentPlayer.bet_x10},
            //    { 20, currentPlayer.bet_x20},
            //    { 40, currentPlayer.bet_x40}
            //};
            currentPlayerIndex++;
            if (currentPlayerIndex == wheel.playersCount){
                button1.Visible = true;
                label2.Visible = false;
                label3.Visible = false;
                label4.Visible = false;
                label5.Visible = false;
                label6.Visible = false;
                label7.Visible = false;
                textBox1.Visible = false;
                button2.Visible = false;
                button3.Visible = false;
                button4.Visible = false;
                button5.Visible = false;
                button6.Visible = false;
                button7.Visible = false;
                button9.Visible = false;
            };
            textBox1.Clear();
            label2.Text = "Ставка на х1: ";
            label3.Text = "Ставка на х2: ";
            label4.Text = "Ставка на х5: ";
            label5.Text = "Ставка на х10: ";
            label6.Text = "Ставка на х20: ";
            label7.Text = "Ставка на х40: ";
        }

        private void button10_Click(object sender, EventArgs e)
        {
            Form newForm = new Form1(); 

            this.Hide();

            newForm.Show();


            //ИЛИ
            //Application.Restart();
        }
    }

}