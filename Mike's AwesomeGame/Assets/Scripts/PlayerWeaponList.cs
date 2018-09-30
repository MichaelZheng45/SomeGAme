using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWeaponList : MonoBehaviour {

    int timer; //the time left before the weapon dematerializes
    string nextPickUpSide; //which arm the weapon pick up will replace
    public int rightWeapon, leftWeapon; //the index of the current weapon the user is using
    public float offset; //distance from where the projectile would spawn in terms of the player

    public List<string> weaponNameArray; //string list of all weapons, helps remember which weapon is which
    public List<GameObject> weaponProjectile; //prefab for the projectile //***********Gonna be redone so it uses a bullet bank if possible

    //VISUAL
    public GameObject rightArmVisual, leftArmVisual;
    public List<Sprite> armVisuals;

    //FIRE RATE
    public List<int> weaponFireRateList;
    int rightFireRate; //this one will change based on weapon
    int rightCurrentFireCount = 0;

    int leftFireRate; // this one will change based on weapon
    int leftCurrentFireCount = 0;

    //SPREAD
    public List<int> weaponSpreadArray;

    //Parts
    public int[] rightWeaponParts;
    public int[] leftWeaponParts;
    bool placePartOnRight; //a toggle to place part on right or left arm

    /*
     
    */
	// Use this for initialization
	void Start () {
        placePartOnRight = true;

    }
	
	// Update is called once per frame
	void FixedUpdate () {
        //rightArmVisual,leftarmvisual will change by choosing from the sprite list
        rightArmVisual.GetComponent<SpriteRenderer>().sprite = armVisuals[rightWeapon];
        leftArmVisual.GetComponent<SpriteRenderer>().sprite = armVisuals[leftWeapon];

        rightFireRate = weaponFireRateList[rightWeapon];
        leftFireRate = weaponFireRateList[leftWeapon];

        rightCurrentFireCount++;
        leftCurrentFireCount++;      
     
		if(Input.GetMouseButton(0) && leftCurrentFireCount > leftFireRate) //left button
        {
            fireWeapon("left",leftWeapon); 
            leftCurrentFireCount = 0;
        }

        if(Input.GetMouseButton(1) && rightCurrentFireCount > rightFireRate) //right button
        {
            fireWeapon("right",rightWeapon); 
            rightCurrentFireCount = 0;
        }

        if(Input.GetKeyDown(KeyCode.Space))
        {
            leftWeapon++;
            rightWeapon++;
            if(leftWeapon > 9 || rightWeapon > 9)
            {
                leftWeapon = 0;//leftweapon %= array.lemgth
                rightWeapon = 0;
            }
        }
	}

    void fireWeapon(string arm, int whichweapon)
    {
        //find the player rotation then create the location with offset, if left then minus offset.
        Vector2 projectileSpawnLoc;
        float rotation = gameObject.transform.rotation.eulerAngles.z;

        if (arm == "right")
        {
            projectileSpawnLoc = new Vector2(Mathf.Cos(rotation * Mathf.PI / 180) * offset,
               Mathf.Sin(rotation * (Mathf.PI / 180)) * offset);
        }
        else
        {
            projectileSpawnLoc = new Vector2(Mathf.Cos((rotation + 180) * Mathf.PI / 180) * offset,
                Mathf.Sin((rotation + 180) * Mathf.PI / 180) * offset);
        }

        if (whichweapon != 0)
        {
            rotation = transform.rotation.eulerAngles.z + Random.Range(weaponSpreadArray[whichweapon] * -1f, weaponSpreadArray[whichweapon]);
            projectileSpawnLoc = new Vector2(gameObject.transform.position.x + projectileSpawnLoc.x, gameObject.transform.position.y + projectileSpawnLoc.y);
            GameObject newProjectile = Instantiate(weaponProjectile[whichweapon], projectileSpawnLoc, Quaternion.Euler(0, 0, rotation)) as GameObject;
            if (newProjectile.GetComponent<TemporaryBulletScript>() != null)
            {
                newProjectile.GetComponent<TemporaryBulletScript>().targetPlayer = false;
            }
        }
        else //ZZZAAAAAA WWWWAAAAARRRRRUUUUUUDDDOOOOOOOOO
        {
            Instantiate(weaponProjectile[whichweapon], transform.position, Quaternion.Euler(new Vector3(0, 0, 0)));
            foreach (GameObject fooObj in GameObject.FindGameObjectsWithTag("Enemy"))
            {
                fooObj.GetComponent<EnemyRobotPatterns>().activateZaWarudo();
                fooObj.GetComponent<EnemyAttack>().activateZaWarudo();

            }
            foreach (GameObject fooObj in GameObject.FindGameObjectsWithTag("Projectile"))
            {
                if (fooObj.GetComponent<TemporaryBulletScript>() != null)
                {
                    fooObj.GetComponent<TemporaryBulletScript>().activateZaWarudo();
                }
            }
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "pickup")
        {
            //add new part and place new weapon
        }
    }
}
