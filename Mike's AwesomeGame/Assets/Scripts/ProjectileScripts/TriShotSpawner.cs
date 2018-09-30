using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriShotSpawner : MonoBehaviour {

    public float degreesOffSet;
    public GameObject bullet;
	// Use this for initialization
	void Start () {
        spawnBullet(-degreesOffSet);
        spawnBullet(0);
        spawnBullet(degreesOffSet);
        Destroy(gameObject);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void spawnBullet(float degreesOff)
    {
        float rotation = transform.rotation.eulerAngles.z + degreesOff;
        GameObject newProjectile = Instantiate(bullet, gameObject.transform.position, Quaternion.Euler(0, 0, rotation)) as GameObject;
        newProjectile.GetComponent<TemporaryBulletScript>().targetPlayer = false;
    }
}
