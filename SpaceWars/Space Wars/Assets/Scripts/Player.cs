using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class Player : NetworkBehaviour {

    [HideInInspector] [SyncVar(hook = "OnNameChange")] public string username;
    public StatusIndicator StatusBar;
    public int MaxHealth = 100;
    [HideInInspector][SyncVar(hook ="OnChangeHealth")]
    public int curHealth;
    private Animator anim;

    private void Awake()
    {
        if(StatusBar == null)
        {
            Debug.LogError("No status indicator on player!");
        }
        curHealth = MaxHealth;
        anim = GetComponent<Animator> ();
    }

    public override void OnStartClient()
    {
        username = GlobalUserStats.logUser.Username;
    }

    public void OnNameChange(string newValue)
    {
        StatusBar.SetUsername(newValue);
        Debug.Log("Username changed!");
    }

    public void DamagePlayer(int damage)
    {
        if (!isServer)
        {
            return;
        }
        curHealth -= damage;
        if (curHealth <= 0)
        {
            GameMaster.KillPlayer(this, true);
        }
    }

    void OnChangeHealth(int health)
    {
        StatusBar.SetHealth(health, MaxHealth);
    }

    public void HealPlayer(int health)
    {
        curHealth += health;
        if(curHealth > MaxHealth)
        {
            curHealth = MaxHealth;
        }
    }

    public void StunPlayer(float stunTime)
    {
        StartCoroutine (PlayerStunned (stunTime));
    }

    IEnumerator PlayerStunned(float stunTime)
    {
        anim.SetBool ("Stunned", true);
        GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
        GetComponent<Character2D>().Stunned = true;
        GetComponent<Character2DUserControl>().enabled = false;

        yield return new WaitForSeconds (stunTime);

        GetComponent<Character2D> ().Stunned = false;
        GetComponent<Character2DUserControl>().enabled = true;
        anim.SetBool ("Stunned", false);
    }
}
