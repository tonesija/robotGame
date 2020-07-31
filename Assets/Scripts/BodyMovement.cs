using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BodyMovement : MonoBehaviour
{
    public float maxRotation;
    public float rotationSpeed;

    Rigidbody2D rb;
    Rigidbody2D ballrb;

    Rigidbody2D gunrb;
    Rigidbody2D headrb;
    WheelJoint2D wj;

    ParticleSystem ps;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        wj = GetComponent<WheelJoint2D>();
        ps = GetComponent<ParticleSystem>();
        gunrb = transform.parent.GetChild(transform.GetSiblingIndex() + 2).GetComponent<Rigidbody2D>();
        ballrb = transform.parent.GetChild(transform.GetSiblingIndex() - 1).GetComponent<Rigidbody2D>();
        headrb = transform.parent.GetChild(transform.GetSiblingIndex() + 1).GetComponent<Rigidbody2D>();
        rb.inertia = 0f;
    }

    
    void FixedUpdate()
    {   
        float force = rotationSpeed / 4f;

        if(!(Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D))){
            force = 0;
            rb.angularVelocity = 0;

            LerpToRotation(0f);
        }else {
            if(Input.GetKey(KeyCode.A)){
                force = -force;
            }
            rb.AddForceAtPosition(new Vector2(force, 0f) * Time.fixedDeltaTime, new Vector2(rb.position.x, rb.position.y + 0.15f));
        }

        if(ballrb.velocity.x > 0 && rb.rotation <= -maxRotation){
            LerpToRotation(-maxRotation);
        } else if(ballrb.velocity.x < 0 && rb.rotation >= maxRotation){
            LerpToRotation(maxRotation);
        } else {
            LerpToRotation(0f);
        }

        if(Mathf.Abs(ballrb.velocity.x) > 4f){
            EmitSpeedParticles();
        }
        
    }

    void LerpToRotation(float rotation){
        float z = rb.rotation;
        z = Mathf.Lerp(z, rotation, rotationSpeed * Time.fixedDeltaTime);
        rb.MoveRotation(z);
    }

    
    void OnJointBreak2D(Joint2D brokenJoint)
    {
        transform.parent = null;
        headrb.transform.parent = null;
        gunrb.transform.parent = null;
        Destroy(gunrb.transform.GetComponent<RobotGunScript>());
        rb.freezeRotation = false;
        gameObject.layer = 0;
        rb.mass = 0.3f;
        Destroy(this);
    }

    void EmitSpeedParticles(){
        ParticleSystem.EmitParams emitParams = new ParticleSystem.EmitParams();
        int count = 1;

        if(Mathf.Abs(ballrb.velocity.x) > 5){
            count++;
        }

        if(Mathf.Abs(ballrb.velocity.x) > 6){
            count++;
        }
        
        ps.Emit(emitParams, count);
    }
}
