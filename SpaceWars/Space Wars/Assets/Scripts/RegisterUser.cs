using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Data;
using System.Data.SqlClient;
using System;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Text.RegularExpressions;

public class RegisterUser : MonoBehaviour
{
    //Script Author: Roberto Di Biase
    
    public InputField userNameInput;
    public InputField emailInput;
    public InputField passwordInput;
    public InputField confirmPassInput;

    //Message box
    public Text mesTitle;
    public Text mesMessage;
    public Button mesButton;

    GameObject login;
    GameObject errorMessage;
    
    void Awake()
    {
        login = GameObject.FindWithTag("login");
        errorMessage = GameObject.FindWithTag("error");
        errorMessage.SetActive(false);
    }
    public void Registration()
    {
        Regex VALID_EMAIL_ADDRESS_REGEX =new Regex("^[A-Za-z0-9._%+-]+@[A-Za-z0-9.-]+\\.[A-Za-z]{2,6}$");
        Regex passRegEx = new Regex("(?=.*[0-9])(?=.*[a-z])(?=.*[A-Z]).{8,}");
        //Connect to database
        Database db = new Database();

        //Get Input from game
        string userName = userNameInput.text;
        string email = emailInput.text;
        string password = passwordInput.text;
        string confirmPass = confirmPassInput.text;
        Match emailMatch = VALID_EMAIL_ADDRESS_REGEX.Match(email);
        Match passMatch = passRegEx.Match(password);

        //Load all users from Db
        List<User> users = db.LoadUsers();
        
        if(userName == "")
        {
            login.SetActive(false);
            errorMessage.SetActive(true);
            
            mesTitle.text = "Missing information";
            mesMessage.text = "Please enter your username.";
            mesButton.onClick.AddListener(LoadBack);
            
            return;
        }
        else if (userName.Length < 2 || userName.Length > 15)
        {
            login.SetActive(false);
            errorMessage.SetActive(true);

            mesTitle.text = "Input Error";
            mesMessage.text = "Your username must be between 2 and 15 character long.";
            mesButton.onClick.AddListener(LoadBack);
            return;
        }
        else if(email == "")
        {
            login.SetActive(false);
            errorMessage.SetActive(true);

            mesTitle.text = "Missing information";
            mesMessage.text = "Please enter your e-mail address.";
            mesButton.onClick.AddListener(LoadBack);
            return;
        }
        else if (!emailMatch.Success)
        {
            login.SetActive(false);
            errorMessage.SetActive(true);

            mesTitle.text = "Incorect email format";
            mesMessage.text = "Please enter a valid e-mail address.";
            mesButton.onClick.AddListener(LoadBack);
            return;
        }
        else if(password == "")
        {
            login.SetActive(false);
            errorMessage.SetActive(true);

            mesTitle.text = "Missing information";
            mesMessage.text = "Please enter your password.";
            mesButton.onClick.AddListener(LoadBack);
            return;
        }
        else if (!passMatch.Success)
        {
            login.SetActive(false);
            errorMessage.SetActive(true);

            mesTitle.text = "Invalid password";
            mesMessage.text = "Weak password. Must contain:\n- 8 Characters or more.\n- 1 or more uppercase characters.\n- 1 or more numeric characters.";
            mesButton.onClick.AddListener(LoadBack);
            return;
        }
        else if (confirmPass == "")
        {
            login.SetActive(false);
            errorMessage.SetActive(true);

            mesTitle.text = "Missing information";
            mesMessage.text = "Please confirm your password.";
            mesButton.onClick.AddListener(LoadBack);
            return;
        }
        else if (!password.Equals(confirmPass))
        {
            login.SetActive(false); 
            errorMessage.SetActive(true);

            mesTitle.text = "Invalid password";
            mesMessage.text = "Passwords don't match.";
            mesButton.onClick.AddListener(LoadBack);
            return;
        }

        //User and Email validation
        foreach (User u in users)
        {
            if (u.Username.Equals(userName,StringComparison.InvariantCultureIgnoreCase))
            {
                login.SetActive(false);
                errorMessage.SetActive(true);

                mesTitle.text = "Resistration error";
                mesMessage.text = "This username is already taken.\n\nYour username must be unique.";
                mesButton.onClick.AddListener(LoadBack);
                return;
            }
            else if (u.Email.Equals(email))
            {
                login.SetActive(false);
                errorMessage.SetActive(true);

                mesTitle.text = "Resistration error";
                mesMessage.text = "This email is already registered.";
                mesButton.onClick.AddListener(LoadBack);
                return;
            }
        }

        User uu = new User(userName, email, password);
        db.AddUser(uu);
        login.SetActive(false);
        errorMessage.SetActive(true);
        mesTitle.text = "Successfully registered!";
        mesMessage.text = "You are now part of Space Wars family.\nNow you can login and enjoy epic battles with friends.";

        mesButton.onClick.AddListener(LoadBack);
        Clear(userNameInput);
        Clear(emailInput);
        Clear(passwordInput);
        Clear(confirmPassInput);
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