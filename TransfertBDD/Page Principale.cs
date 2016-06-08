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
    public partial class PagePrincipale : XtraForm
    {
        #region variables
        Functions fonction = new Functions();
        SQLHelper SqlHelper = new SQLHelper();
        ReadFileHelper FileHelper = new ReadFileHelper();
        SqlConnection connexionBanc = new SqlConnection(Properties.Settings.Default.StrConnDonn);
        #endregion

        public PagePrincipale()
        {
            InitializeComponent();
        }

        private void PagePrincipale_Load(object sender, EventArgs e)
        {

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
                DbvBox.Text = "?";
                CbvBox.Items.Add(i);
                CbvBox.Text = "?";
                ChvBox.Items.Add(i);
                ChvBox.Text = "?";
            }
            #endregion

            #region remplissage du texte du signal
            signalText.Text = "signal ?";
            #endregion
        }

        private void fileButton_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                FileHelper.Read(openFileDialog.FileName);
                transfertButton.Show();
            }
        }

        private void resetButton_Click(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;  // on met un curseur d'attente
            PagePrincipale_Load(sender, e);
            transfertButton.Hide();
            Cursor.Current = Cursors.WaitCursor; // on remet le curseur normal
        }

        private void transfertButton_Click(object sender, EventArgs e)
        {
            bool test = false;

            if (DbvBox.Text != "?" & CbvBox.Text != "?" & ChvBox.Text != "?" & signalText.Text != "signal?" & listOfClient.Text!="Liste des clients")
            {
                test = true;
            }

            if (test == true)
            {

                #region remplissage de l'entête
                String Client = listOfClient.Text;
                String Signal = signalText.Text;
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
                        int start = FileHelper.DetectionStart(FileHelper.content);
                        while (start < size)
                        {
                            start++;
                            progressBar.PerformStep();
                            if (FileHelper.content[start] != "")
                            {
                                String[] datas = FileHelper.ExtractionValeurs(start);
                                SqlHelper.SaveData(datas, ID, connexionBanc);
                            }
                        }
                    }
                    SqlHelper.CloseConnexion(connexionBanc);
                    #endregion
                    transfertButton.Hide();
                    PagePrincipale_Load(sender, e);
                    XtraMessageBox.Show("Transfert vers la BDD réussi!", "Confirmation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    XtraMessageBox.Show("Le transfert à échoué, vérifiez votre connexion au réseau", "Attention!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                #endregion
                progressBar.Value = 0;
                transfertButton.Hide();
                Cursor.Current = Cursors.WaitCursor; // on remet le curseur normal
            }
            else
            {
                XtraMessageBox.Show("Veuillez remplir les valeurs de clics ainsi que le signal utilisé", "Attention!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void clientButton_Click(object sender, EventArgs e)
        {
            PopUpClient pop = new PopUpClient();
            if (pop.ShowDialog() == DialogResult.Yes)
            {
                PagePrincipale_Load(sender,e);
            }
        }
    }
}
