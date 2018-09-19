using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour {

    public bool canAttack;
    public GameObject enemyAttackBullet;
    public int accuracy;

    float currTime;
    public float maxTime;

    public GameObject enemyAttackManager;
    public GameObject spawnLocation;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

        if((GameObject.FindGameObjectWithTag("Player").transform.position - transform.position).magnitude < 15)
        {
            Debug.Log("Attack");
            //ask for permision to attack
            canAttack = true;
        }
        else
        {
            canAttack = false;
        }

        if(canAttack)
        {
            attackPlayer();
        } 
        else
        {
            currTime = 0;
        }
	}

    void attackPlayer()
    {
        currTime += Time.deltaTime;

        if(currTime > maxTime)
        {
            //fire bullet
            float rotation = transform.rotation.eulerAngles.z + Random.Range(accuracy * -1f, accuracy);
            GameObject newProjectile = Instantiate(enemyAttackBullet, spawnLocation.transform.position, Quaternion.Euler(0, 0, rotation)) as GameObject;
            newProjectile.GetComponent<TemporaryBulletScript>().targetPlayer = true;

            currTime = 0;
        }
    }
}
