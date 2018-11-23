using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragonFire : MonoBehaviour {

    public Color[] fireColor = new Color[6];

    public float damage;
    int count = 0;
    public int velocity;

    public float spreadRate;
    public int maxSize;
	Rigidbody2D rigidB;
    // Use this for initialization
    void Start()
    {
		rigidB = gameObject.GetComponent<Rigidbody2D>();
		rigidB.AddForce(transform.up * velocity); 
	}

    // Update is called once per frame
    void FixedUpdate()
	{
        count++;
        if (count > maxSize)
        {
            Destroy(gameObject);
        }

        transform.localScale += new Vector3(spreadRate, spreadRate, 0);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            collision.gameObject.GetComponent<EnemyHealth>().ReduceHealthPoints(damage);
        }
        else if (collision.gameObject.tag == "Wall")
        {
			rigidB.velocity *= 0;
        }
    }
}
