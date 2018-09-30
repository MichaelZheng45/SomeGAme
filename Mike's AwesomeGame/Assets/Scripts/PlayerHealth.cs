using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour {

    public int maxHealth;
    int curHealth;
	// Use this for initialization
	void Start () {
        curHealth = maxHealth;
	}
	
	// Update is called once per frame
	void Update () {

		if(curHealth <= 0)
        {
            Destroy(gameObject);
        }
	}

   public void reduceHealthPoints(int points)
    {
        Debug.Log("<color=red>Player HP: </color>" + curHealth);
        curHealth += points;
    }
}
