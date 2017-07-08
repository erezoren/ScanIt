using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MySql.Data.MySqlClient;
using MySql.Data;
using MySql.Data.Types;
using System.Windows.Forms;
using ScanIt.Logic;

namespace ScanIt
{
    class QueryExec
    {
        private MySqlConnection conn = new MySql.Data.MySqlClient.MySqlConnection("server="+ ScanItProperties.Instance.getPropValue("DB.HOST")+ ";uid="+ ScanItProperties.Instance.getPropValue("DB.USER") + ";" +
            "pwd="+ ScanItProperties.Instance.getPropValue("DB.PASS") + ";database="+ ScanItProperties.Instance.getPropValue("DB.NAME") + ";CharSet=utf8;");

        public QueryExec()
        {
            
        }

        public Dictionary<string, List<string>> performSelectQuery(string query)
        {
            var map = new Dictionary<string, List<string>>();
            try
            {
                conn.Open();
                MySqlCommand cmd = conn.CreateCommand();
                cmd.CommandText = query;
                MySqlDataReader reader = cmd.ExecuteReader();


                do
                {
                    int count = reader.FieldCount;
                    while (reader.Read())
                    {
                        for (int i = 0; i < count; i++)
                        {
                            if (map.ContainsKey(reader.GetName(i)))
                            {
                                if (!map[reader.GetName(i)].Contains(reader.GetString(i)))
                                {
                                    map[reader.GetName(i)].Add(reader.GetString(i));
                                }
                            }
                            else
                            {
                                List<string> values = new List<string>();
                                values.Add(reader.GetString(i));
                                map.Add(reader.GetName(i), values);
                            }
                        }
                    }
                } while (reader.NextResult());

            
                conn.Close();
                
            }
            catch (MySql.Data.MySqlClient.MySqlException ex)
            {
                MessageBox.Show(ex.Message);
            }
            return map;
    }

        public long performInsertQuery(string query)
        {

            try
            {
                conn.Open();
                MySqlCommand cmd = conn.CreateCommand();
                cmd.CommandText = query;
                cmd.ExecuteNonQuery();
                long id = cmd.LastInsertedId;
                conn.Close();
                return id;
               
            }
            catch (MySql.Data.MySqlClient.MySqlException ex)
            {
                conn.Close();
                throw new QueryExecException(ex.Message);
            }
        }
    }

   
   
}
