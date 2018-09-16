using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyRobotPatterns : MonoBehaviour {
    public string playerName;
    public List<int> attackPatterns;
    int currentPattern;

    float curRot;
    public float velocity;
    public float rotationSpeed;
    Vector2 targetLocation;
    bool retarget = false;

	// Use this for initialization
	void Start () {
        currentPattern = 1; //temporary!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!11
        targetLocation = GameObject.Find(playerName).transform.position + new Vector3(Random.Range(-3f, 3f), Random.Range(-3f, 3f), 0);
        rotateTowardTarget(targetLocation);
    }
	
	// Update is called once per frame
	void Update () {
        //getComponent script of robot health, if it is disabled, the it won't activate the script
        robotAttackPatterns();
       
	}

    void robotAttackPatterns()
    {
        //ranged steer attack
        if(currentPattern == 1)
        {
            //facePlayer to attack
            gameObject.transform.rotation = Quaternion.Euler(new Vector3(0, 0, Mathf.Atan2((GameObject.Find(playerName).transform.position.y - transform.position.y)
           , (GameObject.Find(playerName).transform.position.x - transform.position.x)) * Mathf.Rad2Deg + 180 + 90));

            //temp line below
            if(Vector2.Distance(gameObject.transform.position, targetLocation) < 5 || retarget == true)
            {
                targetLocation = GameObject.Find(playerName).transform.position + new Vector3(Random.Range(-5f, 5f)
                    , Random.Range(-3f, 3f), 0);
                retarget = false;
            }
            rotateTowardTarget(targetLocation);
            Vector2 dirVector = new Vector2(Mathf.Cos(curRot* Mathf.Deg2Rad),Mathf.Sin(curRot * Mathf.Deg2Rad)) * velocity;
            gameObject.transform.position = new Vector3(transform.position.x - dirVector.x, transform.position.y - dirVector.y);
        }
        else if(currentPattern == 2) //keep distance, if player gets close, flee (WIP)
        {
            //facePlayer to attack
            gameObject.transform.rotation = Quaternion.Euler(new Vector3(0, 0, Mathf.Atan2((GameObject.Find(playerName).transform.position.y - transform.position.y)
           , (GameObject.Find(playerName).transform.position.x - transform.position.x)) * Mathf.Rad2Deg + 180 + 90));
        }
        else if(currentPattern == 3) // random positions, if player is too far, move toward player (WIP)
        {
            //facePlayer to attack
            gameObject.transform.rotation = Quaternion.Euler(new Vector3(0, 0, Mathf.Atan2((GameObject.Find(playerName).transform.position.y - transform.position.y)
           , (GameObject.Find(playerName).transform.position.x - transform.position.x)) * Mathf.Rad2Deg + 180 + 90));
        }
        else if(currentPattern == 4) //direct attack
        {
            //facePlayer to attack
            gameObject.transform.rotation = Quaternion.Euler(new Vector3(0, 0, Mathf.Atan2((GameObject.Find(playerName).transform.position.y - transform.position.y)
           , (GameObject.Find(playerName).transform.position.x - transform.position.x)) * Mathf.Rad2Deg + 180 + 90));

            gameObject.transform.position += (GameObject.Find(playerName).transform.position - transform.position).normalized * velocity/2;
        }
    }

    void rotateTowardTarget(Vector2 targetPoint)
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

    }

    public void reStartAi()
    {
        //point at player and then choosenewpattern
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Wall")
        {
            Debug.Log("HIT");
            retarget = true;
            curRot = (new Vector3(0, 0, Mathf.Atan2((GameObject.Find(playerName).transform.position.y - transform.position.y)
               , (GameObject.Find(playerName).transform.position.x - transform.position.x)) * Mathf.Rad2Deg + 180 + 90)).z;
        }
       
    }
}
