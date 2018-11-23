using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class foggyAtmosphere : MonoBehaviour {

	public SpriteRenderer sp;

	
	// Update is called once per frame
	void Update () {
		float alpha = 1/8f * Mathf.Sin(Time.time * 2) + .3f;

		sp.color = new Color(1, 1, 1, alpha);

	}
}
