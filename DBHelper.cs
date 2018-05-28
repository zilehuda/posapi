using System;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Collections.Generic;
namespace PosApi
{
    public  class DBHelper
    {
        SqlConnection myConn;
        private void ConnectionOpen()
        {
            
            string connectionString = ConfigurationManager.ConnectionStrings["POSDB"].ConnectionString;
            myConn = new SqlConnection(connectionString);
            myConn.Open();
        }

        private void ConnectionClose()
        {
            myConn.Close();
        }

        public string ExecuteScalar(string query)
        {
            string firstValueOnly = "";
            try
            {
                ConnectionOpen();
                SqlCommand command = new SqlCommand(query, myConn);
                object o = command.ExecuteScalar();
                try
                {
                    firstValueOnly = o.ToString();
                }
                catch (Exception ex)
                {
                }

                ConnectionClose();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return firstValueOnly;
        }

        public DataTable ExecuteDataTable(string query)
        {
            ConnectionOpen();
            SqlCommand command = new SqlCommand(query, myConn);
            SqlDataReader reader = command.ExecuteReader();

            DataTable dt = new DataTable("Result");
            var counter = 0;
            for (; counter < reader.FieldCount; counter++)
            {
                dt.Columns.Add(reader.GetName(counter));
            }

            while (reader.Read())
            {
                object[] param = new object[counter];
                for (int i = 0; i < counter; i++)
                {
                    param[i] = reader[i];
                }
                dt.Rows.Add(param);
            }

            ConnectionClose();
            return dt;
        }

        public DataTable ExecuteDataTableParam(string query, Dictionary<string, string> dic)
        {
            ConnectionOpen();
            SqlCommand command = new SqlCommand(query, myConn);
            foreach (var item in dic)
            {
                command.Parameters.Add(new SqlParameter(item.Key, item.Value));
            }
            SqlDataReader reader = command.ExecuteReader();

            DataTable dt = new DataTable("Result");
            var counter = 0;
            for (; counter < reader.FieldCount; counter++)
            {
                dt.Columns.Add(reader.GetName(counter));
            }

            while (reader.Read())
            {
                object[] param = new object[counter];
                for (int i = 0; i < counter; i++)
                {
                    param[i] = reader[i];
                }
                dt.Rows.Add(param);
            }

            ConnectionClose();
            return dt;
        }

        public bool ExecuteNonQuery(string query, Dictionary<string, string> dic)
        {
            bool bReturn = false;
            try
            {
                ConnectionOpen();
                SqlCommand command = new SqlCommand(query, myConn);

                foreach (var item in dic)
                {
                    command.Parameters.Add(new SqlParameter(item.Key, item.Value));
                }
                int count = command.ExecuteNonQuery();
                if (count > 0)
                    bReturn = true;
                else
                    bReturn = false;

                ConnectionClose();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return bReturn;
        }

        public DataSet ExecuteDataAdapter(string query)
        {
            ConnectionOpen();
            DataSet ds = new DataSet();
            using (SqlDataAdapter a = new SqlDataAdapter(
                    query, myConn))
            {
                a.Fill(ds);
            }
            ConnectionClose();
            return ds;
        }
    }
}