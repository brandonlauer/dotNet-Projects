using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponPurchases : MonoBehaviour {

    int _id;
    int _playerId;
    int _weaponId;

    public WeaponPurchases()
    {
       
    }
    public WeaponPurchases(int id, int playerId, int weaponId)
    {
        ID = id;
        PlayerId = playerId;
        WeaponId = weaponId;
    }

    public WeaponPurchases(int playerId, int weaponId)
    {
        PlayerId = playerId;
        WeaponId = weaponId;
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
    }

    public int PlayerId
    {
        get
        {
            return _playerId;
        }
        set
        {
            _playerId = value;
        }
    }

    public int WeaponId
    {
        get
        {
            return _weaponId;
        }
        set
        {
            _weaponId = value;
        }
    }
}
