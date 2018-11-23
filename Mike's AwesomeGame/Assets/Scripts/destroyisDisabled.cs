using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class destroyisDisabled : MonoBehaviour {

	Explosion explosive;
	// Use this for initialization
	void Start () {
		explosive = GetComponent<Explosion>();
	}
	
	// Update is called once per frame
	void Update () {
		if(explosive.enabled == false)
		{
			Destroy(gameObject);
		}
	}
}
