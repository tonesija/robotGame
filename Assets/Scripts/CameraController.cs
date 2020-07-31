using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    Transform toFollow;
    Rigidbody2D rbToFollow;
    Camera thisCamera;

    bool shake;
    float maxShakeDuration;
    float shakeStrength;
    float shakeDuration;

    void Start()
    {
        toFollow = GameObject.FindGameObjectWithTag("Player").transform;
        rbToFollow = toFollow.GetComponent<Rigidbody2D>();
        thisCamera = GetComponent<Camera>();
    }

    void Update()
    {
        transform.position = new Vector3(toFollow.position.x, toFollow.position.y, transform.position.z);

        float velocity = Mathf.Pow(Mathf.Abs(rbToFollow.velocity.x), 1.5f);
        float targetSize = 2.5f + velocity/15f;
        thisCamera.orthographicSize = Mathf.Lerp(thisCamera.orthographicSize, targetSize, 1f * Time.deltaTime);
    }

    IEnumerator ShakeCorutine(float shakeStrength, float duration){
        float shakeDuration = 0f;
        while(shakeDuration < duration){
            transform.position += (Vector3)new Vector2(shakeStrength * Random.Range(0f, duration), shakeStrength * Random.Range(0f, duration));
            shakeDuration += Time.deltaTime;
            yield return null;
        }
        
    }

    public void Shake(float shakeStrength, float duration){
        IEnumerator corutine = ShakeCorutine(shakeStrength, duration);
        StartCoroutine(corutine);
    }
}
