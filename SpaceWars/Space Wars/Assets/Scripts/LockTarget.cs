using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LockTarget : MonoBehaviour {

    [HideInInspector]
    public Transform parentPlayer { get; set; }

    private AutoTurret parentTurret;
    private Transform currentTarget;

    private void Start()
    {
        parentTurret = transform.parent.GetComponentInChildren<AutoTurret>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(currentTarget != null) // if turret is already targeting a player
        {
            return;
        }
        if (collision.gameObject.GetComponent<Player>())
        {
            if (collision.transform == parentPlayer) // if the player owns the turret
            {
                return;
            }
            parentTurret.SetTarget(collision.transform);
            currentTarget = collision.transform;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.transform == currentTarget) // if the current target leaves the turret's radius
        {
            parentTurret.SetTarget(null);
            currentTarget = null;
        }
    }
}
