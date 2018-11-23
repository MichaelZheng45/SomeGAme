using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TemporaryBulletScript : MonoBehaviour {
    //RAPID and type 1 attacks
    public float damage;
    public bool targetPlayer = false;
    int count = 0;
    public int velocity;
    public bool destroyBullet = true;
	// Use this for initialization


	int lightCount = 0;
	bool highlight = false;
	SpriteRenderer userSprite;
    void Start () {
        gameObject.GetComponent<Rigidbody2D>().AddForce(transform.up * velocity);
		userSprite = GetComponent<SpriteRenderer>();
    }
	
	// Update is called once per frame
	void FixedUpdate()
	{
        count++;
        if (count > 200)
        {
            Destroy(gameObject);
        }

		if(targetPlayer)
		{
			lightCount++;
			if(lightCount > 10)
			{
				lightCount = 0;
				if(highlight)
				{
					userSprite.color = new Color32(242, 253, 130,255);
					highlight = false;
				}
				else
				{
					//lightup.
					userSprite.color = new Color32(255, 50, 60,255);
					
					highlight = true;
				}
			}
		}
         
	}

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(targetPlayer && collision.gameObject.tag == "Player")
        {
            collision.gameObject.GetComponent<PlayerHealth>().reduceHealthPoints(-2);
            Destroy(gameObject);
        }
        if (collision.gameObject.tag == "Enemy" && !targetPlayer)
        {
            collision.gameObject.GetComponent<EnemyHealth>().ReduceHealthPoints(damage);
            if(destroyBullet)
            {

                Destroy(gameObject);
            }
        }
        else if(collision.gameObject.tag == "Wall")
        {
            Destroy(gameObject);
        }
    }
}
