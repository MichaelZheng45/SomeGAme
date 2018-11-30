using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class generatorScript : MonoBehaviour {

	Transform pos;
	// Use this for initialization
	float reloadCount = 0;
	float timeCount = 0;
	bool onCooldown;
	SpriteRenderer sr;

	public int range;

	public Color32 charge;
	public Color32 used;
	public List<GameObject> partslist;
	void Start () {
		pos = gameObject.transform;
		sr = gameObject.GetComponent<SpriteRenderer>();
	}
	
	// Update is called once per frame
	void Update () {

		if ((gameManager.Instance.GetTransformPos() - pos.position).magnitude < range)
		{
			if(onCooldown == false)
			{
				gameManager.Instance.sliderAddData(4,timeCount, charge);
				timeCount += Time.deltaTime;
				if(timeCount > 4)
				{
					//spawnitem
					Instantiate(partslist[Random.Range(0, partslist.Count)],pos.position,pos.rotation);
					timeCount = 0;
					onCooldown = true;
				}
			}
			else
			{
				gameManager.Instance.sliderAddData(20,20 - reloadCount, used);
			}
		}
		else
		{
			timeCount -= Time.deltaTime;
			if(timeCount < 0)
			{
				timeCount = 0;
			}
		}

		if(onCooldown)
		{
			reloadCount += Time.deltaTime;
			if(reloadCount > 20)
			{
				reloadCount = 0;
				onCooldown = false;
			}
			sr.color = new Color32(75, 75, 75, 255);
		}
		else
		{
			sr.color = new Color32(255,255,255,255);
		}
	}
}
