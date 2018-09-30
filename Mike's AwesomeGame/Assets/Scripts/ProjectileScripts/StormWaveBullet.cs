using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StormWaveBullet : MonoBehaviour {

    public float damage;
    int count = 0;
    public int velocity;
    public GameObject stormStarter;
    // Use this for initialization
    void Start()
    {
        gameObject.GetComponent<Rigidbody2D>().AddForce(transform.up * velocity);
    }

    // Update is called once per frame
    void Update()
    {
        count++;
        if (count > 200)
        {
            Destroy(gameObject);
        }

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.gameObject.tag == "Enemy")
        {
            collision.gameObject.GetComponent<EnemyHealth>().ReduceHealthPoints(damage);
            Instantiate(stormStarter, transform.position, transform.rotation);
            Destroy(gameObject);
        }
        else if (collision.gameObject.tag == "Wall")
        {
            Instantiate(stormStarter, transform.position, transform.rotation);
            Destroy(gameObject);
        }
    }
}
