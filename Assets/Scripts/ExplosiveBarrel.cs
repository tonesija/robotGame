using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosiveBarrel : MonoBehaviour
{

    public float forceTreshold;

    public float explosionRadius;
    
    public float explosionPower;

    CameraController mainCamera;

    void Start() {
        mainCamera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>().GetComponent<CameraController>();
    }

    private void OnCollisionEnter2D(Collision2D other) {
        Rigidbody2D otherrb = other.transform.GetComponent<Rigidbody2D>();
        if(otherrb == null){
            return;
        }

        float forceApplied = other.relativeVelocity.magnitude * otherrb.mass;

        if(forceApplied > forceTreshold){
            Explode();
            Destroy(gameObject);
        }
    }

    

    void Explode(){
        mainCamera.Shake(0.5f, 0.5f);
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, explosionRadius);
        List<ExplosiveBarrel> toExplode = new List<ExplosiveBarrel>();

        foreach(Collider2D collider in colliders){
            Rigidbody2D rb = collider.transform.GetComponent<Rigidbody2D>();
            
            if(rb != null){
                if(rb.transform == transform) continue;
                if(rb.transform.GetComponent<ExplosiveBarrel>() != null) toExplode.Add(rb.transform.GetComponent<ExplosiveBarrel>());

                Vector2 forceDir = -transform.position + (Vector3)rb.position;
                float power = (explosionRadius - forceDir.magnitude) * explosionPower;
                forceDir = forceDir.normalized;
                if(rb.mass < 0.05f){
                    rb.velocity += forceDir * power * 15f;
                }else {
                    rb.AddForce(forceDir * power, ForceMode2D.Impulse);
                }
            }
        }

        foreach(ExplosiveBarrel explosiveBarrel in toExplode){
            explosiveBarrel.ExplodeWithoutChain();
        }
    }

    void ExplodeWithoutChain(){
        mainCamera.Shake(0.5f, 0.5f);
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, explosionRadius);
        foreach(Collider2D collider in colliders){
            Rigidbody2D rb = collider.transform.GetComponent<Rigidbody2D>();
            
            if(rb != null){
                if(rb.transform == transform) continue;

                Vector2 forceDir = -transform.position + (Vector3)rb.position;
                float power = (explosionRadius - forceDir.magnitude) * explosionPower;
                forceDir = forceDir.normalized;
                if(rb.mass < 0.05f){
                    rb.velocity += forceDir * power * 15f;
                }else {
                    rb.AddForce(forceDir * power, ForceMode2D.Impulse);
                }
            }
        }
        Destroy(gameObject);
    }

}
