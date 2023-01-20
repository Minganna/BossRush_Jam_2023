using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(Rigidbody2D)), 
 RequireComponent(typeof(CharacterController2D)),
 RequireComponent(typeof(BoxCollider2D)), 
 RequireComponent(typeof(CircleCollider2D))]
public class PlayerMovements : MonoBehaviour
{
    // reference to the Character controller class,
    private CharacterController2D controller;
    //reference to the rigidbody class
    private Rigidbody2D rigidBody;
    // true on character on flying vehicle
    private bool isFlyingOnVehicle = false;
    private bool isOnVehicle = false;
    // float that keeps track of the input read from the player inputs
    private float horizontalMove;
    //if on flying machine move up and down
    private float verticalMove;
    // boolean used to make the character jump if required
    private bool jump=false;
    // boolean used to make the character crouch
    private bool crouch = false;
    //float used to determine the character speed
    [SerializeField]
    float runSpeed = 40.0f;

    // Start is called before the first frame update
    void Start()
    {
        controller = this.GetComponent<CharacterController2D>();
        rigidBody = this.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        getPlayerInputs();
    }

    private void FixedUpdate()
    {
        if (isOnVehicle)
        {
            if (isFlyingOnVehicle)
            {
                controller.Move(horizontalMove, verticalMove);
            }
            else
            {
                controller.Move(horizontalMove, false, false);
            }
        }
        else
        {
            //Move the character
            controller.Move(horizontalMove, crouch, jump);
            Debug.Log("Is Jumping");
            // stop Jumping
            jump = false;
        }
    }

    public void setOnFlyingVehicle(bool vehicle)
    {
        isFlyingOnVehicle = vehicle;
    }

    public void setGravity( float gravity)
    {
        rigidBody.gravityScale = gravity;
    }

    void getPlayerInputs()
    {
        horizontalMove = Input.GetAxisRaw("Horizontal") * runSpeed * Time.fixedDeltaTime;
        if(isOnVehicle)
        {
            if (isFlyingOnVehicle)
            {
                verticalMove = Input.GetAxisRaw("Vertical") * runSpeed * Time.fixedDeltaTime;
            }

        }
        else 
        {
            if (Input.GetButtonDown("Jump"))
            {
                Debug.Log("In Jump");
                jump = true;
            }
            if(Input.GetButtonDown("Crouch"))
            {
                crouch = true;
            }
            else if(Input.GetButtonUp("Crouch"))
            {
                crouch = false;
            }
        }
    }
}
