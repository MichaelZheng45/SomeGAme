using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spawnandDelete : MonoBehaviour {

    float count = 0;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        count += Time.deltaTime;
        if(count > 2)
        {
            Destroy(gameObject);
        }
	}
}
