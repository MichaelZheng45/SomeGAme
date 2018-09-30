using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spawnNodeAvailibility : MonoBehaviour {

    bool spawnAvailable = true;
    public int XBounds, YBounds;
	// Use this for initialization
	void Start () {

    }
	
	// Update is called once per frame
	void Update () {
        float xPos = gameObject.transform.position.x;
        float yPos = gameObject.transform.position.y;
		if(xPos > XBounds || xPos < -XBounds || yPos < -YBounds || yPos > YBounds)
        {
            spawnAvailable = false;
        }
        else
        {
            spawnAvailable = true;
        }
	}

    public bool canSpawn()
    {
        return spawnAvailable;
    }
}
