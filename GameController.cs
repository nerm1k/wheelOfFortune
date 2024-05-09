using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace wheelOfFortune
{
    public class GameController
    {
        private WheelOfFortune wheel;
        private Form1 form;

        public GameController(Form1 form)
        {
            this.form = form;
        }

        public void StartGame(int playersCount, int turnsCount)
        {

            wheel = new WheelOfFortune(form, playersCount, turnsCount);
            Player currentPlayer = wheel.playersList[0];

            form.labelBalance.Text = $"Баланс: {currentPlayer.balance}";
            form.labelCurrentPlayer.Text = $"Игрок №{currentPlayer.id}";
        }

        public void MakeBetOnSector(int sector, int bet, Label label)
        {
            Player currentPlayer = wheel.playersList[wheel.currentPlayerIndex];
            if (!currentPlayer.CanMakeBet(bet))
            {
                currentPlayer.ShowNotEnoughBalance();
                return;
            }
            currentPlayer.MakeBet(sector, bet);
            form.UpdateBetsUI(currentPlayer.id, currentPlayer.balance, currentPlayer.bets[sector], sector, label);
            return;
        }

        public void PassTheTurn()
        {
            if (wheel.currentPlayerIndex + 1 == wheel.playersList.Count)
            {
                form.PrepareTurningUI();

                return;
            }

            wheel.currentPlayerIndex++;
            Player currentPlayer = wheel.playersList[wheel.currentPlayerIndex];

            form.labelBalance.Text = $"Баланс: {currentPlayer.balance}";
            form.labelCurrentPlayer.Text = $"Игрок №{currentPlayer.id}";
        }

        public void StartTurning()
        {
            wheel.wheelIsMoved = true;
            Random rand = new Random();
            wheel.numberOfTwists = rand.Next(50, 100); // кол-во кручений
            wheel.wheelTimer.Start();
        }

        public void StartNewGame()
        {
            Application.Restart();
        }
    }
}
