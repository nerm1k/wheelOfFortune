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
    public class WheelOfFortune
    {
        private readonly Form1 form;
        private readonly Bitmap wheelPic;
        private readonly int[] states;
        private readonly Timer wheelTimer;
        private int angle;
        private int state;
        private int turnsCount;
        private int numberOfTicks;

        public readonly List<IPlayer> players;
        public int currentPlayerIndex = 0;

        public WheelOfFortune(Form1 form, int playersCount, int turnsCount)
        {
            this.form = form;
            wheelPic = new Bitmap(Properties.Resources.wheelv2);
            states = new int[] { 1, 2, 20, 1, 5, 2, 1, 10, 1, 2, 1, 5, 1, 40, 1, 2, 1, 2, 1, 5, 1, 10, 1, 2, 1, 2, 1, 5, 1, 20, 1, 2, 1, 2, 1, 10, 2, 5, 1, 2, 40, 2, 1, 2, 1, 5, 1, 2, 1, 10, 1, 5, 1, 2 };
            angle = 0;
            this.turnsCount = turnsCount;
            players = new List<IPlayer>();
            for (int i = 0; i < playersCount; i++)
            {
                IPlayer player = new Player();
                players.Add(player);
            }
            wheelTimer = new Timer();
            wheelTimer.Interval = 40;
            wheelTimer.Tick += WheelTimerTick;
        }

        private Bitmap RotateImage(Bitmap image, int angle)
        {
            Bitmap rotatedImage = new Bitmap(image.Width, image.Height);
            rotatedImage.SetResolution(image.HorizontalResolution, image.VerticalResolution);
            Graphics g = Graphics.FromImage(rotatedImage);

            PointF offset = new PointF((float)image.Width / 2, (float)image.Height / 2);

            g.TranslateTransform(offset.X, offset.Y);
            g.RotateTransform(angle);
            g.TranslateTransform(-offset.X, -offset.Y);
            g.DrawImage(image, new PointF(0, 0));

            return rotatedImage;
        }

        private void WheelTimerTick(object sender, EventArgs e)
        {
            if (numberOfTicks > 0)
            {
                angle += numberOfTicks / 10;
                angle = angle % 360;
                Bitmap rotatedImage = RotateImage(wheelPic, angle);
                form.UpdateWheelPic(rotatedImage);
                numberOfTicks--;
            }
            else
            {
                StopTurning();

                state = Convert.ToInt32(Math.Ceiling(angle / 6.666666666666667));

                if (state != 0)
                {
                    state -= 1;
                }

                GetResults();
            }
        }

        public void StartTurning()
        {
            Random rand = new Random();
            numberOfTicks = rand.Next(70, 130);
            wheelTimer.Start();
        }

        private void StopTurning()
        {
            wheelTimer.Stop();
        }

        private void EndGame()
        {
            form.PrepareNewGameUI();

            GetWinner();
        }

        private void GetWinner()
        {
            IPlayer winner = null;
            int winnerBalance = -1;

            foreach (IPlayer player in players)
            {
                form.ShowResults(player);

                if (player.balance > winnerBalance)
                {
                    winnerBalance = player.balance;
                    winner = player;
                }
            }

            form.ShowWinner(winner);
        }

        private void GetResults()
        {
            turnsCount--;

            int result = states[state];

            form.ShowWinningSector(result);

            foreach (IPlayer player in players)
            {
                int prize = player.CalculatePrize(result);
                form.ShowPrizes(player.id, prize);
            }

            if (turnsCount == 0)
            {
                EndGame();

                return;
            }

            form.PrepareNewTurnUI();

            currentPlayerIndex = 0;
            form.ShowPlayerAndBalance(players[currentPlayerIndex]);
        }
    }
}
