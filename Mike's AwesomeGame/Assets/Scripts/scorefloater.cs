using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scorefloater : MonoBehaviour {

    // Use this for initialization
    float count = 0;
	void Start () {
        transform.position += new Vector3(Random.Range(-2f, 2f), Random.Range(-.2f, .3f),0);

	}
	
	// Update is called once per frame
	void Update () {
        transform.position += new Vector3(0, 10f, 0);
        count += Time.deltaTime;
        if(count > 1)
        {
            Destroy(gameObject);
        }
	}
}
