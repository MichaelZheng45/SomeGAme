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

	bool initial = true;
	// Use this for initialization
	void Start () {
        player = GameObject.FindGameObjectWithTag("Player");

        foreach (GameObject fooObj in GameObject.FindGameObjectsWithTag("Enemy")) //temp
        {
			GOenemies.Add(fooObj);
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

			if (maxAttack <= 0 || target == null)
			{
				newLaser.GetComponent<ElectricInline>().updatePointTwoValues(transform.position + new Vector3(.1f,.1f,0));
				Destroy(gameObject);
	
			}

			Vector2 pos = target.transform.position;
			transform.position = pos;

			newLaser.GetComponent<ElectricInline>().updatePointTwoValues(transform.position);
			EnemySpawner.Instance.emitStorm(pos);
            count = 0;
            maxAttack--;
            target.gameObject.GetComponent<EnemyHealth>().ReduceHealthPoints(damage);

            GOenemies.Remove(target);
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
			
			if (initial)
			{
				directionToTarget = (Vector2)potentialTarget.transform.position - gameManager.Instance.mousePos();
			}
			

            float dSqrToTarget = directionToTarget.sqrMagnitude;
			float distance = directionToTarget.magnitude;
            if (dSqrToTarget < closestDistanceSqr && distance < 8)
            {
	
                closestDistanceSqr = dSqrToTarget;
                bestTarget = potentialTarget;
            }
        }
		initial = false;
		return bestTarget;
    }
}
