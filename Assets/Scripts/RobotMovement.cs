using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RobotMovement : MonoBehaviour
{
    Rigidbody2D rb;
    public float speed;
    public float deceleration;
    public float jumpForce;

    int robotLayerMask = 1 << 8; //mask for layer 8 (layer 8 = Robot part layer)

    bool grounded; //is robot on the ground, can robot jump?

    float wheelRadius;

    void Start(){
        rb = GetComponent<Rigidbody2D>();
        robotLayerMask = ~robotLayerMask; //everything except layer 8
        wheelRadius = GetComponent<CircleCollider2D>().bounds.size.y / 2;
    }

  
    void Update()
    {

        UpdateGrounded();

        //jump
        if(Input.GetKeyDown(KeyCode.Space) && grounded){
            print("jump");
            rb.AddForce(new Vector2(0f, jumpForce), ForceMode2D.Impulse);
            return;
        }

        if(Input.GetKeyDown(KeyCode.LeftShift)){
            speed *= 2;
        }

        if(Input.GetKeyUp(KeyCode.LeftShift)){
            speed /= 2;
        }

        //left
        if(Input.GetKey(KeyCode.A)){
            if(rb.velocity.x > 0){
                rb.angularVelocity = 2 * speed;
            }else {
                rb.angularVelocity = speed;
            }
            return;
        }

        //right
        if(Input.GetKey(KeyCode.D)){
            if(rb.velocity.x < 0){
                rb.angularVelocity = -2 * speed;
            }else {
                rb.angularVelocity = -speed;
            }
            return;
        }

        //handbreak
        if(Input.GetKey(KeyCode.C) && grounded){
            rb.angularVelocity = 0;
            rb.velocity = new Vector2(0f, 0f);
            return;
        }

        Decelerate();
    }

    void Decelerate(){
        if((Mathf.Abs(rb.angularVelocity) < 1f || rb.velocity.magnitude < 0.1f) && grounded){
            rb.angularVelocity = 0;
            rb.velocity = new Vector2(0, 0);
            return;
        }
        if(rb.angularVelocity > 0){
            rb.angularVelocity -= deceleration;
        }else {
            rb.angularVelocity += deceleration;
        }
    }

    void UpdateGrounded(){
        RaycastHit2D hit = Physics2D.Raycast(transform.position, -Vector2.up, wheelRadius + 0.01f, robotLayerMask);
        //print(hit.collider.gameObject);

        

        if (hit.collider != null) {
            Debug.DrawLine(transform.position, new Vector2(transform.position.x, transform.position.y - (wheelRadius + 0.01f)), Color.red);
            grounded = true;
        }else{
            Debug.DrawLine(transform.position, new Vector2(transform.position.x, transform.position.y - (wheelRadius + 0.01f)), Color.green);
            grounded = false;
        }
    }

}
