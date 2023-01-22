using UnityEngine;
using UnityEngine.InputSystem;

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
    //reference to the new input system action
    [SerializeField]
    Player_Actions playerActions;
    // reference to the action used to move the player
    private InputAction move;
    // reference to the action used to jump
    private InputAction jump;
    // reference to the action used to crouch
    private InputAction crouch;
    // true on character on flying/ground vehicle
    private bool isFlyingOnVehicle = false;
    private bool isOnVehicle = false;
    // float that keeps track of the input read from the player inputs
    private float horizontalMove;
    //if on flying machine move up and down
    private float verticalMove;
    // boolean used to make the character jump if required
    private bool isJumping=false;
    // boolean used to make the character crouch
    private bool isCrouching = false;
    //boolean set by rotate straw, used to confirm if player is looking up
    public bool lookingUp = false;
    //float used to determine the character speed
    [SerializeField]
    float runSpeed = 40.0f;

    private void Awake()
    {
        playerActions = new Player_Actions();
    }

    private void OnEnable()
    {
        move = playerActions.Player.Move;
        move.Enable();

        jump = playerActions.Player.Jump;
        jump.Enable();
        jump.performed += playerJump;

        crouch = playerActions.Player.Crouch;
        crouch.Enable();
        crouch.performed += playerCrouchDown;
        crouch.canceled += playerCrouchUp;
    }

    private void OnDisable()
    {
        move.Disable();
        jump.Disable();
        crouch.Disable();
    }

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
            //checks if player is not looking up
            Debug.Log(verticalMove);
            if(!lookingUp)
            {
                //Move the character
                controller.Move(horizontalMove, isCrouching, isJumping);
                // stop Jumping
                isJumping = false;
            }
            else
            {
                //Move the character
                controller.Move(0, isCrouching, isJumping);
                // stop Jumping
                isJumping = false;
            }

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
        Vector2 playerMove = move.ReadValue<Vector2>();
        horizontalMove = playerMove.x * runSpeed * Time.fixedDeltaTime;
        if(isOnVehicle)
        {
            if (isFlyingOnVehicle)
            {
                verticalMove = playerMove.y * runSpeed * Time.fixedDeltaTime;
            }

        }
        else
        {
            verticalMove = playerMove.y;
        }
    }

    private void playerJump(InputAction.CallbackContext context)
    {
        if (!isOnVehicle)
        {
            isJumping = true;
        }
    }

    private void playerCrouchDown(InputAction.CallbackContext context)
    {
        if(!isOnVehicle)
        {
            isCrouching = true;
        }
    }
    private void playerCrouchUp(InputAction.CallbackContext context)
    {
        if (!isOnVehicle)
        {
            isCrouching = false;
        }
    }
}
