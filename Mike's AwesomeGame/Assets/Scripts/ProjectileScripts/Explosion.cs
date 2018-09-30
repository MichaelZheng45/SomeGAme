using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour {

    public Sprite explosionSp;
    public bool activated = false; //temp pub

    public int damage;
    public float spreadRate;
    public int maxSize;
    public int count = 0;
    // Use this for initialization
    public bool isRipperBomb;
    public GameObject ripperBomb;

    public Color[] fireColor = new Color[6];

    public int secondCount = 0;
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		if(activated)
        {
            count++;
            if(count > maxSize)
            {
                Destroy(gameObject);
            }

            transform.localScale += new Vector3(spreadRate,spreadRate,0);
        }

        secondCount++;
        if(secondCount > 100 +maxSize)
        {
            Destroy(gameObject);
        }
        
	}

    public void activateExplosion(bool ripperCheck = false)
    {
        if(ripperCheck)
        {
            isRipperBomb = true;
            gameObject.GetComponent<SpriteRenderer>().color = fireColor[Random.Range(0, 6)];
        }
        activated = true;
        gameObject.GetComponent<SpriteRenderer>().sprite = explosionSp;
        transform.parent = null;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Enemy" && activated)
        {
            collision.gameObject.GetComponent<EnemyHealth>().ReduceHealthPoints(damage);
            if(isRipperBomb)
            {
                GameObject newProjectile = Instantiate(ripperBomb,collision.gameObject.transform.position, transform.rotation) as GameObject;
                newProjectile.GetComponent<Explosion>().activateExplosion();
            }
        }
    }
}
