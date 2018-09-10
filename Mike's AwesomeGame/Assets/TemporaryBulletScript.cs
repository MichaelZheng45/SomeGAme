using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TemporaryBulletScript : MonoBehaviour {

    int count = 0;
	// Use this for initialization
	void Start () {
        gameObject.GetComponent<Rigidbody2D>().AddForce(transform.up *500);
	}
	
	// Update is called once per frame
	void Update () {
        count++;
        if(count > 90)
        {
            Destroy(gameObject);
        }
	}
}
