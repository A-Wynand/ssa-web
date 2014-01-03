using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;


namespace Festival.Models.DAL
{
    public class BandSQLRepository
    {
        private readonly string _connectionString;

        public BandSQLRepository()
        {
            _connectionString = ConfigurationManager.ConnectionStrings["FestivalConString"].ConnectionString;
        }

        public IList<Band> GetAllBands()
        {
            using (var con = new SqlConnection(_connectionString))
            {
                con.Open();

                using (var cmd = new SqlCommand("SELECT * FROM Band", con))
                {
                    cmd.CommandType = CommandType.Text;

                    // Store the data from the DB...
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
                                newBand.tijdslot = reader["tijdslot"].ToString();
                                newBand.podium = reader["podium"].ToString();
                                
                                lijst.Add(newBand);
                            }

                            return lijst;
                        }
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

                    // Store the data from the DB...
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
                                newBand.tijdslot = reader["tijdslot"].ToString();
                                newBand.podium = reader["podium"].ToString();

                                return newBand;
                            }
                        }

                        return null;
                    }

                }
            }
        }


    }
}