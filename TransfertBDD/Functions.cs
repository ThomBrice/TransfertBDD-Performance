using System;
using System.Data;

namespace TransfertBDD
{
    class Functions
    {
        /// <summary>
        /// Vérifie si le client existe déjà dans la BDD
        /// </summary>
        /// <param name="MyDataSet"></param>
        /// <param name="ClientName"></param>
        /// <returns></returns>
        public bool CheckClient(DataSet MyDataSet,String ClientName)
        {
            bool test = false;

            foreach(DataRow dataRow in MyDataSet.Tables["Clients"].Rows)
            {
                if (dataRow["Client"].ToString().Equals(ClientName) == true)
                {
                    test = true;
                }
            }

            return test;
        }

    }
}
