using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Powerup_Health : MonoBehaviour {

    public int HealAmount = 25;
    public GameObject Particles;

    private int triggerCount;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Player player = collision.GetComponent<Player>();
        if(player != null)
        {
            player.HealPlayer(HealAmount);
            if(Particles != null)
            {
                GameObject destroyParticles = Instantiate(Particles, transform.position, Quaternion.identity);
                Destroy(destroyParticles, 1f);
            }
            Destroy(this.gameObject);
        }
    }
}
