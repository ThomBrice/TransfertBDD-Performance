using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace TransfertBDD
{
    public partial class Form1 : Form
    {
        #region variables
        Functions fonction = new Functions();
        SQLHelper SqlHelper = new SQLHelper();
        #endregion

        public Form1()
        {
            InitializeComponent();



            #region remplissage de la Liste de client
            ReadFileHelper helper = new ReadFileHelper();
            foreach (var adress in helper.FindFiles())
            {
                listOfFiles.Items.Add(adress);
            }
            #endregion

            #region remplissage du DataSet
            SqlHelper.UpdateDataSetClient();
            #endregion

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            ReadFileHelper helper = new ReadFileHelper();

            try {
                clientName.Text = helper.ExtractClient(helper.Read(listOfFiles.Text));
                if (!fonction.CheckClient(SqlHelper.MyDataSet, clientName.Text))
                {
                    MessageBox.Show("le client n'existe pas");
                }
            }
            catch
            {
                MessageBox.Show("Un probléme réside avec votre fichier txt","Attention!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
