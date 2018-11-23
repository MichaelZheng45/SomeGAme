using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class EnemyHealth : MonoBehaviour {

    public float HealthPoints;
    public float damageShieldPwr;
    float CurrentHealthPoints;
	public string type;
    // Use this for initialization

	public GameObject deathExplosion;
	Transform tr;
    void Start () {
		tr = gameObject.GetComponent<Transform>();
        CurrentHealthPoints = HealthPoints;
	}
	
	// Update is called once per frame
	void Update () {

		if(CurrentHealthPoints <= 0)
        {
			soundManage.Instance.useSound(sounds.ENEMYDEATH);
			if (type == "tanker")
			{
				EnemySpawner.Instance.SpawnMonsterAt(tr.position,1);
			}
			gameManager.Instance.addScore(10);
			Instantiate(deathExplosion, tr.position, tr.rotation);
			EnemySpawner.Instance.emitBlood(tr.position);
			Destroy(gameObject);
        }
	}

    public float GetHealthPoints()
    {
        return CurrentHealthPoints;
    }

    public void ReduceHealthPoints(float damage)
    {

		EnemySpawner.Instance.emitDamage(tr.position, 1);
		CurrentHealthPoints -= (damage);
        
    }
}
