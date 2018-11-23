using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {

	public float playerSpeed;
	Rigidbody2D rb;
	// Use this for initialization
	void Start () {
		rb = GetComponent<Rigidbody2D>();
	}
	
	// Update is called once per frame
	void FixedUpdate()
	{
		float playerX = gameObject.transform.position.x;
		float playerY = gameObject.transform.position.y;
		Vector2 newVector = new Vector2(0, 0);

		if(Input.GetKey(KeyCode.A))
		{
			newVector.x += -playerSpeed;
        }
		if(Input.GetKey(KeyCode.S))
		{
			newVector.y += -playerSpeed;
        }
		if(Input.GetKey(KeyCode.D))
		{
			newVector.x += playerSpeed;
        }
		if(Input.GetKey(KeyCode.W))
		{
			newVector.y += playerSpeed;
        }

		rb.velocity = newVector;
	}
}
