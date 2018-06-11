using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Powerup : MonoBehaviour {

    public Transform Previewer;
    public Sprite PowerupIcon;

    Transform player;

    private void Start()
    {
        if(Previewer == null)
        {
            Debug.LogError ("No previewer assigned to power-up!");
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<Player> ())
        {
            if (GameMaster.HasPowerup)
            {
                return;
            }
            player = collision.transform;
            Transform spawnLocation = player.Find ("Power-Up_PreviewLocation");
            Previewer.localScale = new Vector3(-player.localScale.x, Previewer.localScale.y, Previewer.localScale.z);
            Transform preview = Instantiate (Previewer, spawnLocation.position, Previewer.rotation);
            preview.transform.parent = spawnLocation;
            preview.GetComponent<PowerupPreviewer> ().SetParent (player);
            Destroy (this.gameObject);
            GameMaster.ChangePowerup(PowerupIcon);
            GameMaster.HasPowerup = true;
        }
    }
}
