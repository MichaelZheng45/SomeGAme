using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TemporaryBulletScript : MonoBehaviour {
    //RAPID
    public bool targetPlayer = false;
    int count = 0;
    public int velocity;
	// Use this for initialization
	void Start () {
        gameObject.GetComponent<Rigidbody2D>().AddForce(transform.up * velocity);
    }
	
	// Update is called once per frame
	void Update () {
        count++;
        if(count > 200)
        {
            Destroy(gameObject);
        }
       
	}

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(targetPlayer && collision.gameObject.tag == "Player")
        {
            // collision.gameObject.GetComponent<PlayerHealth>().ReduceHealthPoints(1);
            Destroy(gameObject);
        }
        if (collision.gameObject.tag == "Enemy" && !targetPlayer)
        {
            collision.gameObject.GetComponent<EnemyHealth>().ReduceHealthPoints(10);
            Destroy(gameObject);
        }
        else if(collision.gameObject.tag == "Wall")
        {
            Destroy(gameObject);
        }
    }
}
