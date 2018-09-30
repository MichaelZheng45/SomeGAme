using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TemporaryBulletScript : MonoBehaviour {
    //RAPID and type 1 attacks
    public float damage;
    public bool targetPlayer = false;
    int count = 0;
    public int velocity;
    // Use this for initialization

    //zawarudo
    float countWarudoCount = 0;
    bool timeStop = false;

    void Start () {
        gameObject.GetComponent<Rigidbody2D>().AddForce(transform.up * velocity);
    }
	
	// Update is called once per frame
	void Update () {

        if (timeStop)
        {
            countWarudoCount += Time.deltaTime;
            if (countWarudoCount > 2)
            {
                GetComponent<Rigidbody2D>().velocity *= 0;
                timeStop = false;
                Destroy(gameObject);
            }
        }
        else
        {
            count++;
            if (count > 200)
            {
                Destroy(gameObject);
            }
        }  
	}

    public void activateZaWarudo()
    {
        if(targetPlayer)
        {
            timeStop = true;
            countWarudoCount = 0;
            GetComponent<Rigidbody2D>().velocity *= 0;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(targetPlayer && collision.gameObject.tag == "Player")
        {
            collision.gameObject.GetComponent<PlayerHealth>().reduceHealthPoints(-1);
            Destroy(gameObject);
        }
        if (collision.gameObject.tag == "Enemy" && !targetPlayer)
        {
            collision.gameObject.GetComponent<EnemyHealth>().ReduceHealthPoints(damage);
            Destroy(gameObject);
        }
        else if(collision.gameObject.tag == "Wall")
        {
            Destroy(gameObject);
        }
    }
}
