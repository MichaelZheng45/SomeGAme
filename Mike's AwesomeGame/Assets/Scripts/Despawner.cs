﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Despawner : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Projectile")
        {
            Destroy(collision.gameObject);
        }
    }
}
