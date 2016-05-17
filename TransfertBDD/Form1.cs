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
        ReadFileHelper FileHelper = new ReadFileHelper();
        #endregion

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            #region remplissage de la Liste de fichiers
            foreach (var adress in FileHelper.FindFiles())
            {
                listOfFiles.Items.Add(adress);
            }
            #endregion

            #region remplissage du DataSet
            SqlHelper.UpdateDataSetClient();
            #endregion

            #region remplissage de la liste de clients
            listOfClient.DataSource = SqlHelper.MyDataSet.Tables["Clients"];
            listOfClient.DisplayMember = "Client";
            listOfClient.Text = "Liste des clients";
            #endregion
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

            try {
                if (!fonction.CheckClient(SqlHelper.MyDataSet, FileHelper.ExtractClient(FileHelper.Read(listOfFiles.Text))))
                {
                    var result = MessageBox.Show("Le client n'existe pas. Voulez-vous l'ajouter?","Attention!", MessageBoxButtons.YesNo,MessageBoxIcon.Question);
                    if(result == DialogResult.Yes)
                    {
                        SqlHelper.AddClient(FileHelper.ExtractClient(FileHelper.Read(listOfFiles.Text)));
                        SqlHelper.MyDataSet.Tables["Clients"].Clear();
                        SqlHelper.UpdateDataSetClient();
                        listOfClient.Text = FileHelper.ExtractClient(FileHelper.Read(listOfFiles.Text));
                    }
                }
                else
                {
                    listOfClient.Text = FileHelper.ExtractClient(FileHelper.Read(listOfFiles.Text));
                }
            }
            catch
            {
                MessageBox.Show("Un probléme réside avec votre fichier txt","Attention!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
