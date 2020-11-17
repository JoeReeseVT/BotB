using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hurtbox : MonoBehaviour
{

    public float hitDelay;
    private float lastHitTime = 0;
    private bool needToUpdateHurtboxes = false;

    void Update()
    {
        // If timer has elapsed, re-enable all the hurtboxes (only happens once per timer elapsed)
        if (Time.time - lastHitTime < hitDelay)
        {
            needToUpdateHurtboxes = true;
            return;
        } 
        else if (needToUpdateHurtboxes)
        {
            needToUpdateHurtboxes = false;
            foreach (Collider c in gameObject.GetComponentInParent<PlayerCollision>().hurtboxesArr)
            {
                c.enabled = true;
            }
        }
        else 
        { ; } // Do nothing normally, to save on processing
        
        // Re-enable the hurtboxes once time has elapsed

    }

    private void OnTriggerEnter(Collider other)
    {

        // Keep the hurtbox from colliding with the parent's own hitboxes
        if (other.transform.root == gameObject.transform.root)
            return;

        // On hit, reset the hit timer
        lastHitTime = Time.time;
        // STUB for testing collision
        Debug.Log("Collision detected by Hurtbox");

        // Apply damage in response to being hit
        gameObject.GetComponentInParent<PlayerController>().takeDamage(10);
        

        // Disable the hurtboxes and don't re-enable them until timer has elapsed in Update()
        needToUpdateHurtboxes = false;
        foreach (Collider c in gameObject.GetComponentInParent<PlayerCollision>().hurtboxesArr)
        {
            c.enabled = false;
        }
    }
}
