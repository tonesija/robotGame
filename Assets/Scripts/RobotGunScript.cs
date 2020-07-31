using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RobotGunScript : MonoBehaviour
{   
    public GameObject projectile;

    public float shootForce;
    Rigidbody2D rb;

    Transform robotTransform;

    Vector2 bulletSpawn;

    float weaponWidth;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        robotTransform = transform.parent;
        weaponWidth = GetComponent<BoxCollider2D>().bounds.size.x;
        bulletSpawn = new Vector2(weaponWidth + 0.01f, 0);
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.LeftControl)){
            FireProjectile();
        }
    }

    void FireProjectile(){
        GameObject proj = null;
        Rigidbody2D projRB = null;

        if(robotTransform.localScale.x < 0){
            proj = Instantiate(projectile, transform.position - (Vector3)bulletSpawn, Quaternion.identity);
            projRB = proj.GetComponent<Rigidbody2D>();
            projRB.AddForce(new Vector2(-shootForce, 0), ForceMode2D.Impulse);
            rb.velocity += new Vector2(shootForce * 4f,0);
        } else{
            proj = Instantiate(projectile, transform.position + (Vector3)bulletSpawn, Quaternion.identity);
            projRB = proj.GetComponent<Rigidbody2D>();
            projRB.AddForce(new Vector2(shootForce, 0), ForceMode2D.Impulse);
            rb.velocity += new Vector2(-shootForce * 4f,0);
        }
        
    }

    void OnJointBreak2D(Joint2D brokenJoint) {
        this.transform.parent = null;
        Destroy(this);
    }
}
