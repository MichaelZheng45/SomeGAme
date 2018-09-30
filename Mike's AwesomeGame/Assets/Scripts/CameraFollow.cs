using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour {
    public bool followMouse = false;
    public GameObject player;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if(!followMouse)
        {
            gameObject.transform.position = new Vector3(player.transform.position.x, player.transform.position.y, transform.position.z);

        }
        else
        {
            Vector2 mPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            gameObject.transform.position = new Vector3(mPos.x,mPos.y, gameObject.transform.position.z);
        }
    }
}
