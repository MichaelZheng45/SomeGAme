using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShatterProjectile : MonoBehaviour {

    //RAPID and type 1 attacks
    public float damage;
    public bool startShatter = false;
    int count = 0;
    public int velocity;
    public int spawnAmt;

    public GameObject bullet;
    int spawnCount;
    public int maxCount;
    // Use this for initialization
    void Start()
    {
        gameObject.GetComponent<Rigidbody2D>().AddForce(transform.up * velocity);
    }

    // Update is called once per frame
    void FixedUpdate()
	{
        count++;
        if (count >60)
        {
            Destroy(gameObject);
        }


        spawnCount++;
        if(spawnCount > maxCount)
        {
            spawnCount = 0;
            int rot = 0;
            for (int i = 0; i < spawnAmt; i++)
            {
                int addRot = Random.Range(0, 360 / spawnAmt - 1);
                float rotation = rot + addRot;
                GameObject newProjectile = Instantiate(bullet, gameObject.transform.position, Quaternion.Euler(0, 0, rotation)) as GameObject;
                rot += 360 / spawnAmt; ;
            }
        }
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            collision.gameObject.GetComponent<EnemyHealth>().ReduceHealthPoints(damage);
        }
        else if (collision.gameObject.tag == "Wall")
        {
            Destroy(gameObject);
        }
    }
}
