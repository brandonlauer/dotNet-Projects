using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldObstacleDetector : MonoBehaviour {

    public Transform parentPlayer;

    private bool isObstructed;
    private Collider2D obstacle;

    private void Update()
    {
        if (isObstructed && !obstacle)
        {
            isObstructed = false;
            parentPlayer.GetComponent<Character2D> ().shieldObstructed = false;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        isObstructed = true;
        parentPlayer.GetComponent<Character2D> ().shieldObstructed = true;
        obstacle = collision;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        isObstructed = false;
        parentPlayer.GetComponent<Character2D> ().shieldObstructed = false;
    }
}
