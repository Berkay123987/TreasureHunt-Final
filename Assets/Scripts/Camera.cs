using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] private Transform player;
    public float minX = -3.49f; // Sahnenin sol sınırı
    public float maxX = 40.61f;  // Sahnenin sağ sınırı

    private void Update()
    {
        float targetX = player.position.x;
        // Kameranın X koordinatını sınırlar içinde tut
        targetX = Mathf.Clamp(targetX, minX, maxX);

        transform.position = new Vector3(targetX, transform.position.y, transform.position.z);
    }
}
