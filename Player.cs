using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace wheelOfFortune
{
    public interface IPlayer
    {
        int id { get; }
        int balance { get; set; }
        Dictionary<int, int> bets { get; }

        void MakeBet(int sector, int bet);
        bool CanMakeBet(int bet);
        int CalculatePrize(int result);
    }

    public class Player: IPlayer
    {
        private static int nextId = 1;

        public int id { get; }
        public int balance { get; set; }
        public Dictionary<int, int> bets { get; }

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
        }

        public void MakeBet(int sector ,int bet)
        {
            bets[sector] += bet;
            balance -= bet;
        }

        public bool CanMakeBet(int bet)
        {
            if (balance < bet)
            {
                return false;
            }
            return true;
        }

        public int CalculatePrize(int result)
        {
            int prize = 0;
            foreach (var bet in bets)
            {
                if (bet.Key == result)
                {
                    prize = bet.Value * result;
                    balance += prize;
                }
            }
            foreach (var key in bets.Keys.ToList())
            {
                bets[key] = 0;
            }
            return prize;
        }
    }
}
