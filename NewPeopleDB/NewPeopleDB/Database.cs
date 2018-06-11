using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewPeopleDB
{
    class Database
    {
        public void addPerson(Person p)
        {
            try
            {
                SqlConnection connection = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\Brandon\Documents\NewPeopleDB.mdf;Integrated Security=True;Connect Timeout=30");
                connection.Open();
                SqlCommand insertCommand = new SqlCommand("INSERT INTO People (Name, Age, Height) VALUES (@Name, @Age, @Height)", connection);
                insertCommand.Parameters.AddWithValue("@Name", p.name);
                insertCommand.Parameters.AddWithValue("@Age", p.age);
                insertCommand.Parameters.AddWithValue("@Height", p.height);
                insertCommand.ExecuteNonQuery();
            }catch (SqlException ex)
            {
                Console.WriteLine(ex);
            }   
        }

        public List<Person> getAllPeople()
        {
            List<Person> people = new List<Person>();
            SqlConnection connection = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\Brandon\Documents\NewPeopleDB.mdf;Integrated Security=True;Connect Timeout=30");
            connection.Open();

            SqlCommand command = new SqlCommand("SELECT * FROM People", connection);
            using (SqlDataReader reader = command.ExecuteReader())
            {
                // while there is another record present
                while (reader.Read())
                {
                    int id = int.Parse(reader[0].ToString());
                    string name = reader[1].ToString();
                    int age = int.Parse(reader[2].ToString());
                    double height = double.Parse(reader[3].ToString());
                    Person p = new Person(id, name, age, height);
                    people.Add(p);
                }
            }
            return people;
        }

        public void updatePerson(Person p)
        {
            SqlConnection connection = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\Brandon\Documents\NewPeopleDB.mdf;Integrated Security=True;Connect Timeout=30");
            connection.Open();

            SqlCommand updateCommand = new SqlCommand("UPDATE People SET Name = @name, age = @age, Height = @height " +
                "WHERE id = @id", connection);
            updateCommand.Parameters.AddWithValue("@id", p.id);
            updateCommand.Parameters.AddWithValue("@name", p.name);
            updateCommand.Parameters.AddWithValue("@age", p.age);
            updateCommand.Parameters.AddWithValue("@height", p.height);
            updateCommand.ExecuteNonQuery();
        }

        public void deletePerson(int id)
        {
            SqlConnection connection = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\Brandon\Documents\NewPeopleDB.mdf;Integrated Security=True;Connect Timeout=30");
            connection.Open();

            SqlCommand deleteCommand = new SqlCommand("DELETE FROM PEOPLE WHERE Id = " + id, connection);
            deleteCommand.ExecuteNonQuery();
        }
    }
}
