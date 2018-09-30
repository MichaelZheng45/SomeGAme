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
    // Use this for initialization
    void Start()
    {
        gameObject.GetComponent<Rigidbody2D>().AddForce(transform.up * velocity);
        gameObject.GetComponent<SpriteRenderer>().color = fireColor[Random.Range(0, 6)];
    }

    // Update is called once per frame
    void Update()
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
            Destroy(gameObject);
        }
    }
}
