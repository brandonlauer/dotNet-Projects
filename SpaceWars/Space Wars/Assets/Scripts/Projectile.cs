using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour {

    public Transform HitParticles;
    public float Lifetime = 2f;
    public int Damage = 20;

    [HideInInspector]
    public Transform parent;

    private void Update()
    {
        Lifetime -= Time.deltaTime;
        if(Lifetime < 0)
        {
            Destroy (gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.transform == parent)
        {
            return;
        }
        if (collision.gameObject.GetComponent<Player>())
        {
            collision.gameObject.GetComponent<Player>().DamagePlayer(Damage);
        }
        if (collision.transform.GetComponentInChildren<AutoTurret>())
        {
            if(collision.transform.GetComponentInChildren<LockTarget>().parentPlayer == parent)
            {// if turret owned by same player
                return;
            }
            collision.transform.GetComponentInChildren<AutoTurret>().DamageTurret(Damage / 5);
        }

        //Vector2 hitPos = collision.gameObject.GetComponent<Collider2D> ().bounds.ClosestPoint (collision.transform.position);
        Transform hitParticle = Instantiate(HitParticles, transform.position, Quaternion.identity) as Transform;
        Destroy(gameObject);
        Destroy(hitParticle.gameObject, 1);
    }
}
