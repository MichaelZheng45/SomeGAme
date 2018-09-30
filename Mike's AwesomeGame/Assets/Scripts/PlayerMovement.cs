using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {

	public float playerSpeed;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
		float playerX = gameObject.transform.position.x;
		float playerY = gameObject.transform.position.y;
		if(Input.GetKey(KeyCode.A))
		{
			gameObject.transform.position = new Vector2(playerX - playerSpeed, playerY );
            playerX = gameObject.transform.position.x;
        }
		if(Input.GetKey(KeyCode.S))
		{
			gameObject.transform.position = new Vector2(playerX ,playerY - playerSpeed);
            playerY = gameObject.transform.position.y;
        }
		if(Input.GetKey(KeyCode.D))
		{
			gameObject.transform.position = new Vector2(playerX + playerSpeed,playerY);
            playerX = gameObject.transform.position.x;
        }
		if(Input.GetKey(KeyCode.W))
		{
			gameObject.transform.position = new Vector2(playerX,playerY + playerSpeed);
            playerY = gameObject.transform.position.y;
        }
	}
}
