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
    public List<int> choosableAttackPatterns;

    //data for attacking
    float curRot;
    public float rotationalSpeed;
    Vector2 targetLocation;
    bool retarget = false;
    bool isAttacking = false;

    public int range;
    public float mainVelocity;
    //player
    GameObject player;

	// Use this for initialization
	void Start () {

        maxTime = Random.Range(10,20);
        rCount = maxTime + 10;
        player = GameObject.Find(playerName);

        chooseNewPattern();
        //find player to kill
        targetLocation = player.transform.position + new Vector3(Random.Range(-3f, 3f), Random.Range(-3f, 3f), 0);
        curRot = Random.Range(0, 360);
    }
	
	// Update is called once per frame
	void FixedUpdate() { 
		//getComponent script of robot health, if it is disabled, the it won't activate the script

		robotAttackPatterns();
        chooseNewPattern();
        
	}

    void robotAttackPatterns()
    {
        //facePlayer to attack
     
        if ((player.transform.position - transform.position).magnitude < range)
        {
            gameObject.transform.rotation = Quaternion.Euler(new Vector3(0, 0, Mathf.Atan2((player.transform.position.y - transform.position.y)
            , (player.transform.position.x - transform.position.x)) * Mathf.Rad2Deg + 180 + 90));
            isAttacking = true;
        }
        else if (currentPattern == 3 || currentPattern == 2 || currentPattern == 4)
        {
            gameObject.transform.rotation = Quaternion.Euler(new Vector3(0, 0, curRot + 90));
            isAttacking = false;
        }
        else
        {
            isAttacking = false;
        }

       
        if (currentPattern == 1) //ordinal attack
        {
            ordinalAttack(mainVelocity*2);
        }
        else if (currentPattern == 2) //ordinal attack
        {
            ordinalAttack(mainVelocity*2);
        }
        else if(currentPattern == 3)  // longranged speedsteer attack at the player
        {
            steerAttack(mainVelocity, rotationalSpeed / 2);
        }
        else if(currentPattern == 4) // random positional movement
        {
            randomPointAttack(mainVelocity);
        }
        else if(currentPattern == 5) //direct attack
        {
            directAttack(mainVelocity);
        }
    }

    void ordinalAttack(float velocity)
    {
        float radius = Random.Range(8,12);
        float radians = Random.Range(0, 360) * 3.14f / 180;
        if (!isAttacking)
        {
            gameObject.transform.rotation = Quaternion.Euler(new Vector3(0, 0, Mathf.Atan2((targetLocation - (Vector2)transform.position).y, (targetLocation - (Vector2)transform.position).x) * Mathf.Rad2Deg + 180 + 90));
        }

        if ((targetLocation - (Vector2)transform.position).magnitude < 1)
        {
            targetLocation = (Vector2)transform.position + new Vector2(Mathf.Cos(radians), Mathf.Sin(radians)) * radius;
        }
        if(retarget)
        {
            retarget = false;
            float xNeg = transform.position.x / Mathf.Abs(transform.position.x);
            float yNeg = transform.position.y / Mathf.Abs(transform.position.y);
            targetLocation = new Vector2(transform.position.x - radius * xNeg, transform.position.y - radius * yNeg);
        }
    
        gameObject.transform.position += ((Vector3)targetLocation - transform.position).normalized * velocity / 2;
    }

    void steerAttack(float velocity, float rotationSpeed)
    {
        //temp line below
        if (Vector2.Distance(gameObject.transform.position, targetLocation) < 15 || retarget == true)
        {
            targetLocation = player.transform.position + new Vector3(Random.Range(-5f, 5f)
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
            targetLocation = player.transform.position + new Vector3(Random.Range(-10f, 10f)
                , Random.Range(-10f, 10f), 0);
            retarget = false;
        }

        if((player.transform.position - transform.position).magnitude < 10)
        {
            curRot = Quaternion.Inverse(transform.rotation).eulerAngles.z;
        }

        rotateTowardTarget(targetLocation, rotationSpeed);
        Vector2 dirVector = new Vector2(Mathf.Cos(curRot * Mathf.Deg2Rad), Mathf.Sin(curRot * Mathf.Deg2Rad)) * velocity;
        gameObject.transform.position = new Vector3(transform.position.x - dirVector.x, transform.position.y - dirVector.y);

    }

    void randomPointAttack(float velocity)
    {
        velocity += .1f;
        if ((targetLocation - (Vector2)transform.position).magnitude < 10)
        {
            targetLocation = new Vector2(Random.Range(-30, 30), Random.Range(-30, 30));
        }

        rotateTowardTarget(targetLocation, 2);
        Vector2 dirVector = new Vector2(Mathf.Cos(curRot * Mathf.Deg2Rad), Mathf.Sin(curRot * Mathf.Deg2Rad)) * velocity;
        gameObject.transform.position = new Vector3(transform.position.x - dirVector.x, transform.position.y - dirVector.y);
    }

    void directAttack(float velocity)
    {
        curRot = transform.rotation.eulerAngles.z;

        if (!isAttacking)
        {
            gameObject.transform.rotation = Quaternion.Euler(new Vector3(0, 0, Mathf.Atan2((player.transform.position - transform.position).y, (player.transform.position - transform.position).x) * Mathf.Rad2Deg + 180 + 90));
        }

        gameObject.transform.position += (player.transform.position - transform.position).normalized * velocity / 2;
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
            if (Mathf.Abs(curRot - targetRot) > rotationSpeed)
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
                currentPattern = choosableAttackPatterns[Random.Range(0, choosableAttackPatterns.Count)];

                if(currentPattern == 2 || currentPattern == 3)
                {
                    targetLocation = player.transform.position;
                }
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Wall")
        {
            retarget = true;
         
            curRot = (new Vector3(0, 0, Mathf.Atan2((targetLocation.y - transform.position.y)
               , (targetLocation.x - transform.position.x)) * Mathf.Rad2Deg + 180 + 90)).z;
        }
       
    }
}
