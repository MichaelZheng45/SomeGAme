using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PartsScript : MonoBehaviour {

    public int partID; //numbers 0 to 4

    float count = 0;
    int max = 10;
    private void FixedUpdate()
    {
        count += Time.deltaTime;
        if(count > max)
        {
            Destroy(gameObject);
        }
    }
}
