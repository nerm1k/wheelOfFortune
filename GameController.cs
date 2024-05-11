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
            Player currentPlayer = wheel.players[0];

            form.ShowPlayerAndBalance(currentPlayer);
        }

        public void MakeBetOnSector(int sector, int bet, Label label)
        {
            Player currentPlayer = wheel.players[wheel.currentPlayerIndex];

            if (!currentPlayer.CanMakeBet(bet))
            {
                form.ShowNotEnoughBalance();
                return;
            }
            currentPlayer.MakeBet(sector, bet);

            form.UpdateBetsUI(currentPlayer, sector, label);
        }

        public void PassTheTurn()
        {
            if (wheel.currentPlayerIndex + 1 == wheel.players.Count)
            {
                form.PrepareTurningUI();

                return;
            }

            wheel.currentPlayerIndex++;
            Player currentPlayer = wheel.players[wheel.currentPlayerIndex];

            form.ShowPlayerAndBalance(currentPlayer);
        }

        public void StartTurning()
        {
            wheel.wheelIsMoving = true;
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
