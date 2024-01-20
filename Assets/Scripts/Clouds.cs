using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Clouds : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.left*Time.deltaTime*1.1f);
        if (transform.position.x < -6)
            transform.position = new Vector3(175.92f, -6.4f, 0);
    }
}
