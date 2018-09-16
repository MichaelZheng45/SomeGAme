using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWeaponList : MonoBehaviour {

    public List<string> weaponNameArray; //string list of all weapons, helps remember which weapon is which
    public List<GameObject> weaponProjectile; //prefab for the projectile //***********Gonna be redone so it uses a bullet bank if possible
    int timer; //the time left before the weapon dematerializes
    string nextPickUpSide; //which arm the weapon pick up will replace
    int rightWeapon, leftWeapon; //the index of the current weapon the user is using
    public float offset; //distance from where the projectile would spawn in terms of the player

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
    bool placePartonRight;

    /*
     
    */
	// Use this for initialization
	void Start () {
        placePartonRight = true;
        rightWeapon = 6;
        leftWeapon = rightWeapon;
        rightFireRate = weaponFireRateList[0];
        leftFireRate = weaponFireRateList[0];
        //rightArmVisual,leftarmvisual will change by choosing from the sprite list
        //rightArmVisual.GetComponent<SpriteRenderer>().sprite = armVisuals[0];
        //leftArmVisual.GetComponent<SpriteRenderer>().sprite = armVisuals[0];
    }
	
	// Update is called once per frame
	void FixedUpdate () {
        if(rightCurrentFireCount < rightFireRate)
        {
            rightCurrentFireCount++;
        }

        if (leftCurrentFireCount < leftFireRate)
        {
            leftCurrentFireCount++;
        }
     
		if(Input.GetMouseButton(0) && leftCurrentFireCount == leftFireRate) //left button
        {
            fireWeapon("left",0/* leftWeapon*/); //the zero is temporary
            leftCurrentFireCount = 0;
        }

        if(Input.GetMouseButton(1) && rightCurrentFireCount == rightFireRate) //right button
        {
            fireWeapon("right",0 /*rightWeapon*/); //the zero is temporary
            rightCurrentFireCount = 0;
        }
	}

    void fireWeapon(string arm, int whichweapon)
    {
        //find the player rotation then create the location with offset, if left then minus offset.
        Vector2 projectileSpawnLoc;
        float rotation = gameObject.transform.rotation.eulerAngles.z;
        Debug.Log(rotation);

        if(arm == "right")
        {
            projectileSpawnLoc = new Vector2(Mathf.Cos(rotation * Mathf.PI / 180) * offset,
               Mathf.Sin(rotation * (Mathf.PI / 180)) * offset);
        }
        else
        {
            projectileSpawnLoc = new Vector2(Mathf.Cos((rotation + 180) * Mathf.PI / 180) * offset,
                Mathf.Sin((rotation + 180) * Mathf.PI / 180) * offset);
        }

        //base, it is if statement to the weapon id
        if(whichweapon == 0) //#6 The Rapid
        {
            rotation = transform.rotation.eulerAngles.z + Random.Range(weaponSpreadArray[whichweapon] * -1f, weaponSpreadArray[whichweapon]);
            projectileSpawnLoc = new Vector2(gameObject.transform.position.x + projectileSpawnLoc.x, gameObject.transform.position.y + projectileSpawnLoc.y);
            GameObject newProjectile = Instantiate(weaponProjectile[whichweapon], projectileSpawnLoc, Quaternion.Euler(0, 0, rotation)) as GameObject;
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
