using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class User : MonoBehaviour
{
    //Script Author: Roberto Di Biase

    //======CLASS USER======
    int _id;
    string _username;
    string _email;
    string _password;
    int _totWins;
    decimal _totCoins;


    public User()
    {

    }
    //Constructor with ID
    public User(int id, string username, string email, string password, int totWins, decimal totCoins)
    {
        ID = id;
        Username = username;
        Email = email;
        Password = password;
        TotWins = totWins;
        TotalCoins = totCoins;
    }

    public User(int id, string username, string email, string password)
    {
        ID = id;
        Username = username;
        Email = email;
        Password = password;
    }

    //Constructor without ID
    public User(string username, string email, string password)
    {
        Username = username;
        Email = email;
        Password = password;
    }

    public int ID
    {
        get
        {
            return _id;
        }
        set
        {
            _id = value;
        }
    }//END ID class

    public string Username
    {
        get
        {
            return _username;
        }
        set
        {

            _username = value;
        }
    }//END NAME class

    public string Email
    {
        get
        {
            return _email;
        }
        set
        {
            //TO CHECK EMAIL FORMAT
            _email = value;
        }
    }//END EMAIL class

    public string Password
    {
        get
        {
            return _password;
        }
        set
        {

            _password = value;
        }
    }//END PASSWORD class

    public int TotWins
    {
        get
        {
            return _totWins;
        }
        set
        {
            _totWins = value;
        }
    }

    public decimal TotalCoins
    {
        get
        {
            return _totCoins;
        }
        set
        {
            _totCoins = value;
        }
    }
}
