using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Data;
using System.Data.SqlClient;

public class Database : MonoBehaviour
{
    //Script Author: Roberto Di Biase

    private SqlConnection conn;
    public Database()
    {
        SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder();
        builder.DataSource = "spacewarsdb.database.windows.net";
        builder.UserID = "dbadmin";
        builder.Password = "Root12345";
        builder.InitialCatalog = "SpaceWars";
        conn = new SqlConnection(builder.ConnectionString);
        conn.Open();
        Debug.Log("Connected!!!");

    }
    public void AddUser(User u)
    {
        string sql = "INSERT INTO players (userName, email, password) VALUES (@username, @email, @password)";
        using (SqlCommand cmd = new SqlCommand(sql, conn))
        {
            cmd.Parameters.AddWithValue("@username", u.Username);
            cmd.Parameters.AddWithValue("@email", u.Email);
            cmd.Parameters.AddWithValue("@password", u.Password);
            int affectedRows = cmd.ExecuteNonQuery();
            Debug.Log(affectedRows + " rows inserted!");
        }
    }
    public List<User> LoadUsers()
    {
        List<User> result = new List<User>();
        using (SqlCommand command = new SqlCommand("SELECT * FROM players", conn))
        using (SqlDataReader reader = command.ExecuteReader())
        {
            while (reader.Read())
            {
                int id = (int)reader["id"];
                string userName = (string)reader["userName"];
                string email = (string)reader["email"];
                string password = (string)reader["password"];
                int totWins = (int)reader["totalWins"];
                decimal totMoney = (decimal)reader["totalMoney"];
                User u = new User(id, userName, email, password, totWins, totMoney);
                result.Add(u);
                Debug.LogFormat("{0},{1},{2},{3},{4},{5}", id, userName, email, password, totWins, totMoney);
            }
            return result;
        }
    }

    public List<WeaponClass> GetAllWeapons()
    {
        List<WeaponClass> result = new List<WeaponClass>();
        using (SqlCommand command = new SqlCommand("SELECT * FROM weapons", conn))
        using (SqlDataReader reader = command.ExecuteReader())
        {
            while (reader.Read())
            {
                int id = (int)reader["id"];
                string weaponName = (string)reader["name"];
                string weaponDescription = (string)reader["description"];
                decimal price = (decimal)reader["price"];
                WeaponClass w = new WeaponClass(id, weaponName, weaponDescription, price);
                result.Add(w);
                Debug.LogFormat("{0},{1},{2},{3}", id, weaponName, weaponDescription, price);
            }
            return result;
        }
    }

    internal void UpdateTotalMoney(User u)
    {
        string sql = "UPDATE players SET totalMoney = @TotMoney WHERE Id=@Id";
        SqlCommand cmd = new SqlCommand(sql, conn);
        cmd.Parameters.AddWithValue("@Id", u.ID);
        cmd.Parameters.AddWithValue("@TotMoney", u.TotalCoins);
        cmd.CommandType = CommandType.Text;
        cmd.ExecuteNonQuery();
    }

    public void AddWeapon(WeaponClass w)
    {
        string sql = "INSERT INTO weapons (name, description, price) VALUES (@name, @description, @price)";
        using (SqlCommand cmd = new SqlCommand(sql, conn))
        {
            cmd.Parameters.AddWithValue("@name", w.Name);
            cmd.Parameters.AddWithValue("@description", w.Description);
            cmd.Parameters.AddWithValue("@price", w.Price);
            int affectedRows = cmd.ExecuteNonQuery();
            Debug.Log(affectedRows + " rows inserted!");
        }
    }

    public List<WeaponPurchases> GetAllPurchaseForPlayer(int playerId)
    {
        List<WeaponPurchases> result = new List<WeaponPurchases>();
        using (SqlCommand command = new SqlCommand("SELECT * FROM weapon_purchases WHERE playerId = @playerId", conn))
        {
            command.Parameters.AddWithValue("@playerId", playerId);
            using (SqlDataReader reader = command.ExecuteReader())
            {

                while (reader.Read())
                {
                    int id = (int)reader["id"];
                    int playerID = (int)reader["playerId"];
                    int weaponId = (int)reader["waponId"];
                    WeaponPurchases w = new WeaponPurchases(id, playerID, weaponId);
                    result.Add(w);
                    //Debug.LogFormat("{0},{1},{2},{3}", id, playerID, weaponId);
                }
                return result;
            }
        }
        
    }

    public void AddPurchase(WeaponPurchases w)
    {
        string sql = "INSERT INTO weapon_purchases (playerId, waponId) VALUES (@playerId, @weaponId)";
        using (SqlCommand cmd = new SqlCommand(sql, conn))
        {
            cmd.Parameters.AddWithValue("@playerId", w.PlayerId);
            cmd.Parameters.AddWithValue("@weaponId", w.WeaponId);
            int affectedRows = cmd.ExecuteNonQuery();
            Debug.Log(affectedRows + " rows inserted!");
        }
    }
}
