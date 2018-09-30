using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour {

    public List<GameObject> enemyTypes;
    public List<float> enemySpawnChance;
    public List<GameObject> spawnNodes;

    float count = 0;
    public float spawnRate;
	// Use this for initialization
	void Start () {
        foreach (GameObject fooObj in GameObject.FindGameObjectsWithTag("SpawnNodes"))
        {
            spawnNodes.Add(fooObj);
        }
    }
	
	// Update is called once per frame
	void Update () {
        count += Time.deltaTime;
        if(count > spawnRate)
        {
            spawnEnemies();
        }
	}

    void spawnEnemies()
    {
        //find a suitable node to spawn at
        int useNode = 0 ;
        bool nodeFound = false ;
        while(nodeFound == false)
        {
            useNode = Random.Range(0, spawnNodes.Count);
            if(spawnNodes[useNode].GetComponent<spawnNodeAvailibility>().canSpawn())
            {
                nodeFound = true;
            }
        }
        GameObject node = spawnNodes[useNode];
        for(int i = 0; i < enemyTypes.Count; i++)
        {
            if(Random.value <= enemySpawnChance[i])
            {
                Instantiate(enemyTypes[i],node.transform.position, node.transform.rotation);
                count = 0;
                return;
            }
        }
    }
}
