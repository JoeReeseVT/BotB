using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hitbox : MonoBehaviour
{
    private bool isActive;
    private float screenShakeOnHit;
    private float damageOnHit;

    void Update()
    {
        //

    }

    private void OnTriggerEnter(Collider other)
    {
        if (isActive) {
            // Keep the hit from colliding with the parent's own hurtboxes
            if (other.transform.root == gameObject.transform.root)
                return;

            other.transform.root.gameObject.GetComponentInParent<PlayerController>().takeDamage(damageOnHit);
            if (screenShakeOnHit > 0) {
                CameraFollow cameraScript = GameObject.Find("Main Camera").GetComponent(typeof(CameraFollow)) as CameraFollow;
                cameraScript.addCameraShake(screenShakeOnHit);
            }
            DeactivateHitbox();
        }
    }

    public void ActivateHitbox(float damage, float screenShake) {
        damageOnHit = damage;
        screenShakeOnHit = screenShake;
        isActive = true;
    }

    public void DeactivateHitbox() {
        isActive = false;
    }

}
