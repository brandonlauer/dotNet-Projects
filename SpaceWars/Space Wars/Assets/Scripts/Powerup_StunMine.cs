using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Powerup_StunMine : MonoBehaviour {

    public float StunDuration = 2;
    public int Damage = 10;
    public Transform LightningEffect;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.GetComponent<Player> ())
        {
            collision.gameObject.GetComponent<Player> ().StunPlayer (StunDuration);
            collision.gameObject.GetComponent<Player> ().DamagePlayer (Damage);
            Destroy (this.gameObject);
            Transform effect = Instantiate(LightningEffect, 
                new Vector2(collision.transform.position.x, collision.transform.position.y - 0.1f), LightningEffect.rotation);
            effect.SetParent(collision.transform);
            Destroy(effect.gameObject, StunDuration);
        }
    }
}
