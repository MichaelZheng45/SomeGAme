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
    float countWarudoCount = 0;
    bool timeStop = false;
    GameObject player;
    public int range;
	// Use this for initialization
	void Start () {
        player = GameObject.FindGameObjectWithTag("Player");

    }
	
	// Update is called once per frame
	void Update () {

        if (timeStop)
        {
            countWarudoCount += Time.deltaTime;
            if (countWarudoCount > 2)
            {
                timeStop = false;
            }
        }
        else
        {
            attackPattern();
        }

	}

    public void activateZaWarudo()
    {
        timeStop = true;
        countWarudoCount = 0;
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
            GameObject newProjectile = Instantiate(enemyAttackBullet, spawnLocation.transform.position, Quaternion.Euler(0, 0, rotation)) as GameObject;
            newProjectile.GetComponent<TemporaryBulletScript>().targetPlayer = true;

            currTime = 0;
        }
    }
}
