using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    Transform toFollow;
    void Start()
    {
        toFollow = GameObject.FindGameObjectWithTag("Player").transform;
    }

    void Update()
    {
        transform.position = new Vector3(toFollow.position.x, toFollow.position.y, transform.position.z);
    }
}
