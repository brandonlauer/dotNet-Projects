using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class PlayerNetworkSetup : NetworkBehaviour {
    //Script Author: Roberto Di Biase
    public override void OnStartLocalPlayer ()
    {
        GetComponent<Character2DUserControl>().enabled = true;
        GetComponent<Character2D>().enabled = true;
        GetComponent<Player>().enabled = true;
    }
}
