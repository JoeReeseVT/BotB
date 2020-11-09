using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hurtbox : MonoBehaviour
{

    public float hitDelay;
    private float lastHitTime = 0;

    void Update()
    {
    }

    private void OnTriggerEnter(Collider other)
    {

        // Keep the hurtbox from colliding with the parent's own hitboxes
        if (other.transform.root == gameObject.transform.root)
            return;

        // Prevent another hit if 'hitDelay' seconds has not elapsed
        if (Time.deltaTime - lastHitTime > hitDelay)
        {
            return;
        }

        // On hit, reset the hit timer
        lastHitTime = Time.deltaTime;
        // STUB for testing collision
        Debug.Log("Collision detected by Hurtbox");
    }
}
