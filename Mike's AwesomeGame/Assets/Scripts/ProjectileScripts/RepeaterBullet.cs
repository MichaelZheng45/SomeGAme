using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RepeaterBullet : MonoBehaviour {

    GameObject player;
    public int returnDistance;
    public float damage;
    bool targetPlayer = false;
    public int velocity;
    // Use this for initialization
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        gameObject.GetComponent<Rigidbody2D>().AddForce(transform.up * velocity);
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 distance = transform.position - player.transform.position;

        if(distance.magnitude > returnDistance)
        {
            targetPlayer = true;
        }

        if (targetPlayer)
        {
            gameObject.GetComponent<Rigidbody2D>().velocity *= 0f;
            if (distance.magnitude < .5)
            {
                Destroy(gameObject);
            }

            distance = distance.normalized * .5f;

            transform.Rotate(Vector3.forward*360*2*Time.deltaTime);
            //gameObject.transform.rotation = Quaternion.Euler(new Vector3(0, 0,transform.rotation.z + 10));
            gameObject.transform.position -= (Vector3)distance;
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
            targetPlayer = true;
        }
    }
}
