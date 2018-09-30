using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DPSChecker : MonoBehaviour {

    float count = 0;
    float damageTracker;
	// Use this for initialization
	void Start () {
        damageTracker = 0;	
	}
	
	// Update is called once per frame
	void Update () {
        count += Time.deltaTime;
       if(count > 1)
        {
            count = 0;
            Debug.Log("<color=teal>DPS: </color>" + damageTracker);
            damageTracker = 0;
        }
	}

    public void addDamage(int damage)
    {
        damageTracker += damage;
    }
}
