using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Data.SqlClient;
using UnityEngine.UI;
using System.Text.RegularExpressions;
using System;
using UnityEngine.SceneManagement;

public class LoginUser : MonoBehaviour {

    //Script Author: Roberto Di Biase

    public InputField userNameInput;
    public InputField passwordInput;

    public Text mesTitle;
    public Text mesMessage;
    public Button mesButton;
    private bool userExists = false;
    GameObject login;
    GameObject errorMessage;
    void Awake()
    {
        login = GameObject.FindWithTag("login");
        errorMessage = GameObject.FindWithTag("error");
    }

    public void Login()
    {
        Regex passRegEx = new Regex("(?=.*[0-9])(?=.*[a-z])(?=.*[A-Z]).{8,}");
        //Connect to database
        Database db = new Database();

        //Get Input from game
        string userName = userNameInput.text;
        Debug.Log(userName);
        string password = passwordInput.text;
        Match passMatch = passRegEx.Match(password);

        //Load all users from Db
        List<User> users = db.LoadUsers();
        foreach (User uu in users)
        {
            Debug.LogFormat("{0},{1},{2},{3}", uu.ID, uu.Username, uu.Email, uu.Password);
        }

        if (userName == "")
        {
            login.SetActive(false);
            errorMessage.SetActive(true);

            mesTitle.text = "Missing information";
            mesMessage.text = "Please enter your username.";
            mesButton.onClick.AddListener(LoadBack);
            return;
        }
        else if (password == "")
        {
            login.SetActive(false);
            errorMessage.SetActive(true);

            mesTitle.text = "Missing information";
            mesMessage.text = "Please enter your password.";
            mesButton.onClick.AddListener(LoadBack);
            return;
        }
        

        foreach (User u in users)
        {
            if (userName.Equals(u.Username,StringComparison.InvariantCultureIgnoreCase))
            {
                userExists = true;
                if (password.Equals(u.Password))
                {
                    //LOGIN
                    login.SetActive(false);
                    errorMessage.SetActive(true);

                    mesTitle.text = "Login Successful";
                    mesMessage.text = "Welcome back to SPACE WARS!";
                    GlobalUserStats.logUser = u;
                    GlobalUserStats.logged = true;
                    mesButton.onClick.AddListener(LoadByIndex);
                    return;
                }
                else
                {
                    //Wrong Password
                    login.SetActive(false);
                    errorMessage.SetActive(true);

                    mesTitle.text = "Wrong Password!";
                    mesMessage.text = "Wrong password. Please try again.";
                    mesButton.onClick.AddListener(LoadBack);
                    Clear(passwordInput);
                    return;
                }               
            }
        }

        if (!userExists)
        {
            //USERNAME NOT REGISTER
            login.SetActive(false);
            errorMessage.SetActive(true);

            mesTitle.text = "No record found";
            mesMessage.text = "This username is not register yet.\nPlease verify the username or register if you haven't yet";
            mesButton.onClick.AddListener(LoadBack);
            Clear(userNameInput);
            Clear(passwordInput);
            return;
        }
    }

    public void LoadByIndex()
    {
        SceneManager.LoadScene(0);
    }
    public void LoadBack()
    {
        login.SetActive(true);
        errorMessage.SetActive(false);
    }

    public void Clear(InputField inputfield)
    {
        inputfield.Select();
        inputfield.text = "";
    }
}
