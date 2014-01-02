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

        public Boolean insertTicketLine(String soort, int aantal, int userid)
        {
            using (var con = new SqlConnection(_connectionString))
            {
                con.Open();

                using (var cmd = new SqlCommand("INSERT INTO Orders (userid, ticket, aantal) VALUES (@userid, (SELECT ID FROM Ticket WHERE soort = @soort), @aantal)", con))
                {
                    cmd.CommandType = CommandType.Text;

                    cmd.Parameters.Add("@userid", SqlDbType.Int);
                    cmd.Parameters.Add("@soort", SqlDbType.VarChar);
                    cmd.Parameters.Add("@aantal", SqlDbType.Int);

                    cmd.Parameters["@userid"].Value = userid;
                    cmd.Parameters["@soort"].Value = soort;
                    cmd.Parameters["@aantal"].Value = aantal;

                    cmd.ExecuteNonQuery();

                    updateAvailableTickets(soort, aantal);

                    return true;
                }    
            }
        }

        public Boolean updateAvailableTickets(string soort, int aantal)
        {
            using (var con = new SqlConnection(_connectionString))
            {
                con.Open();

                using (var cmd = new SqlCommand("UPDATE Ticket SET aantal = (SELECT aantal FROM Ticket WHERE soort= @soort)-1 WHERE soort = @soort;", con))
                {
                    cmd.CommandType = CommandType.Text;

                    cmd.Parameters.Add("@soort", SqlDbType.VarChar);
                    cmd.Parameters.Add("@aantal", SqlDbType.Int);

                    cmd.Parameters["@soort"].Value = soort;
                    cmd.Parameters["@aantal"].Value = aantal;

                    cmd.ExecuteNonQuery();

                    return true;
                }
            }
        }

        public int lookupUserIdWithName(String username)
        {
            using (var con = new SqlConnection(_connectionString))
            {
                con.Open();

                using (var cmd = new SqlCommand("SELECT UserId FROM UserProfile WHERE username = @username", con))
                {
                    cmd.CommandType = CommandType.Text;

                    cmd.Parameters.Add("@username", SqlDbType.NVarChar);
                    cmd.Parameters["@username"].Value = username;

                    using (var reader = cmd.ExecuteReader())
                    {

                        if (reader.HasRows)
                        {
                            var lijst = new List<Ticket>();

                            if (reader.Read())
                            {
                                return Int32.Parse(reader["UserId"].ToString());
                            }
                        }

                        // internet says null!L!LL!L!
                        return 0;
                    }
                }
            }
        }

        public Boolean insertOrder(List<DagBestelling> order, String username)
        {
            int userid = lookupUserIdWithName(username);

            foreach (DagBestelling myOrder in order)
            {
                if (myOrder.aantal > 0)
                {
                    insertTicketLine(myOrder.soort, myOrder.aantal, userid);
                }
            }
            return true;
        }

        public IList<Ticket> getAllTicketsByUser(string username)
        {
            using (var con = new SqlConnection(_connectionString))
            {
                con.Open();

                int userid = lookupUserIdWithName(username);
                // Group by ticket: meerdere keren kopen = geeft totaal
                using (var cmd = new SqlCommand("SELECT o.userid, o.ticket, sum(o.aantal) as aantal, t.soort, t.prijs FROM Orders o INNER JOIN Ticket t ON t.ID = o.ticket WHERE o.userid = @userid GROUP BY o.userid, o.ticket, t.soort, t.prijs", con))
                {
                    cmd.CommandType = CommandType.Text;
                    cmd.Parameters.Add("@userid", SqlDbType.Int);
                    cmd.Parameters["@userid"].Value = userid;


                    using (var reader = cmd.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            var lijst = new List<Ticket>();

                            while (reader.Read())
                            {
                                Ticket newTicket = new Ticket();
                                newTicket.ID = Int32.Parse(reader["ticket"].ToString());
                                newTicket.soort = reader["soort"].ToString();
                                newTicket.aantal = Int32.Parse(reader["aantal"].ToString());

                                var prijs = reader["prijs"].ToString();
                                float totaalPrijs = float.Parse(prijs == "" ? "0" : prijs) * newTicket.aantal;
                                newTicket.prijs = totaalPrijs;

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