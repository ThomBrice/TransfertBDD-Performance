using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TransfertBDD
{
    public partial class PopUpClient : Form
    {
        public PopUpClient()
        {
            InitializeComponent();
        }

        private void OKButton_Click(object sender, EventArgs e)
        {
            SQLHelper SqlHelper = new SQLHelper();
            if(!clientText.Text.Equals(""))
            {
                SqlHelper.AddClient(clientText.Text);
                SqlHelper.UpdateDataSetClient();
                this.DialogResult = DialogResult.Yes;
                Close();
            }
            else
            {
                MessageBox.Show("Vous n'avez pas rentré de nom", "Attention");
            }
        }

        private void AnnulButton_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
