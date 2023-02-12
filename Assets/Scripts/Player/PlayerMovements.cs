using System.Collections;
using System.Collections.Generic;
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
    
    private playerHealth health;
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
    // reference to the action used to crouch
    private InputAction heal;
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
    [SerializeField]
    private Animator playerAnim;
    // reference to the gameManager
    GameManager gm;
    // bool that detemine if the crouching up animation can be performed (nothing on top of the player)
    public bool canStand=true;
    // bool that store that the player stopped pressing the crouch button
    bool requestStanding = false;
    // bool that show if the player is moving and therefore it shouldn't crouch
    public bool crouchingNoWalk =false;
    // bool that keep track if the player is alive
    bool isDeath = false;

    bool isHealing = false;
    // keep a copy of the executing script
    private IEnumerator healCoroutine;



    private void Awake()
    {
        playerActions = new Player_Actions();
    }

    private void OnEnable()
    {
        //find the straw script (will be used to notify player movements)
        straw = FindObjectOfType<RotateStraw>();
        health =this.GetComponent<playerHealth>();

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

        heal = playerActions.Player.Heal;
        heal.Enable();
        heal.performed += isHealingOn;
        heal.canceled += isHealingOff;

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
        controller.setLinkToMovements(this);
        rigidBody = this.GetComponent<Rigidbody2D>();
        gm= GameManager.instance;
    }

    // Update is called once per frame
    void Update()
    {
        if(!isDeath)
        {
            getPlayerInputs();
        }
        
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
            if(requestStanding && canStand)
            {
                requestStanding =false;
                playerAnim.SetBool("isCrouching",false);
                crouchingNoWalk = false;
            }
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
        if(!isAiming && !isHealing && !crouchingNoWalk && !isDeath)
        {
            if (playerMove.x < -0.2 || playerMove.x > 0.2)
            {
                playerAnim.SetBool("isWalking",true);
                if (playerMove.x < 0)
                {
                    horizontalMove = Mathf.Floor(playerMove.x) * runSpeed * Time.fixedDeltaTime;
                }
                else
                {
                    horizontalMove = Mathf.Ceil(playerMove.x) * runSpeed * Time.fixedDeltaTime;
                }
                if(straw)
                {
                    straw.isPlayerMoving = true;
                }
                gm.isPlayerMoving(playerMove.x);


            }
            else
            {
                playerAnim.SetBool("isWalking",false);
                horizontalMove = 0;
                if(straw)
                {
                    straw.isPlayerMoving = false;
                }
            }
        }
        else
        {
            horizontalMove = 0;
            if(straw)
            {
                straw.isPlayerMoving = false;
            }
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
        if (!isOnVehicle && !isDeath &&!isHealing)
        {
            isJumping = true;
            playerAnim.SetBool("isJumping",true);
        }
    }

    private void playerCrouchDown(InputAction.CallbackContext context)
    {
        if(!isOnVehicle && !isDeath && horizontalMove ==0)
        {
            isCrouching = true;
            playerAnim.SetBool("isCrouching",true);
            crouchingNoWalk=true;
        }
    }
    private void playerCrouchUp(InputAction.CallbackContext context)
    {
        if (!isOnVehicle)
        {
            isCrouching = false;
            requestStanding =true;
        }
    }

    private void isAimingOn(InputAction.CallbackContext context)
    {
        playerAnim.SetBool("isWalking",false);
        if(!isOnVehicle)
        {
            isAiming = true;
        }
    }
    private void isAimingOff(InputAction.CallbackContext context)
    {
        isAiming = false;
    }

    private void isHealingOn(InputAction.CallbackContext context)
    {
        if(horizontalMove != 0)
        {
            return;
        }
        isHealing = true;
        healCoroutine = healingTimer();
        StartCoroutine(healCoroutine);

    }
    private void isHealingOff(InputAction.CallbackContext context)
    {
        if(isHealing && healCoroutine!=null)
        {
            StopCoroutine(healCoroutine);
            healCoroutine =null;
            isHealing = false;
        }
    }
    

    private void OnCollisionEnter2D(Collision2D other) 
    {
        playerAnim.SetBool("isJumping",false);
    }

    public void death()
    {
        isDeath=true;
        playerAnim.SetBool("isDead",true);
        if(straw)
        {
            straw.isDeath=true;
        }
    }

    IEnumerator healingTimer()
    {
        Debug.Log("healing");
        yield return new WaitForSeconds(2.0f);
        health.addRepair();
        Debug.Log("healing completed");
        isHealing = false;
    }
}
