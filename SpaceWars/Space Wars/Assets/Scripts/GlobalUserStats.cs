using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GlobalUserStats : MonoBehaviour {

    public static User logUser;
    public static bool logged=false;
    public static WeaponClass currentWeapon;

    public Text userNameAvatar;
    public Text totalCoinText;
    void Awake()
    {
        if (!logged)
        {
            GameObject.Find("UserAvatar").SetActive(false);
            GameObject.Find("TotalCoin").SetActive(false);
            GameObject.Find("ArenaBrawlButton").SetActive(false);
            GameObject.Find("ShopButton").SetActive(false);
        }
        else
        {
            GameObject.Find("UserAvatar").SetActive(true);
            GameObject.Find("TotalCoin").SetActive(true);
            GameObject.Find("ArenaBrawlButton").SetActive(true);
            GameObject.Find("ShopButton").SetActive(true);
            userNameAvatar.text = logUser.Username;
            totalCoinText.text = logUser.TotalCoins.ToString();
        }
    }
}
