using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DBHelper;
using System.Data.Entity;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;


namespace FestivalWeb.Models
{
    public class Gebruikers
    {
        public int ID { get; set; }
        public string vnaam { get; set; }
        public string anaam { get; set; }
        public string email { get; set; }
        public string wachtwoord { get; set; }
        public Boolean isAdmin { get; set; }
    }

    public class GebruikersDBHandler
    {
        private readonly string _connectionString;

        public GebruikersDBHandler()
        {
            _connectionString = ConfigurationManager.ConnectionStrings["FestivalConString"].ConnectionString;
        }

        public IList<Gebruikers> getAllGebruikers()
        {
            using (var con = new SqlConnection(_connectionString))
            {
                con.Open();

                using (var cmd = new SqlCommand("SELECT * FROM Gebruikers", con))
                {
                    cmd.CommandType = CommandType.Text;

                    // This is what stores your data from the internets...
                    using (var reader = cmd.ExecuteReader())
                    {

                        if (reader.HasRows)
                        {
                            var lijst = new List<Gebruikers>();

                            while (reader.Read())
                            {
                                Gebruikers newGebruiker = new Gebruikers();
                                newGebruiker.ID = Int32.Parse(reader["ID"].ToString());
                                newGebruiker.anaam = reader["anaam"].ToString();
                                newGebruiker.vnaam = reader["vnaam"].ToString();
                                newGebruiker.email = reader["email"].ToString();
                                newGebruiker.wachtwoord = reader["wachtwoord"].ToString();
                                newGebruiker.isAdmin = Int32.Parse(reader["isAdmin"].ToString()) == 1 ? true : false;

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