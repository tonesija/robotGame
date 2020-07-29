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
        //rb.MoveRotation(ballrb.velocity.x * maxRotation);
        float force = rotationSpeed / 4f;

        if(!(Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D))){
            force = 0;
            rb.angularVelocity = 0;

            LerpToRotation(0f);
        }else {
            rb.inertia = 0f;

            if(Input.GetKey(KeyCode.A)){
                force = -force;
            }
        }
        print(force);
        rb.AddForceAtPosition(new Vector2(force, 0f) * Time.fixedDeltaTime, new Vector2(rb.position.x, rb.position.y + 0.15f));
        Debug.DrawLine(rb.position, new Vector2(rb.position.x, rb.position.y + 0.15f));
        


        if(ballrb.velocity.x > 0 && rb.rotation <= -maxRotation){
            rb.freezeRotation = true;
        } else if(ballrb.velocity.x < 0 && rb.rotation >= maxRotation){
            rb.freezeRotation = true;
        } else {
            rb.freezeRotation = false;
        }
        
    }

    void LerpToRotation(float rotation){
        float z = rb.rotation;
        z = Mathf.Lerp(z, rotation, rotationSpeed * Time.fixedDeltaTime);
        rb.MoveRotation(z);
    }

    void LateUpdate() {
        /*
        if(rb.rotation > maxRotation && ballrb.velocity.magnitude > 0.8f){
            rb.freezeRotation = true;
            rb.rotation = maxRotation;
            
        }else if(rb.rotation < -maxRotation && ballrb.velocity.magnitude > 0.8f){
            rb.freezeRotation = true;
            rb.rotation = -maxRotation;

        }else{
            rb.freezeRotation = false;
            rb.rotation = 0f;
        }
        if(Mathf.Abs(ballrb.velocity.x) < 0.2f){
            rb.freezeRotation = false;
        }
        */
    }

    

    float CorrectRotation(){
        float rotation = transform.eulerAngles.z;
        if(rotation > maxRotation && rotation < 180){
            return maxRotation;
        }
        if(rotation < 360 - maxRotation && rotation > 189){
            return 360 - maxRotation;
        }
        return rotation;
        
    }
}
