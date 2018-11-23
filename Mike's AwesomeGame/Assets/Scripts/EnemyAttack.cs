using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//attack type 1 where they will only fire a single bullet.
public class EnemyAttack : MonoBehaviour {

    public bool canAttack;
    public GameObject enemyAttackBullet;
    public int accuracy;

    float currTime;
    public float maxTime;

    public GameObject spawnLocation;
    GameObject player;
    public int range;

	// Use this for initialization
	void Start () {
        player = GameObject.FindGameObjectWithTag("Player");

    }
	
	// Update is called once per frame
	void Update () {
        attackPattern();
        

	}


    void attackPattern()
    {

        if ((player.transform.position - transform.position).magnitude < range)
        {
            //ask for permision to attack
            canAttack = true;
        }
        else
        {
            canAttack = false;
        }

        if (canAttack)
        {
            attackPlayer();
        }
    }

    void attackPlayer()
    {
        currTime += Time.deltaTime;

        if(currTime > maxTime)
        {
            //fire bullet
            float rotation = transform.rotation.eulerAngles.z + Random.Range(accuracy * -1f, accuracy);
			GameObject newProjectile = Instantiate(enemyAttackBullet, spawnLocation.transform.position, Quaternion.Euler(0, 0, rotation));
            newProjectile.GetComponent<TemporaryBulletScript>().targetPlayer = true;
			currTime = 0;
        }
    }
}
