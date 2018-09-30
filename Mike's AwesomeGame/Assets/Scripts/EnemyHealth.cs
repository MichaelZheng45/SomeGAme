using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour {

    public float HealthPoints;
    public float damageShieldPwr;
    float CurrentHealthPoints;
    // Use this for initialization

    GameObject enemyManager;

    public List<Sprite> spriteList;
    public Sprite mainSprite;

    void Start () {
        enemyManager = GameObject.FindGameObjectWithTag("EnemyManager");
        CurrentHealthPoints = HealthPoints;
	}
	
	// Update is called once per frame
	void Update () {
        if(mainSprite == null)
        {
            if (damageShieldPwr > 0)
            {
                GetComponent<SpriteRenderer>().sprite = spriteList[1];
            }
            else
            {
                GetComponent<SpriteRenderer>().sprite = spriteList[0];
            }
        }

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
        if(damageShieldPwr > 0)
        {
            damageShieldPwr -= damage;
        }
        else
        {
            CurrentHealthPoints -= (damage);
        }
      
        enemyManager.GetComponent<DPSChecker>().addDamage((int)damage);
    }
}
