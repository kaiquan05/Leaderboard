using Leaderboard_System__Dell_.Models;
using System.ComponentModel.DataAnnotations;
using System.Data.SqlClient;
using System.Xml.Linq;
namespace Leaderboard_System__Dell_.DAL
{
	public class PointsDAL
	{
		private IConfiguration Configuration { get; }
		private SqlConnection conn;

		public PointsDAL()
		{
			//Read ConnectionString from appsettings.json file
			var builder = new ConfigurationBuilder()
				.SetBasePath(Directory.GetCurrentDirectory())
				.AddJsonFile("appsettings.json");

			Configuration = builder.Build();
			string strConn = Configuration.GetConnectionString("DellLeaderboardConnectionString");

			//Instantiate a SqlConnection object with the
			//Connection String read.
			conn = new SqlConnection(strConn);

		}

		public List<Points> AssignLeaderboard()
		{
			List<Points> Assign = new List<Points>();


			//Create a SqlCommand object from connection object
			SqlCommand cmd = conn.CreateCommand();
			//Specify the SELECT SQL statement 
			cmd.CommandText = @"SELECT TOP 3 * FROM Points ORDER BY Score DESC";
			//Open a database connection
			conn.Open();
			//Execute the SELECT SQL through a DataReader
			SqlDataReader reader = cmd.ExecuteReader();
			//Read all records until the end
			Points? points = null;
			while (reader.Read())
			{
				Assign.Add(new Points
				{
					ID = reader.GetInt32(0),
					Name = reader.GetString(1),
					Score = reader.GetInt32(2)
				});
			}
			reader.Close();
			conn.Close();
			return Assign;
		}
		public int CreatePoints(Points p)
		{
			//Create a SqlCommand object from connection object
			SqlCommand cmd = conn.CreateCommand();
			//Specify an INSERT SQL statement which will
			//return the auto-generated StaffID after insertion
			cmd.CommandText = @"INSERT INTO Points (Name, Score) 
                          OUTPUT INSERTED.ID
                          VALUES(@Name,@Score)";
			//Define the parameters used in SQL statement, value for each parameter
			//is retrieved from respective class's property.
			cmd.Parameters.AddWithValue("@Name", p.Name);
			cmd.Parameters.AddWithValue("@Score", p.Score);
			//A connection to database must be opened before any operations made.
			conn.Open();
			//ExecuteScalar is used to retrieve the auto-generated
			//StaffID after executing the INSERT SQL statement
			p.ID = (int)cmd.ExecuteScalar();
			//A connection should be closed after operations.
			conn.Close();
			return p.ID;
		}
	}
}
