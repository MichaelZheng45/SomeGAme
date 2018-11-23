using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class nodePath : MonoBehaviour {

	//flow data
	public float costSoFar;
	public Vector2 dir;

	public bool CompleteIsolation;
	int maxArrayIndex;
	public Vector2 index;
	public bool meshFinished;
	GameObject[,] meshList;
	
	int layerMask = 1 << 8;
	//surrounding nodes, if true then it isn't obstructed
	public bool[,] adjacent = new bool[3,3]; 
	// Use this for initialization
	void Start() {
		meshFinished = false;
		adjacent = new bool[3, 3];
	}

	// Update is called once per frame
	void Update() {
		/*
		if (meshFinished == false)
		{
			if (CreateMesh.Instance.checktrue())
			{
				int availablePoints = 8;
				meshList = CreateMesh.Instance.meshList;
				maxArrayIndex = CreateMesh.Instance.size - 1;
				for(int x = 0; x < 3; x++)
				{
					for(int y = 0; y < 3; y++)
					{	
						if(y == 1 && x == 1)
						{
							adjacent[x, y] = false;
						}
						else
						{
//combining with x and minus a 1 will look at all nearby objects surrounding it
							if ((int)index.x+x-1 > maxArrayIndex || (int)index.x +x-1 < 0 || (int)index.y+y-1 > maxArrayIndex || (int)index.y+y-1 < 0)
							{
								adjacent[x, y] = false;
							}
							else
							{
								GameObject atNode = meshList[(int)index.x + x - 1, (int)index.y + y - 1];
								RaycastHit2D hit;
								if(x-1 != 0 && y-1 != 0)
								{
									hit = Physics2D.Raycast(transform.position, new Vector2(x - 1, y - 1), 1.5f, layerMask);
								}
								else
								{
									hit = Physics2D.Raycast(transform.position, new Vector2(x - 1, y - 1), 1f, layerMask); 
								}

								if (hit.collider == null)
								{
									adjacent[x, y] = true;
									Debug.DrawRay(transform.position, new Vector2(x - 1, y - 1) * 1, Color.green);
								}
								else
								{
									availablePoints--;
									adjacent[x, y] = false;
									Debug.DrawRay(transform.position, new Vector2(x - 1, y - 1) * hit.distance, Color.red);

								}
							}
						}	
					}
				}

				if(availablePoints <= 0)
				{
					CompleteIsolation = true;
					gameObject.GetComponent<SpriteRenderer>().color = Color.yellow;
				}
				meshFinished = true;
			}

		}
		*/

	}

	public void setIndex(Vector2 i)
	{
		index = i;
	}

	public Vector2 getDir()
	{
		return dir;
	}
}
