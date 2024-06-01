using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace wheelOfFortune
{
    public class FormUpdater
    {
        private readonly Form1 form;

        public FormUpdater(Form1 form)
        {
            this.form = form;
        }

        public void ShowPlayerAndBalance(IPlayer player)
        {
            form.ShowPlayerAndBalance(player);
        }

        public void ShowNotEnoughBalance()
        {
            form.ShowNotEnoughBalance();
        }

        public void UpdateBetsUI(IPlayer player, int sector, Label label)
        {
            form.UpdateBetsUI(player, sector, label);
        }

        public void PrepareTurningUI()
        {
            form.PrepareTurningUI();
        }
    }
}
