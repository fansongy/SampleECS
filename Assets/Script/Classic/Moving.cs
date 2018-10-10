using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Moving : MonoBehaviour {

    private float speed;
    private float minX;

	void Start () {
        speed = Random.Range(2f, 5f);
        minX = transform.position.x;
	}

    void Update()
    {
        Vector3 cur = transform.position;
        if(cur.x+speed *Time.deltaTime > -minX)
        {
            transform.position = new Vector3(minX, 0,cur.z);
        }
        else
        {
            transform.position = new Vector3(cur.x + speed * Time.fixedDeltaTime, 0, cur.z);
        }
        transform.rotation = Quaternion.Euler(0, -90, 0);
    }
}






