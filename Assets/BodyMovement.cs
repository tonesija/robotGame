using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BodyMovement : MonoBehaviour
{
    public float maxRotation;
    public float rotationSpeed;

    Rigidbody2D rb;
    Rigidbody2D ballrb;
    WheelJoint2D wj;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        wj = GetComponent<WheelJoint2D>();
        ballrb = transform.parent.GetChild(transform.GetSiblingIndex() - 1).GetComponent<Rigidbody2D>();
    }

    
    void FixedUpdate()
    {
        if(Mathf.Abs(ballrb.velocity.x) > 0.4f) {
            print(rb.rotation);
            if(Mathf.Abs(rb.rotation) < maxRotation - 1f){
                rb.AddForceAtPosition(new Vector2(ballrb.velocity.x / 10f, 0) * Time.fixedDeltaTime, new Vector2(transform.position.x, transform.position.y + 0.05f));
            } else {
                if(rb.rotation > 0){
                    rb.angularVelocity = 0;
                    rb.MoveRotation(maxRotation);
                }else{
                    rb.angularVelocity = 0;
                    rb.MoveRotation(-maxRotation);
                }
            }
        } else {
            rb.angularVelocity = 0;
            rb.MoveRotation(0);
        }
        
    }

    

    float CorrectRotation(){
        float rotation = transform.eulerAngles.z;
        if(rotation > maxRotation && rotation < 180){
            return maxRotation;
        }
        if(rotation < 360 - maxRotation && rotation > 189){
            return 360 - maxRotation;
        }
        print("Good");
        return rotation;
        
    }
}
