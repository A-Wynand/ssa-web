using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;


namespace Login.Models.DAL
{
    public class GebruikerSQLRepository
    {
        private readonly string _connectionString;

        public GebruikerSQLRepository()
        {
            _connectionString = ConfigurationManager.ConnectionStrings["FestivalConString"].ConnectionString;
        }

        public IList<Gebruiker> getAllGebruikers()
        {
            using (var con = new SqlConnection(_connectionString))
            {
                con.Open();

                using (var cmd = new SqlCommand("SELECT * FROM UserProfile", con))
                {
                    cmd.CommandType = CommandType.Text;

                    // This is what stores your data from the internets...
                    using (var reader = cmd.ExecuteReader())
                    {

                        if (reader.HasRows)
                        {
                            var lijst = new List<Gebruiker>();

                            while (reader.Read())
                            {
                                Gebruiker newGebruiker = new Gebruiker();
                                newGebruiker.UserId = Int32.Parse(reader["UserId"].ToString());
                                newGebruiker.UserName = reader["UserName"].ToString();
                                newGebruiker.anaam = reader["anaam"].ToString();
                                newGebruiker.vnaam = reader["vnaam"].ToString();
                                newGebruiker.email = reader["email"].ToString();
                                
                                lijst.Add(newGebruiker);
                            }

                            return lijst; // lol
                        }

                        // internet says null!L!LL!L!
                        return null;
                    }

                }
            }
        }

    }
}