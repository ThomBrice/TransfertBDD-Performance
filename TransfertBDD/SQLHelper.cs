using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;

namespace TransfertBDD
{
    class SQLHelper
    {
        #region variables;
        public DataSet MyDataSet = new DataSet();
        SqlConnection connexionClient = new SqlConnection(Properties.Settings.Default.StrConnClient);
        SqlConnection connexionBanc = new SqlConnection(Properties.Settings.Default.StrConnDonn);
        #endregion;

        /// <summary>
        /// Ouvre la connexion avec la BDD
        /// </summary>
        /// <param name="connexion"></param>
        /// <returns> Boolean pour savoir si la connexion est établie</returns>
        private bool OpenConnexion(SqlConnection connexion)
        {
            try
            {
                connexion.Open();
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return false;
            }
        }

        /// <summary>
        /// Ferme la connexion avec la BDD
        /// </summary>
        /// <param name="connexion"></param>
        /// <returns>Boolean pour savoir si la connexion est bien fermée</returns>
        private bool CloseConnexion(SqlConnection connexion)
        {
            try
            {
                connexion.Close();
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return false;
            }
        }

        /// <summary>
        /// Ajoute un client à la BDD
        /// </summary>
        /// <param name="client"></param>
        public void AddClient(String client)
        {
            String query = "Insert Into Clients (Client) Values('" + client + "')";

            if (this.OpenConnexion(connexionClient) == true)
            {
                SqlCommand cmd = new SqlCommand(query, connexionClient);
                cmd.ExecuteNonQuery();
                this.CloseConnexion(connexionClient);
            }
        }

        /// <summary>
        /// Remplit le Dataset avec les données clients de la BDD Connexion Client
        /// </summary>
        public void UpdateDataSetClient()
        {
            String query = "Select * from Clients";

            if (this.OpenConnexion(connexionClient) == true)
            {
                SqlCommand cmd = new SqlCommand(query, connexionClient);
                SqlDataAdapter adapteur = new SqlDataAdapter(cmd);
                adapteur.Fill(MyDataSet, "Clients");
                this.CloseConnexion(connexionClient);
            }
        }

        public bool SaveEntete(String Client,String Signal,int Dbv,int Cbv,int Chv,String Remarques)
        {
            String query = "Insert Into Entête(Date,Client,Signal,[Détente Basse Vitesse]," +
                "[Compression Basse Vitesse],[Compression Haute Vitesse],Remarques) Values" +
                "(GETDATE(),'" + Client + "','" + Signal + "'," + Dbv + "," + Cbv + "," + Chv + ",'" + Remarques + "')";
            if (this.OpenConnexion(connexionBanc) == true)
            {
                SqlCommand cmd = new SqlCommand(query, connexionBanc);
                cmd.ExecuteNonQuery();
                this.CloseConnexion(connexionBanc);
                return true;
            }
            return false;
        }
    }
}
