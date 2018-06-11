using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quiz2Passengers
{
    class Database
    {
        private SqlConnection connection = new SqlConnection ();

        public Database()
        {
            connection = new SqlConnection (@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=D:\Brandon Lauer\dotNET\Quiz2Passengers\Quiz2Passengers\Quiz2PassengersDB.mdf;Integrated Security=True;Connect Timeout=30");
            connection.Open ();
        }

        public void AddPassenger(Passenger p)
        {
            try
            {
                string sql = "INSERT INTO Passengers (Name, Passport, Destination, DepartureDateTime) VALUES (@Name, @Passport, @Destination, @DepartureDateTime)";
                SqlCommand insertCommand = new SqlCommand (sql, connection);
                insertCommand.Parameters.AddWithValue ("@Name", p.Name);
                insertCommand.Parameters.AddWithValue ("@Passport", p.Passport);
                insertCommand.Parameters.AddWithValue ("@Destination", p.Destination);
                insertCommand.Parameters.AddWithValue ("@DepartureDateTime", p.DepartureDateTime);
                insertCommand.ExecuteNonQuery ();
            }catch(SqlException e)
            {
                Console.WriteLine (e);
            }
        }

        public List<Passenger> GetAllPassengers()
        {
            List<Passenger> result = new List<Passenger> ();
            string sql = "SELECT * FROM Passengers ORDER BY " + Globals.sortingMode;
            using (SqlCommand command = new SqlCommand (sql, connection))
            using(SqlDataReader reader = command.ExecuteReader ())
            {
                while (reader.Read ())
                {
                    int id = (int)reader["Id"];
                    string name = (string)reader["Name"];
                    string passport = (string)reader["Passport"];
                    string destination = (string)reader["Destination"];
                    DateTime departureDateTime = (DateTime)reader["DepartureDateTime"];
                    bool hasDeparted = departureDateTime < DateTime.Now;
                    Passenger pass = new Passenger { Id = id, Name = name, Passport = passport, Destination = destination, DepartureDateTime = departureDateTime, HasDeparted = hasDeparted };
                    result.Add (pass);
                }
            }
            return result;
        }

        public void UpdatePassenger(Passenger p)
        {
            string sql = "UPDATE Passengers SET Name = @Name, Passport = @Passport, Destination = @Destination, DepartureDateTime = @DepartureDateTime WHERE Id = @Id";
            SqlCommand updateCommand = new SqlCommand (sql, connection);
            updateCommand.Parameters.AddWithValue ("@Id", p.Id);
            updateCommand.Parameters.AddWithValue ("@Name", p.Name);
            updateCommand.Parameters.AddWithValue ("@Passport", p.Passport);
            updateCommand.Parameters.AddWithValue ("@Destination", p.Destination);
            updateCommand.Parameters.AddWithValue ("@DepartureDateTime", p.DepartureDateTime);
            updateCommand.ExecuteNonQuery ();
        }

        public void DeletePassengerById(int id)
        {
            string sql = "DELETE FROM Passengers WHERE Id = " + id;
            SqlCommand deleteCommand = new SqlCommand (sql, connection);
            deleteCommand.ExecuteNonQuery ();
        }
    }
}
