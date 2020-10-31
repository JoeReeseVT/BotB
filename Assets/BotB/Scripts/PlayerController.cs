using System.Collections;
using System.Collections.Generic;
using UnityEngine; 
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    private CharacterController controller;
    private PlayerInput input;

    private Vector3 movementVector3;

    private float playerSpeed = 1.8f;

    // Start is called before the first frame update
    void Start()
    {
        controller = gameObject.GetComponent(typeof(CharacterController)) as CharacterController;
        input = gameObject.GetComponent(typeof(PlayerInput)) as PlayerInput;

    }
    public void OnEnable()
    {

    }

    // Update is called once per frame
    void Update()
    {
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
