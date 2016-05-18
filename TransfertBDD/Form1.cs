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
using System.Data.SqlClient;

namespace TransfertBDD
{
    public partial class Form1 : Form
    {
        #region variables
        Functions fonction = new Functions();
        SQLHelper SqlHelper = new SQLHelper();
        ReadFileHelper FileHelper = new ReadFileHelper();
        SqlConnection connexionBanc = new SqlConnection(Properties.Settings.Default.StrConnDonn);
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
                FileHelper.Read(listOfFiles.Text);
                if (!fonction.CheckClient(SqlHelper.MyDataSet, FileHelper.ExtractClient()))
                {
                    var result = MessageBox.Show("Le client n'existe pas. Voulez-vous l'ajouter?","Attention!", MessageBoxButtons.YesNo,MessageBoxIcon.Question);
                    if(result == DialogResult.Yes)
                    {
                        SqlHelper.AddClient(FileHelper.ExtractClient());
                        SqlHelper.MyDataSet.Tables["Clients"].Clear();
                        SqlHelper.UpdateDataSetClient();
                        listOfClient.Text = FileHelper.ExtractClient();
                    }
                }
                else
                {
                    listOfClient.Text = FileHelper.ExtractClient();
                }
            }
            catch
            {
                MessageBox.Show("Un probléme réside avec votre fichier txt","Attention!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            #region remplissage de l'entête
            String Client = listOfClient.Text;
            String Signal = SignalBox.Text;
            int Dbv = int.Parse(DbvBox.Text);
            int Cbv = int.Parse(CbvBox.Text);
            int Chv = int.Parse(ChvBox.Text);
            String Remarques = remarqueBox.Text;

            Cursor.Current = Cursors.WaitCursor;  // on met un curseur d'attente

            if(SqlHelper.SaveEntete(Client,Signal,Dbv,Cbv,Chv,Remarques) == true)
            {
                #region remplissage des données
                if (SqlHelper.OpenConnexion(connexionBanc) == true)
                {
                    int size = FileHelper.content.Length - 4;
                    for (int i = 16; i < size; i++)
                    {
                        String[] datas = FileHelper.ExtractionValeurs(i);
                        SqlHelper.SaveData(datas,7,connexionBanc);
                    }
                }
                SqlHelper.CloseConnexion(connexionBanc);
                #endregion

                MessageBox.Show("Transfert vers la BDD réussi!", "Confirmation", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("Le transfert à échoué, vérifiez votre connexion au réseau", "Attention!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            #endregion

            Cursor.Current = Cursors.WaitCursor; // on remet le curseur normal
        }
    }
}
