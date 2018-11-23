using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class EnemySpawner : MonoBehaviour {

	private static EnemySpawner _instance;
	public static EnemySpawner Instance { get { return _instance; } }

	private void Awake()
	{
		if (_instance != null && _instance != this)
		{
			Destroy(this.gameObject);
		}
		else
		{
			_instance = this;
		}
	}

	float spawnReduceCount = 0;
	public int index = 0;
	public GameObject monster;
	public GameObject miniSpawns; //for the tankers
	public List<float> spawnRate;
	float spawnCount;

	public List<GameObject> specials;
	public List<float> specialSpawnRate;
	float specialSpawnCount;

	public List<GameObject> spawnNodelist = new List<GameObject>();
	public Transform bloodPosition;
	public ParticleSystem particleblood;

	public Transform damagePosition;
	public ParticleSystem particledamage;

	public Transform stormPosition;
	public ParticleSystem particleStorm;

	// Use this for initialization
	void Start () {
		
    }

	private void Update()
	{
		float newTime = Time.deltaTime;
		spawnCount += newTime;
		specialSpawnCount += newTime;
		spawnReduceCount += newTime;

		//regular monsters
		if (spawnCount > spawnRate[index])
		{
			spawnMonster();
			spawnCount = 0;
		}

		//specials
		if (specialSpawnCount > specialSpawnRate[index])
		{
			spawnSpecials();
			specialSpawnCount = 0;
		}

		//spawnReducer
		if (spawnReduceCount > 30 && index < spawnRate.Count - 1)
		{
			spawnReduceCount = 0;
			index++;
		}
	}

	void spawnSpecials()
	{
		int randIndex = Random.Range(0, spawnNodelist.Count);
		int randSpecial = Random.Range(0, specials.Count);
		Instantiate(specials[randSpecial], spawnNodelist[randIndex].transform.position, Quaternion.Euler(new Vector2(0, 0)));
	}

	void spawnMonster()
	{
		int randIndex = Random.Range(0, spawnNodelist.Count);
		Instantiate(monster, spawnNodelist[randIndex].transform.position, Quaternion.Euler(new Vector2(0, 0)));
	}

	public void addNewPoint(GameObject newNode)
	{
		if(!spawnNodelist.Contains(newNode))		
			spawnNodelist.Add(newNode);
	}

	public void removePoint(GameObject node)
	{
		if(spawnNodelist.Contains(node))
			spawnNodelist.Remove(node);
	}

	public void emitBlood(Vector2 pos, int count = 5)
	{
		bloodPosition.position = pos;
		particleblood.Emit(count);
	}

	public void emitDamage(Vector2 pos, int count = 5)
	{
		damagePosition.position = pos;
		particledamage.Emit(count);
	}

	public void emitStorm(Vector2 pos, int count = 5)
	{
		stormPosition.position = pos;
		particleStorm.Emit(count);
	}

	public void SpawnMonsterAt(Vector2 pos, int count)
	{
		for(int i = 0; i < count; i++)
		{
			Instantiate(miniSpawns, pos, Quaternion.Euler(new Vector2(0, 0)));
		}
	}
}
