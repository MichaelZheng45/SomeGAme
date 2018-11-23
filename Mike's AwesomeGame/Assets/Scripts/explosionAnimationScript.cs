using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class explosionAnimationScript : MonoBehaviour {

	Animator explosionAnimator;
	public GameObject particleObj;
	float count = 0;
	// Use this for initialization
	void Start () {
		explosionAnimator = GetComponent<Animator>();
	}

	// Update is called once per frame
	void Update()
	{
		count += Time.deltaTime;
		if (count > .5)
		{
			Destroy(gameObject);
		}
	}
}
