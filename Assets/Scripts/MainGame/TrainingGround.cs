using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;

public class TrainingGround : MonoBehaviour
{
    // reference to the gameManager
    GameManager gm;
    int TrainingLevel=0;
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
    // reference to the action used to jump
    private InputAction fire;
    // reference to the action used to Exit
    private InputAction exit;
    [SerializeField]
    TextMeshProUGUI TrainingText;
    bool playerJumped=false;
    bool playerCrouched=false;
    bool isFiring=false;
    bool isAiming=false;
    bool needDamage=true;
    bool localHealing=false;

    [SerializeField]
    playerHealth health;
    [SerializeField]
    PlayerMovements movements;

    
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

        aim = playerActions.Player.Aim;
        aim.Enable();
        aim.performed += isAimingOn;
        aim.canceled += isAimingOff;

        heal = playerActions.Player.Heal;
        heal.Enable();
        heal.performed += isHealingOn;
        heal.canceled += isHealingOff;
        fire = playerActions.Player.Fire;
        fire.Enable();
        fire.performed += fireBullet;
        fire.canceled += stopFireBullet;

        exit = playerActions.Player.ReturnToMainMenu;
        exit.Enable();
        exit.performed += isEscPressed;
    }
    
    private void Start() {
        gm=GameManager.instance;
    }

    private void Update() {
        Vector2 playerMove = move.ReadValue<Vector2>();
        if(TrainingLevel==0 &&playerMove.x!=0)
        {
            TrainingLevel++;
            if(TrainingText)
            {
                TrainingText.text="Space/ up Arrow/ W / Button South (GamePad) to Jump";
            }
        }
        if(TrainingLevel==1 &&playerJumped)
        {
            TrainingLevel++;
            if(TrainingText)
            {
                TrainingText.text="S/ down Arrow/ W / Button West (GamePad) to Crouch";
            }
        }
        if(TrainingLevel==2 &&playerCrouched)
        {
            TrainingLevel++;
            if(TrainingText)
            {
                TrainingText.text="While crouching, WD / Arrows / Left GamePad stick to crouch walk";
            }
        }
        if(TrainingLevel==3 && playerCrouched && playerMove.x!=0)
        {
            TrainingLevel++;
            if(TrainingText)
            {
                TrainingText.text="Release the button to stop crouching";
            }
        }
        if(TrainingLevel==4 && !playerCrouched)
        {
            TrainingLevel++;
            if(TrainingText)
            {
                TrainingText.text="Hold Down Left mouse button/ Button East (Gamepad) to shoot, Mouse position/ Left GamePad stick to aim your straw";
            }
        }
        if(TrainingLevel==5 && isFiring)
        {
            TrainingLevel++;
            if(TrainingText)
            {
                TrainingText.text="Release Left mouse Button/ Button East (Gamepad) to stop shooting";
            }
        }
        if(TrainingLevel==6 && !isFiring)
        {
            TrainingLevel++;
            if(TrainingText)
            {
                TrainingText.text="Hold Down the Middle Bouse button/ Right Shoulder (GamePad) to aim better ";
            }
        }
        if(TrainingLevel==7 && isAiming)
        {
            TrainingLevel++;
            if(TrainingText)
            {
                TrainingText.text="Release  the Middle Bouse button/ Right Shoulder (GamePad) to stop aiming ";
            }
        }
        if(TrainingLevel==8 && !isAiming)
        {
            TrainingLevel++;
            if(needDamage)
            {
                needDamage=false;
                if(health)
                {
                    health.Damage(1);
                }
            }
            if(TrainingText)
            {
                TrainingText.text="When damaged, Hold down the right mouse button/ E/ Button North (GamePad) until the animation ends to heal";
            }
        }
        if(TrainingLevel==9 && localHealing)
        {
            if(movements)
            {
                if(!movements.getIsHealing())
                {
                    TrainingLevel++;
                    if(TrainingText)
                    {
                        TrainingText.text="You are now ready! ";
                        StartCoroutine(startGame());
                    }
                }

            }
            else
            {
                movements=(PlayerMovements)FindObjectOfType(typeof(PlayerMovements));
            }

        }


    }

    private void isAimingOn(InputAction.CallbackContext context)
    {
        if(TrainingLevel>=6)
        {
            isAiming = true;
        }

    }
    private void isAimingOff(InputAction.CallbackContext context)
    {
        if(TrainingLevel>=7)
        {
            isAiming = false;
        }
    }

    private void isHealingOn(InputAction.CallbackContext context)
    {
        if(TrainingLevel==9)
        {
            localHealing=true;
        }
    }
    private void isHealingOff(InputAction.CallbackContext context)
    {

    }

    private void playerJump(InputAction.CallbackContext context)
    {
        if(TrainingLevel==1)
        {
            playerJumped=true;
        }
    }

    private void playerCrouchDown(InputAction.CallbackContext context)
    {
        if(TrainingLevel>=2)
        {
            playerCrouched=true;
        }
    }
    private void playerCrouchUp(InputAction.CallbackContext context)
    {
        if(TrainingLevel>=3)
        {
            playerCrouched=false;
        }
    }

    void fireBullet(InputAction.CallbackContext context)
    {
        if(TrainingLevel>=4)
        {
            isFiring = true;
        }   
    }
    void stopFireBullet(InputAction.CallbackContext context)
    {
        if(TrainingLevel>=5)
        {
            isFiring = false;
        }
    }

    IEnumerator startGame()
    {
        yield return new WaitForSeconds(1.0f);
        gm.resetValues();
        gm.LoadScene(3);
    }

    void isEscPressed(InputAction.CallbackContext context)
    {
        gm.exitScene(true);
    }
}
