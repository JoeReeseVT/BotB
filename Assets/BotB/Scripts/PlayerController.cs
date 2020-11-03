using System.Collections;
using System.Collections.Generic;
using UnityEngine; 
using UnityEngine.InputSystem;
using static Notes;

public class PlayerController : MonoBehaviour
{
    public GameObject otherPlayer;
    private CharacterController controller;
    private PlayerInput input;
    private Conductor conductor;

    private Vector3 movementVector3;

    private float playerSpeed = 1.8f;
    private float dashDistance = 0.8f;
    private float turnSpeed = 6.0f;

    //time, in seconds, that you can be off from a note and have it still count
    private float timingForgiveness = 0.11f;
    
    // Start is called before the first frame update
    void Start()
    {
        controller = gameObject.GetComponent(typeof(CharacterController)) as CharacterController;
        input = gameObject.GetComponent(typeof(PlayerInput)) as PlayerInput;
        conductor = GameObject.Find("Conductor").GetComponent(typeof(Conductor)) as Conductor;
    }
    public void OnEnable()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.rotation = Quaternion.Slerp(transform.rotation, 
                             Quaternion.LookRotation(otherPlayer.transform.position - transform.position, Vector3.up), 
                             Time.deltaTime*turnSpeed);

        //transform.LookAt(otherPlayer.transform, Vector3.up);
        controller.Move(movementVector3 * Time.deltaTime * playerSpeed);
    }

    public void OnMove(InputValue value)
    {
        var movementVector = value.Get<Vector2>();
        movementVector3 = new Vector3(movementVector[0]*-1, 0, movementVector[1]*-1);

        //init animation related movement stuff.
    }

    public void OnKick()
    {
        Debug.Log("Kick!");

        //placeholder for dash. don't let this ship!
        if (Mathf.Abs(conductor.nearestNoteDist((int)Notes.DrumKick)) <= timingForgiveness)
        {
            controller.Move(Vector3.Normalize(movementVector3) * dashDistance);
        }
        else {
            Debug.Log("Player tried to dash but was " + conductor.nearestNoteDist((int)Notes.DrumKick) + " seconds off.");
        }

    }

    public void OnSnare()
    {
        Debug.Log("Snare!");
    }

    public void OnCrash()
    {
        Debug.Log("Crash!");
    }

    public void OnHat()
    {
        Debug.Log("Hat!");
    }

}
