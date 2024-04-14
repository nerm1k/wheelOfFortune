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

        private WheelOfFortune wheel;

        public Form1()
        {
            InitializeComponent();
        }

        private void StartTurn(object sender, EventArgs e)
        {
            wheel.wheelIsMoved = true;
            Random rand = new Random();
            wheel.numberOfTwists = rand.Next(50, 100); // кол-во кручений
            wheel.wheelTimer.Start();
        }

        private void AllowOnlyDigits(object sender, KeyPressEventArgs e)
        {
            char button = e.KeyChar;

            if (!Char.IsDigit(button) && button != (char)8)
            {
                e.Handled = true;
            }
        }

        private void MakeBetX1(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(textBoxBet.Text))
            {
                Player currentPlayer = wheel.playersList[wheel.currentPlayerIndex];
                int bet = int.Parse(textBoxBet.Text);
                if (!currentPlayer.CanMakeBet(bet))
                {
                    labelNotEnoughBalance.Visible = true;
                    return;
                }
                currentPlayer.MakeBet(1, bet);
                labelBetX1.Text = "Ставка на х1: " + currentPlayer.bets[1];
                labelBalance.Text = $"Баланс: {currentPlayer.balance}";
                labelCurrentPlayer.Text = $"Игрок №{currentPlayer.id}";
                textBoxBet.Clear();
                labelNotEnoughBalance.Visible = false;
            }
        }

        private void MakeBetX2(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(textBoxBet.Text))
            {
                Player currentPlayer = wheel.playersList[wheel.currentPlayerIndex];
                int bet = int.Parse(textBoxBet.Text);
                if (!currentPlayer.CanMakeBet(bet))
                {
                    labelNotEnoughBalance.Visible = true;
                    return;
                }
                currentPlayer.MakeBet(2, bet);
                labelBetX2.Text = "Ставка на х2: " + currentPlayer.bets[2];
                labelBalance.Text = $"Баланс: {currentPlayer.balance}";
                labelCurrentPlayer.Text = $"Игрок №{currentPlayer.id}";
                textBoxBet.Clear();
                labelNotEnoughBalance.Visible = false;
            }
        }

      
        private void MakeBetX5(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(textBoxBet.Text))
            {
                Player currentPlayer = wheel.playersList[wheel.currentPlayerIndex];
                int bet = int.Parse(textBoxBet.Text);
                if (!currentPlayer.CanMakeBet(bet))
                {
                    labelNotEnoughBalance.Visible = true;
                    return;
                }
                currentPlayer.MakeBet(5, bet);
                labelBetX5.Text = "Ставка на х5: " + currentPlayer.bets[5];
                labelBalance.Text = $"Баланс: {currentPlayer.balance}";
                labelCurrentPlayer.Text = $"Игрок №{currentPlayer.id}";
                textBoxBet.Clear();
                labelNotEnoughBalance.Visible = false;
            }
        }

        private void MakeBetX10(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(textBoxBet.Text))
            {
                Player currentPlayer = wheel.playersList[wheel.currentPlayerIndex];
                int bet = int.Parse(textBoxBet.Text);
                if (!currentPlayer.CanMakeBet(bet))
                {
                    labelNotEnoughBalance.Visible = true;
                    return;
                }
                currentPlayer.MakeBet(10, bet);
                labelBetX10.Text = "Ставка на х10: " + currentPlayer.bets[10];
                labelBalance.Text = $"Баланс: {currentPlayer.balance}";
                labelCurrentPlayer.Text = $"Игрок №{currentPlayer.id}";
                textBoxBet.Clear();
                labelNotEnoughBalance.Visible = false;
            }
        }

        private void MakeBetX20(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(textBoxBet.Text))
            {
                Player currentPlayer = wheel.playersList[wheel.currentPlayerIndex];
                int bet = int.Parse(textBoxBet.Text);
                if (!currentPlayer.CanMakeBet(bet))
                {
                    labelNotEnoughBalance.Visible = true;
                    return;
                }
                currentPlayer.MakeBet(20, bet);
                labelBetX20.Text = "Ставка на х20: " + currentPlayer.bets[20];
                labelBalance.Text = $"Баланс: {currentPlayer.balance}";
                labelCurrentPlayer.Text = $"Игрок №{currentPlayer.id}";
                textBoxBet.Clear();
                labelNotEnoughBalance.Visible = false;
            }
        }

        private void MakeBetX40(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(textBoxBet.Text))
            {
                Player currentPlayer = wheel.playersList[wheel.currentPlayerIndex];
                int bet = int.Parse(textBoxBet.Text);
                if (!currentPlayer.CanMakeBet(bet))
                {
                    labelNotEnoughBalance.Visible = true;
                    return;
                }
                currentPlayer.MakeBet(40, bet);
                labelBetX40.Text = "Ставка на х40: " + currentPlayer.bets[40];
                labelBalance.Text = $"Баланс: {currentPlayer.balance}";
                labelCurrentPlayer.Text = $"Игрок №{currentPlayer.id}";
                textBoxBet.Clear();
                labelNotEnoughBalance.Visible = false;
            }
        }

        private void StartGame(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(textBoxPlayers.Text) && !string.IsNullOrEmpty(textBoxTurns.Text))
            {
                int playersCount = int.Parse(textBoxPlayers.Text);
                int turnsCount = int.Parse(textBoxTurns.Text);

                wheel = new WheelOfFortune(this, playersCount, turnsCount);

                Player currentPlayer = wheel.playersList[0];

                labelPlayers.Visible = false;
                labelTurns.Visible = false;
                textBoxPlayers.Visible = false;
                textBoxTurns.Visible = false;
                buttonStartGame.Visible = false;

                labelBetX1.Visible = true;
                labelBetX2.Visible = true;
                labelBetX5.Visible = true;
                labelBetX10.Visible = true;
                labelBetX20.Visible = true;
                labelBetX40.Visible = true;
                labelBalance.Visible = true;
                labelBalance.Text = $"Баланс: {currentPlayer.balance}";
                labelBet.Visible = true;
                labelCurrentPlayer.Visible = true;
                labelCurrentPlayer.Text = $"Игрок №{currentPlayer.id}";
                textBoxBet.Visible = true;
                buttonBetX1.Visible = true;
                buttonBetX2.Visible = true;
                buttonBetX5.Visible = true;
                buttonBetX10.Visible = true;
                buttonBetX20.Visible = true;
                buttonBetX40.Visible = true;
                buttonPassTheTurn.Visible = true;
                pictureBoxWheel.Visible = true;
                pictureBoxArrow.Visible = true;
            }
        }

        private void PassTheTurn(object sender, EventArgs e)
        {
            labelNotEnoughBalance.Visible = false;
            if (wheel.currentPlayerIndex + 1 == wheel.playersList.Count){
                buttonStartTurn.Visible = true;

                labelWinningSector.Visible = false;
                labelBetX1.Visible = false;
                labelBetX2.Visible = false;
                labelBetX5.Visible = false;
                labelBetX10.Visible = false;
                labelBetX20.Visible = false;
                labelBetX40.Visible = false;
                labelBalance.Visible = false;
                labelPrizes.Visible = false;
                labelCurrentPlayer.Visible = false;
                labelBet.Visible = false;
                textBoxBet.Visible = false;
                buttonBetX1.Visible = false;
                buttonBetX2.Visible = false;
                buttonBetX5.Visible = false;
                buttonBetX10.Visible = false;
                buttonBetX20.Visible = false;
                buttonBetX40.Visible = false;
                buttonPassTheTurn.Visible = false;           
                return;
            };
            wheel.currentPlayerIndex++;
            Player currentPlayer = wheel.playersList[wheel.currentPlayerIndex];
            textBoxBet.Clear();
            labelBetX1.Text = "Ставка на х1: ";
            labelBetX2.Text = "Ставка на х2: ";
            labelBetX5.Text = "Ставка на х5: ";
            labelBetX10.Text = "Ставка на х10: ";
            labelBetX20.Text = "Ставка на х20: ";
            labelBetX40.Text = "Ставка на х40: ";
            labelBalance.Text = $"Баланс: {currentPlayer.balance}";
            labelCurrentPlayer.Text = $"Игрок №{currentPlayer.id}";
        }

        private void StartNewGame(object sender, EventArgs e)
        {
            Application.Restart();
        }
    }

}