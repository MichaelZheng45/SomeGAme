using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyRobotPatterns : MonoBehaviour {
    public string playerName;
    public int currentPattern;

    //randomizer
    public bool shouldRandomAttack;
    float rCount;
    float maxTime;
    //data for attacking
    float curRot;
    public float rotationalSpeed;
    Vector2 targetLocation;
    bool retarget = false;

	// Use this for initialization
	void Start () {

        maxTime = Random.Range(10,20);
        targetLocation = GameObject.Find(playerName).transform.position + new Vector3(Random.Range(-3f, 3f), Random.Range(-3f, 3f), 0);
        rotateTowardTarget(targetLocation, rotationalSpeed);
    }
	
	// Update is called once per frame
	void Update () {
        //getComponent script of robot health, if it is disabled, the it won't activate the script
        robotAttackPatterns();
        chooseNewPattern();
	}

    void robotAttackPatterns()
    {
        //facePlayer to attack
        if ((GameObject.FindGameObjectWithTag(playerName).transform.position - transform.position).magnitude < 15)
        {
            gameObject.transform.rotation = Quaternion.Euler(new Vector3(0, 0, Mathf.Atan2((GameObject.Find(playerName).transform.position.y - transform.position.y)
          , (GameObject.Find(playerName).transform.position.x - transform.position.x)) * Mathf.Rad2Deg + 180 + 90));
        }
        else
        {
            gameObject.transform.rotation = Quaternion.Euler(new Vector3(0, 0, curRot + 90));
        }
          

        //ranged slow steer attack
        if (currentPattern == 1)
        {
            steerAttack(.2f,rotationalSpeed/2);
        }
        else if (currentPattern == 2) //fast running attack around the player
        {
            steerRunAttack(.2f, rotationalSpeed);
        }
        else if(currentPattern == 3) //keep distance, if player gets close, flee (WIP)
        {

        }
        else if(currentPattern == 4) // random positions, if player is too far, move toward player (WIP)
        {

        }
        else if(currentPattern == 5) //direct attack
        {
            curRot = transform.rotation.eulerAngles.z;
            float velocity = .2f;
            gameObject.transform.position += (GameObject.Find(playerName).transform.position - transform.position).normalized * velocity/2;
        }
    }

    void steerAttack(float velocity, float rotationSpeed)
    {
        //temp line below
        if (Vector2.Distance(gameObject.transform.position, targetLocation) < 10 || retarget == true)
        {
            targetLocation = GameObject.Find(playerName).transform.position + new Vector3(Random.Range(-5f, 5f)
                , Random.Range(-5f, 5f), 0);
            retarget = false;
        }
        rotateTowardTarget(targetLocation,rotationSpeed);
        Vector2 dirVector = new Vector2(Mathf.Cos(curRot * Mathf.Deg2Rad), Mathf.Sin(curRot * Mathf.Deg2Rad)) * velocity;
        gameObject.transform.position = new Vector3(transform.position.x - dirVector.x, transform.position.y - dirVector.y);

    }

    void steerRunAttack(float velocity, float rotationSpeed)
    {
        //temp line below
        if (Vector2.Distance(gameObject.transform.position, targetLocation) < 8 || retarget == true)
        {
            targetLocation = GameObject.Find(playerName).transform.position + new Vector3(Random.Range(-10f, 10f)
                , Random.Range(-10f, 10f), 0);
            retarget = false;
        }

        if((GameObject.FindGameObjectWithTag(playerName).transform.position - transform.position).magnitude < 10)
        {
            curRot = Quaternion.Inverse(transform.rotation).eulerAngles.z;
            Debug.Log(curRot);
        }

        rotateTowardTarget(targetLocation, rotationSpeed);
        Vector2 dirVector = new Vector2(Mathf.Cos(curRot * Mathf.Deg2Rad), Mathf.Sin(curRot * Mathf.Deg2Rad)) * velocity;
        gameObject.transform.position = new Vector3(transform.position.x - dirVector.x, transform.position.y - dirVector.y);

    }

    void rotateTowardTarget(Vector2 targetPoint, float rotationSpeed)
    {
        bool goRight;
        float targetRot = Mathf.Atan2((targetPoint.y - transform.position.y),(targetPoint.x - transform.position.x)) * Mathf.Rad2Deg + 180;
        if (targetRot > curRot)
        {
            if(targetRot - curRot > curRot + (360 - targetRot))
            {
                goRight = true;
            }
            else
            {
                goRight = false;
            }
        }
        else
        {
            if(360 - (curRot - targetRot) > curRot - targetRot)
            {
                goRight = true;
            }
            else
            {
                goRight = false;
            }
        }

        if(goRight)
        {
            if (Mathf.Abs(curRot - targetRot) > 2)
            {
                curRot -= rotationSpeed;
            }     
            if(curRot < 0)
            {
                curRot = 360 - rotationSpeed;
            }

        }
        else
        {
            curRot += rotationSpeed;
        }
        curRot = Mathf.Abs(curRot);
        if(curRot > 360 || curRot < -360)
        {
            curRot = 0;
        }
    }

    void chooseNewPattern()
    {
        if(shouldRandomAttack)
        {
            rCount+= Time.deltaTime;
            if(rCount > maxTime)
            {
                rCount = 0;
                currentPattern = Random.Range(1, 4);
            }
        }
    }

    public void reStartAi()
    {
        //point at player and then choosenewpattern
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Wall")
        {
 
            retarget = true;
            curRot = (new Vector3(0, 0, Mathf.Atan2((GameObject.Find(playerName).transform.position.y - transform.position.y)
               , (GameObject.Find(playerName).transform.position.x - transform.position.x)) * Mathf.Rad2Deg + 180 + 90)).z;
        }
       
    }
}
