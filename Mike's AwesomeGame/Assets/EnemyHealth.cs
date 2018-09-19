using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour {

    public float HealthPoints;
    public float damageShieldPwr;
    float CurrentHealthPoints;
	// Use this for initialization
	void Start () {
        CurrentHealthPoints = HealthPoints;
	}
	
	// Update is called once per frame
	void Update () {
		if(CurrentHealthPoints <= 0)
        {
            Destroy(gameObject);
        }
	}

    public float GetHealthPoints()
    {
        return CurrentHealthPoints;
    }

    public void ReduceHealthPoints(float damage)
    {
        CurrentHealthPoints -= (damage - damageShieldPwr);
    }
}
