using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class SelectOnInput : MonoBehaviour {

    public EventSystem eventSystem;
    public GameObject selectedObject;

    private bool buttonSelected;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetAxisRaw("Vertical") != 0  && buttonSelected == false)
        {
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
            eventSystem.SetSelectedGameObject(selectedObject);
            buttonSelected = true;
        }
        else if (Input.GetMouseButton(0) || Input.GetMouseButton(1)|| Input.GetMouseButton(2))
        {
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
            OnDisable();
        }
	}

    private void OnDisable()
    {
        buttonSelected = false;
    }
}
