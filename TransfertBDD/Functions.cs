using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;

namespace TransfertBDD
{
    class Functions
    {
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
