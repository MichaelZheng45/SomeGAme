using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class PlayerWeaponList : MonoBehaviour {

    int timer; //the time left before the weapon dematerializes
    string nextPickUpSide; //which arm the weapon pick up will replace
    public int rightWeapon, leftWeapon; //the index of the current weapon the user is using
    public float offset; //distance from where the projectile would spawn in terms of the player

    public List<string> weaponNameArray; //string list of all weapons, helps remember which weapon is which
    public List<GameObject> weaponProjectile; //prefab for the projectile //***********Gonna be redone so it uses a bullet bank if possible

	public GameObject leftSpawnPosition, rightSpawnPosition;

    //VISUAL
    public GameObject rightArmVisual, leftArmVisual;
    public List<Sprite> armVisuals;

	//ammoCount
	public int leftAmmo, rightAmmo;
	public List<int> weaponAmmo;

    //FIRE RATE
    public List<int> weaponFireRateList;
    int rightFireRate; //this one will change based on weapon
    int rightCurrentFireCount = 0;

    int leftFireRate; // this one will change based on weapon
    int leftCurrentFireCount = 0;

    //SPREAD
    public List<int> weaponSpreadArray;

    //Parts
    public List<int> rightWeaponParts;
    public List<int> leftWeaponParts;

    //scoring
    GameObject scoreSpawner;
    int baseScore = 10;


    //ui
    public List<Sprite> partSprites;
	
	int newPart = -1;

	public Image partHolder;
	public Text leftPossibility;
	public Text rightPossibility;
	public Image leftWeaponImage;
	public Image rightWeaponImage;

	public Sprite emptyHolder;
	public List<Sprite> weaponIcons;
	// Use this for initialization
	void Start () {
        scoreSpawner = GameObject.FindGameObjectWithTag("scorer");
		rightAmmo = weaponAmmo[rightWeapon];
		leftAmmo = weaponAmmo[leftWeapon];
	}

	private void Update()
	{
		float rotation = gameObject.transform.rotation.eulerAngles.z;
		Debug.Log(rotation);
		//rightArmVisual,leftarmvisual will change by choosing from the sprite list
		rightArmVisual.GetComponent<SpriteRenderer>().sprite = armVisuals[rightWeapon];
		leftArmVisual.GetComponent<SpriteRenderer>().sprite = armVisuals[leftWeapon];

		//firing weapon
		if (Input.GetMouseButton(0) && leftCurrentFireCount > leftFireRate && (leftAmmo > 0 || leftWeapon == 0)) //left button
		{
			fireWeapon("left", leftWeapon);
			leftCurrentFireCount = 0;
			leftAmmo--;

			if (leftWeapon != 0)
			{
				gameManager.Instance.setLeftAmmoCount(leftAmmo, weaponAmmo[leftWeapon]);
			}
			else
			{
				gameManager.Instance.setLeftAmmoCount(1, 1);
			}

			soundManage.Instance.weaponSounds((weaponSound)leftWeapon);
		}

		if (Input.GetMouseButton(1) && rightCurrentFireCount > rightFireRate && (rightAmmo > 0 || rightWeapon == 0)) //right button
		{
			fireWeapon("right", rightWeapon);
			rightCurrentFireCount = 0;
			rightAmmo--;

			if(rightWeapon != 0)
			{
				gameManager.Instance.setRightAmmoCount(rightAmmo, weaponAmmo[rightWeapon]);
			}
			else
			{
				gameManager.Instance.setRightAmmoCount(1, 1);
			}

			soundManage.Instance.weaponSounds((weaponSound)rightWeapon);
		}

		if (Input.GetKeyDown(KeyCode.Space))
		{
			soundManage.Instance.useSound(sounds.GUNCOCK);
			newPart = -1;
			leftWeapon = 0;
			rightWeapon = 0;
		}

		seePossibilities(newPart);

		if(newPart > -1)
		{
			if (Input.GetKeyDown(KeyCode.Q))
			{
				soundManage.Instance.useSound(sounds.GUNCOCK);
				addPart(newPart, false);
				newPart = -1;
				leftAmmo = weaponAmmo[leftWeapon];

				if (leftWeapon != 0)
				{
					gameManager.Instance.setLeftAmmoCount(leftAmmo, weaponAmmo[leftWeapon]);
				}
				else
				{
					gameManager.Instance.setLeftAmmoCount(1, 1);
				}
			}
			else if (Input.GetKeyDown(KeyCode.E))
			{
				soundManage.Instance.useSound(sounds.GUNCOCK);
				addPart(newPart, true);
				newPart = -1;
				rightAmmo = weaponAmmo[rightWeapon];

				if (rightWeapon != 0)
				{
					gameManager.Instance.setRightAmmoCount(rightAmmo, weaponAmmo[rightWeapon]);
				}
				else
				{
					gameManager.Instance.setRightAmmoCount(1, 1);
				}
			}
		}

		

	}
	// Update is called once per frame
	void FixedUpdate () {

        //for fire rate
        rightFireRate = weaponFireRateList[rightWeapon];
        leftFireRate = weaponFireRateList[leftWeapon];
        rightCurrentFireCount++;
        leftCurrentFireCount++;
	}

	void seePossibilities(int index)
	{
		int id = index;
	
		if(index == -1)
		{
			partHolder.sprite = emptyHolder;
			leftPossibility.text = null;
			rightPossibility.text = null;
			leftWeaponImage.sprite = emptyHolder;
			rightWeaponImage.sprite = emptyHolder;
		}
		else
		{
			int zero = 0, one = 0, two = 0, three = 0, four = 0;
			rightWeaponParts.Add(id);
			int rightTotal = 0;
			for (int i = 1; i < 4; i++)
			{
				switch (rightWeaponParts[i])
				{
					case 0:
						zero++;
						break;
					case 1:
						one++;
						break;
					case 2:
						two++;
						break;
					case 3:
						three++;
						break;
					case 4:
						four++;
						break;
				}
			}

			if (one != 0)
			{
				rightTotal += 1;
			}

			if (two != 0)
			{
				rightTotal += 2;
			}

			if (three != 0)
			{
				rightTotal += 3;
			}

			if (four != 0)
			{
				rightTotal += 4;
			}
			rightWeaponParts.RemoveAt(3);

			//forleft
			zero = 0; one = 0; two = 0; three = 0; four = 0;
			leftWeaponParts.Add(id);
			int leftTotal = 0;
			for (int i = 1; i < 4; i++)
			{
				switch (leftWeaponParts[i])
				{
					case 0:
						zero++;
						break;
					case 1:
						one++;
						break;
					case 2:
						two++;
						break;
					case 3:
						three++;
						break;
					case 4:
						four++;
						break;
				}
			}

			if (one != 0)
			{
				leftTotal += 1;
			}

			if (two != 0)
			{
				leftTotal += 2;
			}

			if (three != 0)
			{
				leftTotal += 3;
			}

			if (four != 0)
			{
				leftTotal += 4;
			}
			leftWeaponParts.RemoveAt(3);

			//do UI
			partHolder.sprite = partSprites[index];
			leftPossibility.text = weaponNameArray[leftTotal];
			rightPossibility.text = weaponNameArray[rightTotal];
			leftWeaponImage.sprite = weaponIcons[leftTotal];
			rightWeaponImage.sprite = weaponIcons[rightTotal];
		}
	
}

	public void addPart(int index, bool onRight)
	{
		int id = index;
		int zero = 0, one = 0, two = 0, three = 0, four = 0;
		int total = 0;
		if (onRight)
		{
			rightWeaponParts.RemoveAt(0);
			rightWeaponParts.Add(id);

			for (int i = 0; i < 3; i++)
			{
				switch (rightWeaponParts[i])
				{
					case 0:
						zero++;
						break;
					case 1:
						one++;
						break;
					case 2:
						two++;
						break;
					case 3:
						three++;
						break;
					case 4:
						four++;
						break;
				}
			}

		}
		else
		{
			leftWeaponParts.RemoveAt(0);
			leftWeaponParts.Add(id);

			for (int i = 0; i < 3; i++)
			{
				switch (leftWeaponParts[i])
				{
					case 0:
						zero++;
						break;
					case 1:
						one++;
						break;
					case 2:
						two++;
						break;
					case 3:
						three++;
						break;
					case 4:
						four++;
						break;
				}
			}
		}

		if (one != 0)
		{
			total += 1;
		}

		if (two != 0)
		{
			total += 2;
		}

		if (three != 0)
		{
			total += 3;
		}

		if (four != 0)
		{
			total += 4;
		}
		Debug.Log("Zero: " + zero + "One:" + one + " two: " + two + " three: " + three + " Four: " + four + " OnRight: " + onRight);
		if (onRight)
		{
			rightWeapon = total;
		}
		else
		{
			leftWeapon = total;
		}
	}

    void fireWeapon(string arm, int whichweapon)
    {
        //find the player rotation then create the location with offset, if left then minus offset.
        Vector2 projectileSpawnLoc;
		float rotation = gameObject.transform.rotation.eulerAngles.z;
        if (arm == "right")
        {
			projectileSpawnLoc = rightSpawnPosition.transform.position;
        }
        else
        {
			projectileSpawnLoc = leftSpawnPosition.transform.position;
        }
    
        rotation += Random.Range(weaponSpreadArray[whichweapon] * -1f, weaponSpreadArray[whichweapon]);
        GameObject newProjectile = Instantiate(weaponProjectile[whichweapon], projectileSpawnLoc, Quaternion.Euler(0, 0, rotation)) as GameObject;
        if (newProjectile.GetComponent<TemporaryBulletScript>() != null)
        {
            newProjectile.GetComponent<TemporaryBulletScript>().targetPlayer = false;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "pickup")
        {
			gameManager.Instance.addScore(30);
			newPart = collision.gameObject.GetComponent<PartsScript>().partID;
			GetComponent<PlayerHealth>().reduceHealthPoints(0); //heals 1 hp
            Destroy(collision.gameObject);
        }
    }
}
