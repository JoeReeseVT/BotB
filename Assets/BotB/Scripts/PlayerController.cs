using System.Collections;
using System.Collections.Generic;
using UnityEngine; 
using UnityEngine.InputSystem;
using UnityEngine.UI;
using static Notes;

public class PlayerController : MonoBehaviour
{
    public GameObject otherPlayer;
    public Slider healthBar;

    private CharacterController controller;
    private PlayerInput input;
    private Conductor conductor;
    private Vector3 movementVector3;
    private CameraFollow cameraScript;
    private Animator animStateMachine;
    private Animator gameplayStateMachine;


    private float playerSpeed = 1.8f;
    private float dashDistance = 0.8f;
    private float turnSpeed = 6.0f;

    private float health = 100f;

    //time, in seconds, that you can be off from a note and have it still count
    private float timingForgiveness = 0.15f;
    
    // Start is called before the first frame update
    void Start()
    {
        controller = gameObject.GetComponent(typeof(CharacterController)) as CharacterController;
        input = gameObject.GetComponent(typeof(PlayerInput)) as PlayerInput;
        conductor = GameObject.Find("Conductor").GetComponent(typeof(Conductor)) as Conductor;
        cameraScript = GameObject.Find("Main Camera").GetComponent(typeof(CameraFollow)) as CameraFollow;
        gameplayStateMachine = gameObject.GetComponent(typeof(Animator)) as Animator;
        //Assumes our mesh is our first or only child.
        animStateMachine = transform.GetChild(0).GetComponent(typeof(Animator)) as Animator;



        healthBar.maxValue = health;
        healthBar.value = health;

    }
    public void OnEnable()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (health <= 0) {
            return;
        }

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
        gameplayStateMachine.SetTrigger("KickInput");

        Debug.Log("Kick!");

        /*
        //placeholder for dash. don't let this ship!

        if (health <= 0)
        {
            return;
        }

        if (Mathf.Abs(conductor.nearestNoteDist((int)Notes.DrumKick)) <= timingForgiveness)
        {
            controller.Move(Vector3.Normalize(movementVector3) * dashDistance);
        }
        else {
            Debug.Log("Player tried to dash but was " + conductor.nearestNoteDist((int)Notes.DrumKick) + " seconds off.");
        }
        */

    }

    public void OnSnare()
    {
        gameplayStateMachine.SetTrigger("SnareInput");

        /*
        if (health <= 0)
        {
            return;
        }
        //placeholder for attack. don't let this ship!
        if ( Mathf.Abs(conductor.nearestNoteDist((int)Notes.DrumSnare)) <= timingForgiveness && Vector3.Magnitude(transform.position - otherPlayer.transform.position) < 2) {
            otherPlayer.GetComponent<PlayerController>().takeDamage(4.0f);

            cameraScript.addCameraShake(.6f);
        }
        */
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

    public void takeDamage(float damage) {
        health = health - damage;
        healthBar.value = health;
    }

    public float getHealth() {
        return health;
    }

    //Returns gameplay state machine if false, animation state machine if true
    public Animator getStateMachine(bool b) {
        if (b)
        {
            return animStateMachine;
        }
        else {
            return gameplayStateMachine;
        }
    }

    //spits out movement axes relative to player rotation, as opposed to world
    public float getMovementForward() {
        return Vector3.Dot(transform.forward, movementVector3);
    }

    public float getMovementRight()
    {
        return Vector3.Dot(transform.right, movementVector3);
    }

}
