using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Data.SqlClient;
public class ShopManager : MonoBehaviour {
    [Header("Total coins")]
    public Text totalCoin;

    [Header("Laser Gun")]
    //LaserGun 
    public Text laserPrice;
    public Text laserName;
    public Text laserDescription;
    [Header("Button")]
    //Laser gun button
    public Button laserGun;
    public Text laserGunText;

    [Header("Weapon 2")]
    //Weapon 2
    public Text weapon2Price;
    public Text weapon2Name;
    public Text weapon2Description;
    [Header("Button")]
    //Weapon 2 button
    public Button weapon2Gun;
    public Text weapon2GunText;

    [Header("Weapon 3")]
    //Weapon 3
    public Text weapon3Price;
    public Text weapon3Name;
    public Text weapon3Description;
    [Header("Button")]
    //Weapon 3 button
    public Button weapon3Gun;
    public Text weapon3GunText;

    [Header("Message Box")]
    //Message box
    public Text mesTitle;
    public Text mesMessage;
    public Button mesButton;


    //Database and lists
    Database db = new Database();
    List<WeaponClass> weapons = new List<WeaponClass>();
    bool weaponbought = false;
    List<WeaponPurchases> purchases = new List<WeaponPurchases>();

    GameObject shopPanel;
    GameObject errorMessage;



    void Awake()
    {
        shopPanel = GameObject.FindWithTag("shopPanel");
        errorMessage = GameObject.FindWithTag("error");
        errorMessage.SetActive(false);
        totalCoin.text = GlobalUserStats.logUser.TotalCoins.ToString();

        purchases = db.GetAllPurchaseForPlayer(GlobalUserStats.logUser.ID);
        laserGun.onClick.AddListener(BuyLasergun);
        weapon2Gun.onClick.AddListener(BuyWeapon2);
        weapon3Gun.onClick.AddListener(BuyWeapon3);

        weapons = db.GetAllWeapons();

        laserPrice.text = weapons[0].Price.ToString();
        laserName.text = weapons[0].Name.ToString();
        laserDescription.text = weapons[0].Description.ToString();

        weapon2Price.text = weapons[1].Price.ToString();
        weapon2Name.text = weapons[1].Name.ToString();
        weapon2Description.text = weapons[1].Description.ToString();

        weapon3Price.text = weapons[2].Price.ToString();
        weapon3Name.text = weapons[2].Name.ToString();
        weapon3Description.text = weapons[2].Description.ToString();
        foreach (WeaponPurchases p in purchases)
        {
            switch (p.WeaponId)
            {
                case 1:
                    laserGunText.text = "Equip";
                    laserGun.onClick.RemoveListener(BuyLasergun);
                    laserGun.onClick.AddListener(EquipWeapon);
                    break;
                case 2:
                    weapon2GunText.text = "Equip";
                    weapon2Gun.onClick.RemoveListener(BuyLasergun);
                    weapon2Gun.onClick.AddListener(EquipWeapon);
                    break;
                case 3:
                    weapon3GunText.text = "Equip";
                    weapon3Gun.onClick.RemoveListener(BuyLasergun);
                    weapon3Gun.onClick.AddListener(EquipWeapon);
                    break;
                default:
                    break;
            }
        }


    }

	
    public void BuyLasergun()
    {
        decimal totCoin;
        decimal.TryParse(totalCoin.text,out totCoin);
        decimal laserP;
        decimal.TryParse(laserPrice.text, out laserP);

        if(totCoin <= 0 || totCoin < laserP)
        {
            laserGun.onClick.RemoveListener(BuyLasergun);
            laserGun.onClick.AddListener(NoCoin);
        }
        else
        {
            try
            {
                totCoin = totCoin - laserP;
                laserGunText.text = "Equip";
                laserGun.onClick.RemoveListener(BuyLasergun);
                laserGun.onClick.AddListener(EquipWeapon);
                GlobalUserStats.logUser.TotalCoins = totCoin;
                db.UpdateTotalMoney(GlobalUserStats.logUser);
                WeaponPurchases w = new WeaponPurchases(GlobalUserStats.logUser.ID, 1);
                db.AddPurchase(w);
                shopPanel.SetActive(false);
                errorMessage.SetActive(true);
                mesTitle.text = "Weapon Buy";
                mesMessage.text = "You have successfully bought this weapon.\n To equip it click the EQUIP button.";
                mesButton.onClick.AddListener(LoadBack);
                totalCoin.text = totCoin + "";
            }catch (SqlException ex)
            {
                shopPanel.SetActive(false);
                errorMessage.SetActive(true);
                mesTitle.text = "SQL Error";
                mesMessage.text = ex.Message;
            }
            
        }

    }

