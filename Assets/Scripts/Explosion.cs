using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour
{
    public int direction; // Either 1 (right), or -1 (left)
    public float speed;
    public float leftLimit;
    public float rightLimit;

    void Update()
    {
        transform.position += transform.right*direction * Time.deltaTime * speed;
        bool outOfScreen = direction > 0 ? transform.position.x > rightLimit : transform.position.x < leftLimit;
        if (outOfScreen){
            Destroy(gameObject, .1f);
        }
    }

    public void EndExplosion() {
        Destroy(gameObject);
    }
}
