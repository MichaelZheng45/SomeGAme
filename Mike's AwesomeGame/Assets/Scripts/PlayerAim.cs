using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAim : MonoBehaviour {

	// Use this for initialization
	void Start () {
        Cursor.visible = false;
	}
	
	// Update is called once per frame
	void Update () {
        modifyRotation();
	}

    void modifyRotation()
    {
        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        float hypo = Vector2.Distance(mousePos, gameObject.transform.position);
        float xDistance = mousePos.x - transform.position.x;
        float theta = Mathf.Acos(xDistance / hypo) * (180 / Mathf.PI);
        if (mousePos.y <transform.position.y)
        {
            theta = 360f - theta;
        }
        gameObject.transform.rotation = Quaternion.Euler(0, 0, theta - 90);
    }

}
