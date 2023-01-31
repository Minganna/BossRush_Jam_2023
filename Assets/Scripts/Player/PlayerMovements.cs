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
    //the character way of shooting
    private RotateStraw straw;
    //reference to the new input system action
    [SerializeField]
    Player_Actions playerActions;
    // reference to the action used to move the player
    private InputAction move;
    // reference to the action used to jump
    private InputAction jump;
    // reference to the action used to crouch
    private InputAction crouch;
    // reference to the action used to crouch
    private InputAction aim;
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
    private bool isAiming = false;
    //float used to determine the character speed
    [SerializeField]
    float runSpeed = 40.0f;

    GameManager gm;

    private void Awake()
    {
        playerActions = new Player_Actions();
        
    }

    private void OnEnable()
    {
        //find the straw script (will be used to notify player movements)
        straw = FindObjectOfType<RotateStraw>();
        gm= GameManager.instance;

        move = playerActions.Player.Move;
        move.Enable();

        jump = playerActions.Player.Jump;
        jump.Enable();
        jump.performed += playerJump;

        crouch = playerActions.Player.Crouch;
        crouch.Enable();
        crouch.performed += playerCrouchDown;
        crouch.canceled += playerCrouchUp;

        aim = playerActions.Player.Aim;
        aim.Enable();
        aim.performed += isAimingOn;
        aim.canceled += isAimingOff;

    }

    private void OnDisable()
    {
        move.Disable();
        jump.Disable();
        crouch.Disable();
        aim.Disable();
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
            //Move the character
            controller.Move(horizontalMove, isCrouching, isJumping);
            // stop Jumping
            isJumping = false;
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
        //check if the player is aiming
        if(!isAiming)
        {
            if (playerMove.x < -0.2 || playerMove.x > 0.2)
            {
                if (playerMove.x < 0)
                {
                    horizontalMove = Mathf.Floor(playerMove.x) * runSpeed * Time.fixedDeltaTime;
                }
                else
                {
                    horizontalMove = Mathf.Ceil(playerMove.x) * runSpeed * Time.fixedDeltaTime;
                }
                straw.isPlayerMoving = true;
                gm.isPlayerMoving(playerMove.x);


            }
            else
            {
                horizontalMove = 0;
                straw.isPlayerMoving = false;
            }
        }
        else
        {
            horizontalMove = 0;
            straw.isPlayerMoving = false;
        }
        if (isOnVehicle)
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

    private void isAimingOn(InputAction.CallbackContext context)
    {
        if(!isOnVehicle)
        {
            isAiming = true;
        }
    }
    private void isAimingOff(InputAction.CallbackContext context)
    {
        isAiming = false;
    }
}
