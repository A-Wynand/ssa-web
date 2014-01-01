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
    // Srly i'm foaming at the mouth here
    /// <summary>
    ///  WTF you guys need learn proper arch xD
    /// THIS mai frend is how the teachers teach it, can't do shit about it
    /// fucking midget trying to fuck everyone over so no company hires us ;( save me master :p
    /// </summary>
    public class BandSQLRepository
    {
        private readonly string _connectionString;

        public BandSQLRepository()
        {
            _connectionString = ConfigurationManager.ConnectionStrings["FestivalConString"].ConnectionString;
        }

        // WTFTFTFTSTFWTFTF
        public IList<Band> GetAllBands()
        {
            using (var con = new SqlConnection(_connectionString))
            {
                con.Open();

                using (var cmd = new SqlCommand("SELECT * FROM Band", con))
                {
                    cmd.CommandType = CommandType.Text;

                    // This is what stores your data from the internets...
                    using (var reader = cmd.ExecuteReader())
                    {

                        if (reader.HasRows)
                        {
                            var lijst = new List<Band>();

                            while (reader.Read())
                            {
                                Band newBand = new Band();
                                newBand.ID = Int32.Parse(reader["ID"].ToString());
                                newBand.naam = reader["naam"].ToString();
                                newBand.omschrijving = reader["omschrijving"].ToString();
                                newBand.fotoUri = reader["fotoUri"].ToString();
                                
                                lijst.Add(newBand);
                            }

                            return lijst; // lol
                        }

                        // internet says null!L!LL!L!
                        return null;
                    }

                }
            }
        }


        public Band getBandSpecific(int id)
        {
            using (var con = new SqlConnection(_connectionString))
            {
                con.Open();

                using (var cmd = new SqlCommand("SELECT * FROM Band WHERE ID = @ID", con))
                {
                    cmd.CommandType = CommandType.Text;
                    cmd.Parameters.Add("@ID", SqlDbType.Int);
                    cmd.Parameters["@ID"].Value = id;

                    // This is what stores your data from the internets...
                    using (var reader = cmd.ExecuteReader())
                    {

                        if (reader.HasRows)
                        {

                            if (reader.Read())
                            {
                                Band newBand = new Band();
                                newBand.ID = Int32.Parse(reader["ID"].ToString());
                                newBand.naam = reader["naam"].ToString();
                                newBand.omschrijving = reader["omschrijving"].ToString();
                                newBand.fotoUri = reader["fotoUri"].ToString();

                                return newBand;
                            }
                        }

                        // internet says null!L!LL!L!
                        return null;
                    }

                }
            }
        }


    }
}