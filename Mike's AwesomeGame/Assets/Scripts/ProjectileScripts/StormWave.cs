using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StormWave : MonoBehaviour {

    public GameObject stormInLine;

    int count = 0;

    public int maxAttack;
    public int damage;

    public List<GameObject> GOenemies;
    GameObject player;

	// Use this for initialization
	void Start () {
        player = GameObject.FindGameObjectWithTag("Player");

        foreach (GameObject fooObj in GameObject.FindGameObjectsWithTag("Enemy"))
        {
            if (Mathf.Abs(player.transform.position.x - fooObj.transform.position.x) < 18 && Mathf.Abs(player.transform.position.y - fooObj.transform.position.y) < 15)
            {

               GOenemies.Add(fooObj);
                

            }

        }
    }
	
	// Update is called once per frame
	void FixedUpdate () {
        count++;
        if(count > 2 || maxAttack <= 0)
        {
            Destroy(gameObject);
        }

        foreach (GameObject fooObj in GOenemies)
        {
            if(fooObj == null)
            {
                GOenemies.Remove(fooObj);
            }
        }

            if (GOenemies.Count != 0)
        {
            GameObject newLaser = Instantiate(stormInLine, transform.position, transform.rotation);
            newLaser.GetComponent<ElectricInline>().updatePointOne(transform.position);

            GameObject target = GetClosestEnemy(GOenemies);

            transform.position = target.transform.position;

            count = 0;
            maxAttack--;
            target.gameObject.GetComponent<EnemyHealth>().ReduceHealthPoints(damage);

                

            if (maxAttack <= 0)
            {
                Destroy(gameObject);
            }

            GOenemies.Remove(target);
            
            newLaser.GetComponent<ElectricInline>().updatePointTwoValues(transform.position);
        }
	}

    GameObject GetClosestEnemy(List<GameObject> enemies)
    {
        GameObject bestTarget = null;
        float closestDistanceSqr = Mathf.Infinity;
        Vector3 currentPosition = transform.position;
        foreach (GameObject potentialTarget in enemies)
        {
            if(potentialTarget == null)
            {
                return null;
            }

            Vector3 directionToTarget = potentialTarget.transform.position - currentPosition;
            float dSqrToTarget = directionToTarget.sqrMagnitude;
            if (dSqrToTarget < closestDistanceSqr )
            {
                closestDistanceSqr = dSqrToTarget;
                bestTarget = potentialTarget;
            }
        }

        return bestTarget;
    }
}
