using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HurtboxTest : MonoBehaviour
{

    public float hitDelay;
    private float hitTimer = 0;

    void Update()
    {
        // Increment the hit timer each frame
        hitTimer += Time.deltaTime;
    }

    private void OnTriggerEnter(Collider other)
    {

        // Keep the hurtbox from colliding with the parent's own hitboxes
        if (other.transform.root == gameObject.transform.root)
            return;

        // Prevent another hit if 'hitDelay' seconds has not elapsed
        if (hitTimer < hitDelay)
            return;

        // On hit, reset the hit timer
        hitTimer = 0;
        // STUB for testing collision
        Debug.Log("Collision detected by Hurtbox");
    }
}
