using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DesktopClient
{
    public partial class FormAccount : Form
    {
        public Dictionary<string, string> tokenDictionary;

        public FormAccount(Dictionary<string, string> token)
        {
            InitializeComponent();
            tokenDictionary = token;
        }

        private void buttonMap_Click(object sender, EventArgs e)
        {
            FormMap fm = new FormMap(tokenDictionary);
            fm.Show();
            this.Close();
        }

        private void buttonAccount_Click(object sender, EventArgs e)
        {

        }

        private void checkBoxBusy_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void buttonBackToMap_Click(object sender, EventArgs e)
        {

        }
    }
}
