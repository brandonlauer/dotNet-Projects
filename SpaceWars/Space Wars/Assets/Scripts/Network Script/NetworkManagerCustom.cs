using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.Networking.Types;
using UnityEngine.Networking.Match;
using UnityEngine.UI;

public class NetworkManagerCustom : NetworkManager {
    //Script Author: Roberto Di Biase

    public void StartUpHost()
    {
        SetPort();
        NetworkManager.singleton.StartHost();
    }

    public void JoinGame()
    {
        SetIPAddress();
        SetPort();
        NetworkManager.singleton.StartClient();

    }

    public void CancelJoin()
    {
        NetworkManager.singleton.StopClient();
        
    }

    public void EnableMackMaker()
    {
        NetworkManager.singleton.StartMatchMaker();
 
    }

    void SetIPAddress() 
    {
        string ipAddress = GameObject.Find("InputFieldIPAddress").transform.Find("Text").GetComponent<Text>().text;
        NetworkManager.singleton.networkAddress = ipAddress;
    }

    void SetPort()
    {
        NetworkManager.singleton.networkPort = 7777;
    }
    
    void OnLevelWasLoaded(int level)
    {
        if(level == 4)
        {
            SetupMenuSceneButtons();

        }
        else
        {
            SetupOtherSceneButtons();
        }
    }

    void SetupMenuSceneButtons()
    {
        //GameObject.Find("Host").GetComponent<Button>().onClick.RemoveAllListeners();
        GameObject.Find("Host").GetComponent<Button>().onClick.AddListener(StartUpHost);

        //GameObject.Find("LanJoin").GetComponent<Button>().onClick.RemoveAllListeners();
        GameObject.Find("LanJoin").GetComponent<Button>().onClick.AddListener(JoinGame);

        //GameObject.Find("CancelJoin").GetComponent<Button>().onClick.AddListener(CancelJoin);
        //GameObject.Find("OnlineHost").GetComponent<Button>().onClick.RemoveAllListeners();
        GameObject.Find("OnlineHost").GetComponent<Button>().onClick.AddListener(EnableMackMaker);
    }

    void SetupOtherSceneButtons()
    {
        GameObject.Find("DisconnectButton").GetComponent<Button>().onClick.RemoveAllListeners();
        GameObject.Find("DisconnectButton").GetComponent<Button>().onClick.AddListener(NetworkManager.singleton.StopHost);
    }
}