    public void BuyWeapon2()
    {
        decimal totCoin;
        decimal.TryParse(totalCoin.text, out totCoin);
        decimal laserP;
        decimal.TryParse(laserPrice.text, out laserP);

        if (totCoin <= 0 || totCoin < laserP)
        {
            weapon2Gun.onClick.RemoveListener(BuyWeapon2);
            weapon2Gun.onClick.AddListener(NoCoin);
        }
        else
        {
            try
            {
                totCoin = totCoin - laserP;
                weapon2GunText.text = "Equip";
                weapon2Gun.onClick.RemoveListener(BuyWeapon2);
                weapon2Gun.onClick.AddListener(EquipWeapon);
                GlobalUserStats.logUser.TotalCoins = totCoin;
                db.UpdateTotalMoney(GlobalUserStats.logUser);
                WeaponPurchases w = new WeaponPurchases(GlobalUserStats.logUser.ID, 2);
                db.AddPurchase(w);
                shopPanel.SetActive(false);
                errorMessage.SetActive(true);
                mesTitle.text = "Weapon Buy";
                mesMessage.text = "You have successfully bought this weapon.\n To equip it click the EQUIP button.";
                mesButton.onClick.AddListener(LoadBack);
                totalCoin.text = totCoin + "";
            }
            catch (SqlException ex)
            {
                shopPanel.SetActive(false);
                errorMessage.SetActive(true);
                mesTitle.text = "SQL Error";
                mesMessage.text = ex.Message;
            }

        }

    }


    public void BuyWeapon3()
    {
        decimal totCoin;
        decimal.TryParse(totalCoin.text, out totCoin);
        decimal laserP;
        decimal.TryParse(laserPrice.text, out laserP);

        if (totCoin <= 0 || totCoin < laserP)
        {
            weapon3Gun.onClick.RemoveListener(BuyWeapon3);
            weapon3Gun.onClick.AddListener(NoCoin);
        }
        else
        {
            try
            {
                totCoin = totCoin - laserP;
                weapon3GunText.text = "Equip";
                weapon3Gun.onClick.RemoveListener(BuyWeapon3);
                weapon3Gun.onClick.AddListener(EquipWeapon);
                GlobalUserStats.logUser.TotalCoins = totCoin;
                db.UpdateTotalMoney(GlobalUserStats.logUser);
                WeaponPurchases w = new WeaponPurchases(GlobalUserStats.logUser.ID, 1);
                db.AddPurchase(w);
                shopPanel.SetActive(false);
                errorMessage.SetActive(true);
                mesTitle.text = "Weapon Buy";
                mesMessage.text = "You have successfully bought this weapon.\n To equip it click the EQUIP button.";
                mesButton.onClick.AddListener(LoadBack);
                totalCoin.text = totCoin + "";
            }
            catch (SqlException ex)
            {
                shopPanel.SetActive(false);
                errorMessage.SetActive(true);
                mesTitle.text = "SQL Error";
                mesMessage.text = ex.Message;
            }

        }

    }

    public void NoCoin()
    {
        laserGun.enabled = false;
        laserDescription.text = "Not enough coins to buy this weapon.";
    }

    public void EquipWeapon()
    {
        //CODE TO EQUIP WEAPON
    }


    public void LoadBack()
    {
        shopPanel.SetActive(true);
        errorMessage.SetActive(false);
    }

}
