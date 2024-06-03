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
        private readonly FormUpdater formUpdater;
        private WheelOfFortune wheel;

        public GameController(Form1 form)
        {
            formUpdater = new FormUpdater(form);
        }

        public void StartGame(Form1 form, int playersCount, int turnsCount)
        {

            wheel = new WheelOfFortune(form, playersCount, turnsCount);
            IPlayer currentPlayer = wheel.players[0];

            formUpdater.ShowPlayerAndBalance(currentPlayer);
        }

        public void MakeBetOnSector(int sector, int bet, Label label)
        {
            IPlayer currentPlayer = wheel.players[wheel.currentPlayerIndex];

            if (!currentPlayer.CanMakeBet(bet))
            {
                formUpdater.ShowNotEnoughBalance();
                return;
            }
            currentPlayer.MakeBet(sector, bet);

            formUpdater.UpdateBetsUI(currentPlayer, sector, label);
        }

        public void PassTheTurn()
        {
            if (wheel.currentPlayerIndex + 1 == wheel.players.Count)
            {
                formUpdater.PrepareTurningUI();

                return;
            }

            wheel.currentPlayerIndex++;
            IPlayer currentPlayer = wheel.players[wheel.currentPlayerIndex];

            formUpdater.ShowPlayerAndBalance(currentPlayer);
        }

        public void StartTurning()
        {
            wheel.wheelIsMoving = true;
            Random rand = new Random();
            wheel.numberOfTwists = rand.Next(50, 100);
            wheel.wheelTimer.Start();
        }

        public void StartNewGame()
        {
            Application.Restart();
        }
    }
}
