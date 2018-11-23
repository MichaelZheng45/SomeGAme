using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour {
    public bool followMouse = false;
    public GameObject player;
	// Use this for initialization
	public bool isCamera = false;

	float count = 0;
	bool shake = false;
	
	void Start () {
		if(isCamera)
		{
			Screen.SetResolution(1280, 1024, true);
		}
	}
	
	// Update is called once per frame
	void Update () {
        if(!followMouse)
        {
            gameObject.transform.position = new Vector3(player.transform.position.x, player.transform.position.y, transform.position.z);

        }
        else
        {
            Vector2 mPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            gameObject.transform.position = new Vector3(mPos.x,mPos.y, gameObject.transform.position.z);
        }

		if(isCamera && shake)
		{
			//do shake
			Vector3 pos = gameObject.transform.position;
			float curTime = Time.time;
			gameObject.transform.position = new Vector3(pos.x + Random.Range(-1,1), pos.y + Random.Range(-1, 1), pos.z);
			count += Time.deltaTime;
			if(count > .6)
			{
				count = 0;
				shake = false;
			}
		}
    }

	public void cameraShake()
	{
		shake = true;
		count = 0;
	}
}
