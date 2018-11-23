using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlastBullet : MonoBehaviour {

    //RAPID and type 1 attacks
    public float damage;
    public bool targetPlayer = false;
    int count = 0;
    public int velocity;
    // Use this for initialization
    public GameObject bullet;
    public float degreesOffSet;
    void Start()
    {
        gameObject.GetComponent<Rigidbody2D>().AddForce(transform.up * velocity);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        count++;
        if (count > 200)
        {
            Destroy(gameObject);
        }

    }

    void spawnBullet(float degreesOff)
    {
        float rotation = transform.rotation.eulerAngles.z + degreesOff;
        GameObject newProjectile = Instantiate(bullet, gameObject.transform.position, Quaternion.Euler(0, 0, rotation)) as GameObject;
        newProjectile.GetComponent<TemporaryBulletScript>().targetPlayer = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.gameObject.tag == "Enemy")
        {
            collision.gameObject.GetComponent<EnemyHealth>().ReduceHealthPoints(damage);
            spawnBullet(-degreesOffSet);
            spawnBullet(0);
            spawnBullet(degreesOffSet);
            Destroy(gameObject);
        }
        else if (collision.gameObject.tag == "Wall")
        {
            Destroy(gameObject);
        }
    }
}
