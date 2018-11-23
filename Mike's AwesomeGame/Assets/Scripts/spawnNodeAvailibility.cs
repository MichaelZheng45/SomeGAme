using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spawnNodeAvailibility : MonoBehaviour {


	private void OnTriggerExit2D(Collider2D collision)
	{
		if(collision.tag == "despawner")
		EnemySpawner.Instance.addNewPoint(gameObject);		
	}

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.tag == "despawner")
			EnemySpawner.Instance.removePoint(gameObject);
	}
}
