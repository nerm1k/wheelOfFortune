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
        private GameController gameController;

        public Form1()
        {
            InitializeComponent();
        }

        private void AllowOnlyDigits(object sender, KeyPressEventArgs e)
        {
            char button = e.KeyChar;

            if (!Char.IsDigit(button) && button != (char)8)
            {
                e.Handled = true;
            }
        }

        private void MakeBetX1Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(textBoxBet.Text))
            {
                int bet = int.Parse(textBoxBet.Text);
                gameController.MakeBetOnSector(1, bet, labelBetX1);
            }
        }

        private void MakeBetX2Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(textBoxBet.Text))
            {
                int bet = int.Parse(textBoxBet.Text);
                gameController.MakeBetOnSector(2, bet, labelBetX2);
            }
        }

        private void MakeBetX5Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(textBoxBet.Text))
            {
                int bet = int.Parse(textBoxBet.Text);
                gameController.MakeBetOnSector(5, bet, labelBetX5);
            }
        }

        private void MakeBetX10Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(textBoxBet.Text))
            {
                int bet = int.Parse(textBoxBet.Text);
                gameController.MakeBetOnSector(10, bet, labelBetX10);
            }
        }

        private void MakeBetX20Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(textBoxBet.Text))
            {
                int bet = int.Parse(textBoxBet.Text);
                gameController.MakeBetOnSector(20, bet, labelBetX20);
            }
        }

        private void MakeBetX40Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(textBoxBet.Text))
            {
                int bet = int.Parse(textBoxBet.Text);
                gameController.MakeBetOnSector(40, bet, labelBetX40);
            }
        }

        private void StartGameClick(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(textBoxPlayers.Text) && !string.IsNullOrEmpty(textBoxTurns.Text))
            {
                int playersCount = int.Parse(textBoxPlayers.Text);
                int turnsCount = int.Parse(textBoxTurns.Text);

                gameController = new GameController(this);
                gameController.StartGame(playersCount, turnsCount);

                PrepareGameUI();
            }
        }

        private void PassTheTurnClick(object sender, EventArgs e)
        {
            labelNotEnoughBalance.Visible = false;

            gameController.PassTheTurn();

            PrepareNextTurnUI();
        }

        private void StartNewGameClick(object sender, EventArgs e)
        {
            gameController.StartNewGame();
        }

        private void StartTurningClick(object sender, EventArgs e)
        {
            gameController.StartTurning();
        }

        public void ShowResults(Player player)
        {
            labelBalances.Text += $"Баланс игрока №{player.id}: {player.balance} \n";
        }

        public void ShowWinner(Player winner)
        {
            labelWinner.Text = $"Победитель: Игрок №{winner.id}.\nБаланс: {winner.balance}.\nИГРА ОКОНЧЕНА";
        }

        public void PrepareNewGameUI()
        {
            buttonStartNewGame.Visible = true;
            labelWinner.Visible = true;

            labelBetX1.Visible = false;
            labelBetX2.Visible = false;
            labelBetX5.Visible = false;
            labelBetX10.Visible = false;
            labelBetX20.Visible = false;
            labelBetX40.Visible = false;
            labelBalance.Visible = false;
            labelNotEnoughBalance.Visible = false;
            labelBet.Visible = false;
            labelCurrentPlayer.Visible = false;
            textBoxBet.Visible = false;
            buttonStartTurn.Visible = false;
            buttonBetX1.Visible = false;
            buttonBetX2.Visible = false;
            buttonBetX5.Visible = false;
            buttonBetX10.Visible = false;
            buttonBetX20.Visible = false;
            buttonBetX40.Visible = false;
            buttonPassTheTurn.Visible = false;
            pictureBoxWheel.Visible = false;
            pictureBoxArrow.Visible = false;
        }

        private void PrepareGameUI()
        {
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
            labelBet.Visible = true;
            labelCurrentPlayer.Visible = true;
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

        private void PrepareNextTurnUI()
        {
            textBoxBet.Clear();
            labelBetX1.Text = "Ставка на х1: ";
            labelBetX2.Text = "Ставка на х2: ";
            labelBetX5.Text = "Ставка на х5: ";
            labelBetX10.Text = "Ставка на х10: ";
            labelBetX20.Text = "Ставка на х20: ";
            labelBetX40.Text = "Ставка на х40: ";
        }

        public void PrepareNewTurnUI()
        {
            labelBetX1.Text = "Ставка на х1: ";
            labelBetX2.Text = "Ставка на х2: ";
            labelBetX5.Text = "Ставка на х5: ";
            labelBetX10.Text = "Ставка на х10: ";
            labelBetX20.Text = "Ставка на х20: ";
            labelBetX40.Text = "Ставка на х40: ";

            labelBetX1.Visible = true;
            labelBetX2.Visible = true;
            labelBetX5.Visible = true;
            labelBetX10.Visible = true;
            labelBetX20.Visible = true;
            labelBetX40.Visible = true;
            labelBalance.Visible = true;
            labelCurrentPlayer.Visible = true;
            textBoxBet.Visible = true;
            buttonBetX1.Visible = true;
            buttonBetX2.Visible = true;
            buttonBetX5.Visible = true;
            buttonBetX10.Visible = true;
            buttonBetX20.Visible = true;
            buttonBetX40.Visible = true;
            buttonPassTheTurn.Visible = true;

            buttonStartTurn.Visible = false;
        }

        public void PrepareTurningUI()
        {
            buttonStartTurn.Visible = true;

            textBoxBet.Clear();
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
        }
    }
}