using Leaderboard_System__Dell_.Models;
using Microsoft.Extensions.Configuration;
using MySqlConnector;
using System.Collections.Generic;
using System.Data.Common;
using System.IO;
using System.Xml.Linq;

namespace Leaderboard_System__Dell_.DAL
{
    public class PointsDAL
    {
        private IConfiguration Configuration { get; }
        private MySqlConnection conn;

        public PointsDAL()
        {
            // Read ConnectionString from appsettings.json file
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json");

            Configuration = builder.Build();
            string strConn = Configuration.GetConnectionString("DellLeaderboardConnectionString");

            // Instantiate a MySqlConnection object with the Connection String read.
            conn = new MySqlConnection(strConn);
        }

        public List<Points> AssignLeaderboard(MySqlConnection connection)
        {
            List<Points> Assign = new List<Points>();

            using (MySqlCommand cmd = connection.CreateCommand())
            {
                cmd.CommandText = @"SELECT * FROM Points ORDER BY Score DESC LIMIT 3";

                connection.Open();

                using (MySqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Assign.Add(new Points
                        {
                            ID = reader.GetInt32(0),
                            Name = reader.GetString(1),
                            Score = reader.GetInt32(2)
                        });
                    }
                }

                connection.Close();
            }

            return Assign;
        }


        public int CreatePoints(Points p, MySqlConnection connection)
        {
            // Create a MySqlCommand object from connection object
            MySqlCommand cmd = conn.CreateCommand();

            // Specify an INSERT SQL statement
            cmd.CommandText = @"INSERT INTO Points (Name, Score) VALUES(@Name, @Score)";

            //Define the parameters used in the SQL statement
            cmd.Parameters.AddWithValue("@Name", p.Name);
            cmd.Parameters.AddWithValue("@Score", p.Score);

            // A connection to the database must be opened before any operations are made
            conn.Open();

            // Execute the INSERT SQL statement and retrieve the auto-generated ID
            cmd.ExecuteNonQuery();
            p.ID = (int)cmd.LastInsertedId;

            // A connection should be closed after operations
            conn.Close();

            return p.ID;
        }
    }
}
