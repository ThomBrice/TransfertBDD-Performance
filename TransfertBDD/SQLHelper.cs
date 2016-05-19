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
        public bool OpenConnexion(SqlConnection connexion)
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
        public bool CloseConnexion(SqlConnection connexion)
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

        /// <summary>
        /// Enregistre les données de l'entête dans la BDD Banc Hydraulique
        /// </summary>
        /// <param name="Client"></param>
        /// <param name="Signal"></param>
        /// <param name="Dbv"></param>
        /// <param name="Cbv"></param>
        /// <param name="Chv"></param>
        /// <param name="Remarques"></param>
        /// <returns></returns>
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

        /// <summary>
        /// Enregistre les données dans la BDD (nécessite que la connexion soit deja établie)
        /// </summary>
        /// <param name="datas"></param>
        /// <param name="ID"></param>
        /// <param name="connexion"></param>
        public void SaveData(String[] datas, int ID, SqlConnection connexion)
        {
            String query = "Insert Into Données(ID,position,force,vitesse,acceleration) Values(" +
                ID + "," + datas[0] + "," + datas[1] + "," + datas[2] + "," + datas[3] + ")";

            SqlCommand cmd = new SqlCommand(query, connexion);
            cmd.ExecuteNonQuery();
        }

        public int RecoverID(String Client,String Signal,float Dbv,float Cbv,float Chv)
        {
            int ID=0;

            String query = "Select ID From Entête WHere (Client='" + Client + "') AND (Signal='" + Signal + "') AND ([Détente Basse Vitesse]= " + Dbv +
                ") AND ([Compression Basse Vitesse]=" + Cbv + ") AND ([Compression Haute Vitesse]=" + Chv + ")";

            if (this.OpenConnexion(connexionBanc) == true)
            {
                SqlCommand cmd = new SqlCommand(query, connexionBanc);
                //SqlDataReader reader = cmd.ExecuteReader();
                ID = (int)cmd.ExecuteScalar();
                this.CloseConnexion(connexionBanc);
                return ID;
            }
            return ID;

        }
    }
}
