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
    public class TicketSQLRepository
    {
        private readonly string _connectionString;

        public TicketSQLRepository()
        {
            _connectionString = ConfigurationManager.ConnectionStrings["FestivalConString"].ConnectionString;
        }

        public IList<Ticket> getAllTickets()
        {
            using (var con = new SqlConnection(_connectionString))
            {
                con.Open();

                using (var cmd = new SqlCommand("SELECT * FROM Ticket", con))
                {
                    cmd.CommandType = CommandType.Text;

                    // This is what stores your data from the internets...
                    using (var reader = cmd.ExecuteReader())
                    {

                        if (reader.HasRows)
                        {
                            var lijst = new List<Ticket>();

                            while (reader.Read())
                            {
                                Ticket newTicket = new Ticket();
                                newTicket.ID = Int32.Parse(reader["ID"].ToString());
                                newTicket.soort = reader["soort"].ToString();
                                newTicket.aantal = Int32.Parse(reader["aantal"].ToString());

                                var prijs = reader["prijs"].ToString();
                                prijs = (prijs == "" ? "0" : prijs);

                                newTicket.prijs = float.Parse(prijs);

                                lijst.Add(newTicket);
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