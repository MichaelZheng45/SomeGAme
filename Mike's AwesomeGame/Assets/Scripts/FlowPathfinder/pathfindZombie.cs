using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pathfindZombie : MonoBehaviour {

	Rigidbody2D rb;
	Transform tr;
	EnemyHealth healthData;
	SpriteRenderer sr;
	public float vel;
	
	public string type;
	public float beserkerVel;

	float count = 0;

	public float range;
	public Sprite normal;
	public Sprite attacking;
	public int damage;

	bool attacked = false;
	float attackcount = 0;
	float whenAttack = .3f;
	// Use this for initialization
	void Start () {
		rb = GetComponent<Rigidbody2D>();
		tr = gameObject.transform;
		sr = GetComponent<SpriteRenderer>();
	}
	
	// Update is called once per frame
	void Update () {
		Vector2 dir = CreateMesh.Instance.getDirection(tr.position);
		Vector2 newVelocity = dir * vel;
		if(type == "beserker")
		{
			if((gameManager.Instance.GetTransformPos() - tr.position).magnitude < 15)
			{
				newVelocity = dir * beserkerVel;
			}
		}
		if(type == "tankerSpawn")
		{
			count += Time.deltaTime;
			if(count > .2)
			{
				count = 0;
				EnemySpawner.Instance.emitBlood(tr.position, 3);
			}
		}
		if(type == "Wraith")
		{
			if ((gameManager.Instance.GetTransformPos() - tr.position).magnitude < 5)
			{
				sr.color = Color.white;
			}
			else
			{
				sr.color = new Color32(255, 255, 255, 30);
			}
		}

		rb.velocity = newVelocity;
		rb.rotation = Mathf.Atan2(dir.y,dir.x) * Mathf.Rad2Deg + 180 + 90;

		damagePlayer();
	}

	void damagePlayer()
	{
		Vector2 pos = gameManager.Instance.GetTransformPos();
		if((pos - (Vector2)tr.position).magnitude < range )
		{
			attackcount += Time.deltaTime;
			sr.sprite = attacking;
			if(attackcount > whenAttack)
			{
				attackcount = 0;
				gameManager.Instance.damagePlayer(-damage);
			}
		}
		else
		{
			attackcount = 0;
			sr.sprite = normal;
		}
	}
}
