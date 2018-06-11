using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;

public class GameMaster : NetworkBehaviour {

    public Transform PowerupIconBox;
    public static bool HasPowerup = false;
    public static Sprite EmptyPowerup;
    public float TimeToSpawnPowerup = 20;
    public Transform PowerupSpawnLocations;
    public List<GameObject> Powerups;

    private float powerupCountdown;
    private static Transform icon;
    private List<Transform> powerupSpawnPoints = new List<Transform>();

    private void Awake()
    {
        powerupCountdown = TimeToSpawnPowerup;
        if(PowerupIconBox == null)
        {
            Debug.LogError("No location for power-up icon assigned to the Game Master");
        }
        else
        {
            icon = PowerupIconBox;
            EmptyPowerup = PowerupIconBox.GetComponent<SpriteRenderer>().sprite;
        }

        if(PowerupSpawnLocations == null)
        {
            Debug.LogError("No Power-up spawn points assigned to Game Master!");
        }
        else
        {
            foreach (Transform spawnPoint in PowerupSpawnLocations)
            {
                powerupSpawnPoints.Add(spawnPoint);
            }
        }
    }

    public static void KillPlayer(Player player, bool respawn)
    {
        Destroy(player.gameObject);
        SceneManager.LoadScene("Result");
    }

    public static void ChangePowerup (Sprite powerupIcon)
    {
        icon.GetComponent<SpriteRenderer>().sprite = powerupIcon;
    }

    private void FixedUpdate()
    {
        if (!NetworkServer.active)
        {
            return;
        }
        powerupCountdown -= Time.deltaTime;
        if(powerupCountdown <= 0)
        {
            SpawnPowerup();
            powerupCountdown = TimeToSpawnPowerup;
        }
    }

    void SpawnPowerup()
    {
        Transform spawnPoint = powerupSpawnPoints[Random.Range(0, powerupSpawnPoints.Count)];

        GameObject powerup = Powerups[Random.Range (0, Powerups.Count)];

        GameObject power = Instantiate (powerup, spawnPoint.position, Quaternion.identity);
        NetworkServer.Spawn(power);
    }
}
