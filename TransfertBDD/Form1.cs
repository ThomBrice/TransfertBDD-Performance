using System;
using System.Windows.Forms;
using System.Data.SqlClient;
using DevExpress.XtraEditors;
using DevExpress.UserSkins;
using DevExpress.Data;
using DevExpress.Utils;
using DevExpress.Skins;
using DevExpress.LookAndFeel;

namespace TransfertBDD
{
    public partial class Form1 : XtraForm
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
            DevExpress.UserSkins.BonusSkins.Register();
            DevExpress.Skins.SkinManager.EnableFormSkins();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            #region remplissage de la Liste de fichiers
            listOfFiles.Items.Clear();
            listOfFiles.Text = "Liste des Clients";
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

            #region remplissage des box de detente et compression  (nombres de clics dans la boucle FOR)
            for (int i=0; i < 61; i++)
            {
                DbvBox.Items.Add(i);
                CbvBox.Items.Add(i);
                ChvBox.Items.Add(i);
            }
            #endregion 
        }

        private void listOfFiles_SelectedIndexChanged(object sender, EventArgs e)
        {

            try {
                FileHelper.Read(listOfFiles.Text);
                if (!fonction.CheckClient(SqlHelper.MyDataSet, FileHelper.ExtractClient()))
                {
                    var result = XtraMessageBox.Show("Le client"+ FileHelper.ExtractClient()+" n'existe pas. Voulez-vous l'ajouter?","Attention!", MessageBoxButtons.YesNo,MessageBoxIcon.Question);
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
                XtraMessageBox.Show("Un probléme réside avec votre fichier txt","Attention!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            progressBar.Maximum = FileHelper.content.Length-20;
            progressBar.Step = 1;
            progressBar.Value = 0;
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            bool test = false;

            if(DbvBox.Text!="?" &  CbvBox.Text != "?" & ChvBox.Text != "?" & SignalBox.Text != "Liste des Signaux")
            {
                test = true;
            }

            if (test == true)
            {

                #region remplissage de l'entête
                String Client = listOfClient.Text;
                String Signal = SignalBox.Text;
                int Dbv = int.Parse(DbvBox.Text);
                int Cbv = int.Parse(CbvBox.Text);
                int Chv = int.Parse(ChvBox.Text);
                String Remarques = remarqueBox.Text;

                Cursor.Current = Cursors.WaitCursor;  // on met un curseur d'attente

                if (SqlHelper.SaveEntete(Client, Signal, Dbv, Cbv, Chv, Remarques) == true)
                {
                    int ID = SqlHelper.RecoverID(Client, Signal, Dbv, Cbv, Chv);

                    #region remplissage des données
                    if (SqlHelper.OpenConnexion(connexionBanc) == true)
                    {
                        int size = FileHelper.content.Length - 4;
                        for (int i = 16; i < size; i++)
                        {
                            progressBar.PerformStep();
                            if (FileHelper.content[i] != "")
                            {
                                String[] datas = FileHelper.ExtractionValeurs(i);
                                SqlHelper.SaveData(datas, ID, connexionBanc);
                            }
                        }
                    }
                    SqlHelper.CloseConnexion(connexionBanc);
                    #endregion

                    XtraMessageBox.Show("Transfert vers la BDD réussi!", "Confirmation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    XtraMessageBox.Show("Le transfert à échoué, vérifiez votre connexion au réseau", "Attention!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                #endregion
                progressBar.Value=0;
                Cursor.Current = Cursors.WaitCursor; // on remet le curseur normal
            }
            else
            {
                XtraMessageBox.Show("Veuillez remplir les valeurs de clics ainsi que le signal utilisé", "Attention!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;  // on met un curseur d'attente
            Form1_Load(sender, e);
            Cursor.Current = Cursors.WaitCursor; // on remet le curseur normal
        }
    }
}
