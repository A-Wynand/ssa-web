using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;


namespace Festival.RSS
{
    public class RSSSQLRepository
    {
        private readonly string _connectionString;

        public RSSSQLRepository()
        {
            _connectionString = ConfigurationManager.ConnectionStrings["FestivalConString"].ConnectionString;
        }

        public IList<MyFeedItem> GetAllRSS()
        {
            using (var con = new SqlConnection(_connectionString))
            {
                con.Open();

                using (var cmd = new SqlCommand("SELECT * FROM RSS", con))
                {
                    cmd.CommandType = CommandType.Text;

                    using (var reader = cmd.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            IList<MyFeedItem> items = new List<MyFeedItem>();

                            while (reader.Read())
                            {
                                MyFeedItem newItem = new MyFeedItem();

                                newItem.Id = Int32.Parse(reader["ID"].ToString());
                                newItem.Title = reader["Title"].ToString();
                                newItem.Description = reader["Description"].ToString();
                                newItem.CreatedBy = reader["CreatedBy"].ToString();
                                newItem.DateAdded = DateTime.Parse(reader["DateAdded"].ToString());

                                items.Add(newItem);
                            }

                            return items;
                        }
                        return null;
                    }

                }
            }
        }
    }
}