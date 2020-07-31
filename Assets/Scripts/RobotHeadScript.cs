using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RobotHeadScript : MonoBehaviour
{
    void OnJointBreak2D(Joint2D brokenJoint)
    {
        transform.parent = null;
        transform.GetComponent<Rigidbody2D>().mass = 0.1f;
        gameObject.layer = 0;
    }
}
