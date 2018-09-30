using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElectricInline : MonoBehaviour {

    public Vector2 point1Pos;
    public Vector2 point2Pos;

    int count = 0;
    // Use this for initialization
    void Start()
    {
        if(transform.parent != null)
        {
            transform.parent = null;
        }

    }

    // Update is called once per frame
    void Update()
    {
        count++;
        if(count > 10)
        {
            Destroy(gameObject);
        }

        updatePointTwoValues(point2Pos);
    }
    public void updatePointOne(Vector2 point)
    {
        point1Pos = point;
    }
    public void updatePointTwoValues(Vector2 point)
    {
        point2Pos = point;
        modifyPosition();
        modifyRotation();
        modifySize();

    }

    void modifyPosition()
    {
        gameObject.transform.position = new Vector2((point1Pos.x - point2Pos.x) / 2f + point2Pos.x, (point1Pos.y - point2Pos.y) / 2f + point2Pos.y);
    }
    void modifyRotation()
    {
        float hypo = Vector2.Distance(point1Pos, point2Pos);
        float xDistance = point1Pos.x - point2Pos.x;
        float theta = Mathf.Acos(xDistance / hypo) * (180 / Mathf.PI);
        if (point1Pos.y < point2Pos.y)
        {
            theta = 360f - theta;
        }
        gameObject.transform.rotation = Quaternion.Euler(0, 0, theta);
    }
    void modifySize()
    {
        gameObject.transform.localScale = new Vector2(Vector2.Distance(point1Pos, point2Pos), transform.localScale.y);
    }
}
