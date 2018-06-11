using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponClass : MonoBehaviour {

    int _id;
    string _name;
    string _description;
    decimal _price;

    public WeaponClass()
    {

    }
    public WeaponClass(int id, string name, string description, decimal price)
    {
        ID = id;
        Name = name;
        Description = description;
        Price = price;
    }

    public WeaponClass(string name, string description, decimal price)
    {
        Name = name;
        Description = description;
        Price = price;
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

    public string Name
    {
        get
        {
            return _name;
        }
        set
        {
            _name = value;
        }
    }

    public string Description
    {
        get
        {
            return _description;
        }
        set
        {
            _description = value;
        }
    }

    public decimal Price
    {
        get
        {
            return _price;
        }
        set
        {
            _price = value;
        }
    }

}
