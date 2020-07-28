using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RobotMovement : MonoBehaviour
{
    Rigidbody2D rb;
    public float speed;
    public float deceleration;
    public float jumpForce;

    bool grounded;

    void Start(){
        rb = GetComponent<Rigidbody2D>();
    }

  
    void Update()
    {

        if(Input.GetKeyDown(KeyCode.Space) && grounded){
            rb.AddForce(new Vector2(0f, jumpForce), ForceMode2D.Impulse);
        }

        if(Input.GetKey(KeyCode.A)){
            if(rb.velocity.x > 0){
                rb.angularVelocity = 2 * speed;
            }else {
                rb.angularVelocity = speed;
            }
            return;
        }

        if(Input.GetKey(KeyCode.D)){
            if(rb.velocity.x < 0){
                rb.angularVelocity = -2 * speed;
            }else {
                rb.angularVelocity = -speed;
            }
            return;
        }

        Decelerate();

    }

    void Decelerate(){
        if(rb.angularVelocity > 0){
            rb.angularVelocity -= deceleration;
        }else {
            rb.angularVelocity += deceleration;
        }
    }

    void OnCollisionEnter2D(Collision2D other){
        grounded = true;
    }

    void OnCollisionExit2D(Collision2D other){
        grounded = false;
    }

}
