using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RobotMovement : MonoBehaviour
{
    Rigidbody2D rb;
    public float maxSpeed;
    float speed;
    public float deceleration;
    public float jumpForce;

    public float maxAngularVelocity;
    float currAngularVelocity;

    int robotLayerMask = 1 << 8; //mask for layer 8 (layer 8 = Robot part layer)

    bool grounded; //is robot on the ground, can robot jump?

    float wheelRadius;

    void Start(){
        rb = GetComponent<Rigidbody2D>();
        robotLayerMask = ~robotLayerMask; //everything except layer 8
        wheelRadius = GetComponent<CircleCollider2D>().bounds.size.y / 2;
    }

  
    void FixedUpdate()
    {

        UpdateGrounded();

        //jump
        if(Input.GetKey(KeyCode.Space) && grounded){
            rb.AddForce(new Vector2(0f, jumpForce), ForceMode2D.Impulse);
        }

        //sprint
        speed = maxSpeed;
        currAngularVelocity = maxAngularVelocity;
        if(Input.GetKey(KeyCode.LeftShift)){
            speed *= 2;
            currAngularVelocity *= 2;
        }
        

        //left
        if(Input.GetKey(KeyCode.A)){
            if(rb.velocity.x > 0){
                rb.AddTorque(2*speed);
            }else {
                rb.AddTorque(speed);
            }
            ClampAngularVelocity();
            FlipRobot(-1f);
        }

        //right
        if(Input.GetKey(KeyCode.D)){
            if(rb.velocity.x < 0){
                rb.AddTorque(-2*speed);
            }else {
                rb.AddTorque(-speed);
            }
            ClampAngularVelocity();
            FlipRobot(1f);
        }

        //handbreak
        if(Input.GetKey(KeyCode.C) && grounded){
            rb.angularVelocity = 0;
            rb.velocity = new Vector2(0f, 0f);
        }

        Decelerate();
    }

    void Decelerate(){
        if(rb.velocity.magnitude < 0.01f) return;
        if(rb.angularVelocity > 0){
            rb.angularVelocity -= deceleration;
        }else {
            rb.angularVelocity += deceleration;
        }
    }

    void ClampAngularVelocity(){
        if(rb.angularVelocity > currAngularVelocity){
            rb.angularVelocity = currAngularVelocity;
            return;
        }
        if(rb.angularVelocity < -currAngularVelocity){
            rb.angularVelocity = -currAngularVelocity;
        }
    }

    void UpdateGrounded(){
        RaycastHit2D hit = Physics2D.Raycast(transform.position, -Vector2.up, wheelRadius + 0.03f, robotLayerMask);

        if (hit.collider != null) {
            Debug.DrawLine(transform.position, new Vector2(transform.position.x, transform.position.y - (wheelRadius + 0.03f)), Color.red);
            grounded = true;
        }else{
            Debug.DrawLine(transform.position, new Vector2(transform.position.x, transform.position.y - (wheelRadius + 0.03f)), Color.green);
            grounded = false;
        }
    }

    //0 - left, 1 right
    void FlipRobot(float dir){
        if(transform.parent.localScale.x.Equals(dir)){
            return;
        }

        transform.parent.localScale = new Vector3(dir, transform.parent.localScale.y, transform.parent.localScale.z);

        transform.parent.position = new Vector3(transform.parent.position.x - dir*2*transform.localPosition.x, transform.parent.position.y, transform.parent.position.z);
    }

}
