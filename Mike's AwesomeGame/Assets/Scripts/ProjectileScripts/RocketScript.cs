using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RocketScript : MonoBehaviour {

    public int damage;
    public bool isRipper;
    public bool isHive;
    int count = 0;
    public int velocity;
    public float nonRocketVelocity;
    public GameObject explosion;
    Vector2 targetLocation;

    float curRot;
    // Use this for initialization
    void Start()
    {
		targetLocation = gameManager.Instance.mousePos();
		curRot = gameObject.transform.rotation.eulerAngles.z - 90;
        if (!isRipper && !isHive)
        {
            gameObject.GetComponent<Rigidbody2D>().AddForce(transform.up * velocity);
        }
    }

    // Update is called once per frame
    void FixedUpdate()
	{
        if(!isRipper && !isHive)
        {
            count++;
            if (count > 100)
            {
				soundManage.Instance.weaponSounds(weaponSound.ROCKET, true);
				explosion.GetComponent<Explosion>().activateExplosion();
                Destroy(gameObject);
            }
        }
        else if(isRipper)//ripper attack
        {
            Vector2 distance = (Vector2)transform.position - targetLocation;
            if (distance.magnitude < 1.5)
            {
				soundManage.Instance.weaponSounds(weaponSound.RIPPER, true);
                explosion.GetComponent<Explosion>().activateExplosion(true);
                Destroy(gameObject);
            }

            distance = distance.normalized * nonRocketVelocity;
            gameObject.transform.position -= (Vector3)distance;
        }
        else //hiveAttack
        {
            count++;
            if (count > 100)
            {
				soundManage.Instance.weaponSounds(weaponSound.HIVE, true);
				explosion.GetComponent<Explosion>().activateExplosion();
                Destroy(gameObject);
            }

            targetLocation = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            rotateTowardTarget(targetLocation, 10);
            Vector2 dirVector = new Vector2(Mathf.Cos(curRot * Mathf.Deg2Rad), Mathf.Sin(curRot * Mathf.Deg2Rad)) * nonRocketVelocity;
            gameObject.transform.position = new Vector3(transform.position.x - dirVector.x, transform.position.y - dirVector.y);
            gameObject.transform.rotation = Quaternion.Euler(new Vector3(0, 0, curRot+90));
        }
    }

    void rotateTowardTarget(Vector2 targetPoint, float rotationSpeed)
    {
        bool goRight;
        float targetRot = Mathf.Atan2((targetPoint.y - transform.position.y), (targetPoint.x - transform.position.x)) * Mathf.Rad2Deg + 180;
        if (targetRot > curRot)
        {
            if (targetRot - curRot > curRot + (360 - targetRot))
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
            if (360 - (curRot - targetRot) > curRot - targetRot)
            {
                goRight = true;
            }
            else
            {
                goRight = false;
            }
        }

        if (goRight)
        {
            if (Mathf.Abs(curRot - targetRot) > rotationSpeed)
            {
                curRot -= rotationSpeed;
            }
            if (curRot < 0)
            {
                curRot = 360 - rotationSpeed;
            }

        }
        else
        {
            curRot += rotationSpeed;
        }
        curRot = Mathf.Abs(curRot);
        if (curRot > 360 || curRot < -360)
        {
            curRot = 0;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Enemy" && !isRipper)
        {
            collision.gameObject.GetComponent<EnemyHealth>().ReduceHealthPoints(damage);
			count = 300;
        }
        else if (collision.gameObject.tag == "Wall")
        {
			if(!isHive)
			{
				count = 300;
			}
		}
    }
}
