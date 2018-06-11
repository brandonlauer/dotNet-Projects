using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class PowerupPreviewer : NetworkBehaviour {

    public GameObject Powerup;

    bool previewActivated = false;
    Transform parentPlayer;
    bool isObstructed;
    Collider2D obstacle;

	// Use this for initialization
	void Start () {
		if(Powerup == null)
        {
            Debug.LogError ("No power-up assigned!");
        }
        transform.GetComponent<SpriteRenderer> ().enabled = false;
        transform.GetComponent<PolygonCollider2D> ().enabled = false;
    }

    public void SetParent(Transform parent)
    {
        parentPlayer = parent;
    }
	
	// Update is called once per frame
	void Update () {
        if(Input.GetKeyUp(KeyCode.E) && !previewActivated)
        {
            previewActivated = true;
            transform.GetComponent<SpriteRenderer> ().enabled = true;
            transform.GetComponent<PolygonCollider2D> ().enabled = true;
            return;
        }

		if (isObstructed && !obstacle)
        {
            isObstructed = false;
            transform.GetComponent<SpriteRenderer> ().color = new Color (129, 129, 129, 0.5f);
        }

        if (isObstructed) { return; }

        if (Input.GetKeyUp (KeyCode.E) && previewActivated)
        {
            GameObject _powerup = Instantiate (Powerup, transform.position, transform.rotation);
            _powerup.transform.localScale = new Vector3 (-parentPlayer.localScale.x, 1, 1);

            if (_powerup.GetComponentInChildren<LockTarget> ()) // If power-up is a turret
            {
                _powerup.GetComponentInChildren<LockTarget> ().parentPlayer = parentPlayer;
            }
            NetworkServer.Spawn(_powerup);
            
            Destroy (this.gameObject);
            GameMaster.ChangePowerup(GameMaster.EmptyPowerup);
            GameMaster.HasPowerup = false;
        }
	}

    private void OnTriggerEnter2D(Collider2D collision)
    {
        isObstructed = true;
        obstacle = collision;
        transform.GetComponent<SpriteRenderer> ().color = new Color (255, 0, 0, 0.5f);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        isObstructed = false;
        transform.GetComponent<SpriteRenderer> ().color = new Color (129, 129, 129, 0.5f);
    }
}
