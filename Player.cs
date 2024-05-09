using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace wheelOfFortune
{
    public class Player
    {
        private static int nextId = 1;
        private readonly Form1 form;

        public readonly int id;
        public int balance;
        public Dictionary<int, int> bets;

        public Player(Form1 form)
        {
            id = nextId;
            nextId++;
            this.form = form;
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

        public void CalculatePrize(int result)
        {
            foreach (var bet in bets)
            {
                if (bet.Key == result)
                {
                    int prize = bet.Value * result;
                    balance += prize;
                    form.labelPrizes.Text += $"Игрок №{id} выиграл {prize} \n";
                }
            }
            foreach (var key in bets.Keys.ToList())
            {
                bets[key] = 0;
            }
        }
    }
}
