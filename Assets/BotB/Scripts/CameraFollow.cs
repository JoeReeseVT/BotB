using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform characterA;
    public Transform characterB;
    public float cameraTrackSpeed = 1;
    public float cameraLookSpeed = 1;
    
    //distance between the two targets at which the camera should be fully panned out/is set up in the scene
    public float pannedOutDistance = 12;

    //offset for the Y position of the look target, since we may want to frame the fighters slightly below centerframe
    public float lookTargetYOffset = 1;

    //Offset for the camera position when panned in.
    public Vector3 panInOffset = new Vector3(0,0,0);

    public float cameraShakeMultiplier = 1;
    public float cameraShakeDecaySpeed = 1;

    private float cameraShakeAmount = 0;

    private GameObject lookTarget;
    private Vector3 startPosition;

    //not the position of our target, but the position the camera is lerping to.
    //stored so we don't get motion sickness inducing swings from rotation keeping up with the movement
    private Vector3 targetCameraPosition;

    //we store lerped camera pre shake so that the look targeting doesn't go crazy trying to follow screen shake
    private Vector3 positionPreShake;
    
    // Init lookTarget and resolve camera position at start
    void Start()
    {
        startPosition = transform.position;
        lookTarget = new GameObject();
        positionPreShake = FollowCamSolve();
        transform.position = positionPreShake;
    }

// Solve new targets and lerp our position + rotation to frame them
void Update()
    {
        //TODO: if paused, return here

        targetCameraPosition = FollowCamSolve();
        positionPreShake = Vector3.Lerp(positionPreShake, targetCameraPosition, cameraTrackSpeed * Time.deltaTime);
        
        if (cameraShakeAmount > 0.1) {
            transform.position = positionPreShake + (cameraShakeAmount * cameraShakeMultiplier *
                new Vector3(Random.Range(-1, 1), Random.Range(-1, 1), Random.Range(-0.5f, 0.5f)));
            cameraShakeAmount = Mathf.Lerp(cameraShakeAmount, 0, cameraShakeDecaySpeed * Time.deltaTime);
        } else {
            transform.position = positionPreShake;
        }


    //Fast look at (don't need when we're using smooth look)
    //transform.LookAt(lookTarget.transform);

    //Smooth look at https://forum.unity.com/threads/smooth-look-at.26141/
    var targetRotation = Quaternion.LookRotation(lookTarget.transform.position - targetCameraPosition);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, cameraLookSpeed * Time.deltaTime);

    }

    public void addCameraShake(float addAmount) {
        cameraShakeAmount = cameraShakeAmount + addAmount;
    }

    Vector3 FollowCamSolve() {
        float interTargetDistance = Vector3.Magnitude(characterA.position - characterB.position);
        lookTarget.transform.position = Vector3.Lerp(characterA.position, characterB.position, 0.5f) + new Vector3(0,lookTargetYOffset,0);
        //float currentDistance = Vector3.Magnitude(lookTarget.transform.position - positionPreShake);

        return Vector3.Lerp(startPosition, lookTarget.transform.position + panInOffset, Mathf.Lerp(0.7f, 0, interTargetDistance/pannedOutDistance));
    }
}
