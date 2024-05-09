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
            labelWinner.Visible = true;
            labelWinner.Text = $"Победитель: Игрок №{winner.id}.\nБаланс: {winner.balance}.\nИГРА ОКОНЧЕНА";
        }

        public void UpdateBetsUI(int playerId, int playerBalance, int playerBetsOnSector, int sector, Label label)
        {
            label.Text = $"Ставка на х{sector}: {playerBetsOnSector}";
            labelBalance.Text = $"Баланс: {playerBalance}";
            labelCurrentPlayer.Text = $"Игрок №{playerId}";
            textBoxBet.Clear();
            labelNotEnoughBalance.Visible = false;
        }

        private void SwitchUI(bool value)
        {
            labelBetX1.Visible = value;
            labelBetX2.Visible = value;
            labelBetX5.Visible = value;
            labelBetX10.Visible = value;
            labelBetX20.Visible = value;
            labelBetX40.Visible = value;
            buttonBetX1.Visible = value;
            buttonBetX2.Visible = value;
            buttonBetX5.Visible = value;
            buttonBetX10.Visible = value;
            buttonBetX20.Visible = value;
            buttonBetX40.Visible = value;
            labelBalance.Visible = value;
            labelBet.Visible = value;
            textBoxBet.Visible = value;
            labelCurrentPlayer.Visible = value;
            buttonPassTheTurn.Visible = value;
        }

        private void PrepareLabelsBets()
        {
            labelBetX1.Text = "Ставка на х1: ";
            labelBetX2.Text = "Ставка на х2: ";
            labelBetX5.Text = "Ставка на х5: ";
            labelBetX10.Text = "Ставка на х10: ";
            labelBetX20.Text = "Ставка на х20: ";
            labelBetX40.Text = "Ставка на х40: ";
        }

        public void PrepareNewGameUI()
        {
            buttonStartNewGame.Visible = true;

            SwitchUI(false);
            labelNotEnoughBalance.Visible = false;
            buttonStartTurn.Visible = false;
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

            SwitchUI(true);
            pictureBoxWheel.Visible = true;
            pictureBoxArrow.Visible = true;
        }

        private void PrepareNextTurnUI()
        {
            PrepareLabelsBets();
            textBoxBet.Clear();
        }

        public void PrepareNewTurnUI()
        {
            PrepareLabelsBets();
            buttonStartTurn.Visible = false;

            SwitchUI(true);
        }

        public void PrepareTurningUI()
        {
            buttonStartTurn.Visible = true;

            SwitchUI(false);
            textBoxBet.Clear();
            labelWinningSector.Visible = false;
            labelPrizes.Visible = false;
        }
    }
}