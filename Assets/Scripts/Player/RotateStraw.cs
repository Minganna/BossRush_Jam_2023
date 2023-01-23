using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class RotateStraw : MonoBehaviour
{
    [SerializeField]
    Player_Actions playerActions;
    // reference to the action used to look the player
    private InputAction lookMouse;
    private InputAction lookGamePad;
    public bool isPlayerMoving = false;

    // boolean set by the class CheckForControllerConnected responsible for checking gamepads connection
    public bool isDeviceConnected;

    private void Awake()
    {
        playerActions = new Player_Actions();
    }

    private void OnEnable()
    {
        lookMouse = playerActions.Player.Look;
        lookMouse.Enable();
        lookGamePad = playerActions.Player.Move;
        lookGamePad.Enable();
    }

    private void OnDisable()
    {
        lookMouse.Disable();
        lookGamePad.Disable();
    }


    // Update is called once per frame
    void Update()
    {
        //Get the Screen positions of the object
        Vector2 positionOnScreen = Camera.main.WorldToViewportPoint(transform.position);
        float angle=0.0f;
        if (isDeviceConnected==false)
        {
            Vector2 lookValue = lookMouse.ReadValue<Vector2>();
            //Get the Screen position of the mouse
            Vector2 mouseOnScreen = (Vector2)Camera.main.ScreenToViewportPoint(lookValue);
            //float used to get the angle between the points
            angle = getTheAngle(positionOnScreen, mouseOnScreen);
            angle = refineAngle(positionOnScreen, mouseOnScreen, angle);
        }
        else
        {
            Vector2 lookValue = lookGamePad.ReadValue<Vector2>();
            if (lookValue.x ==0 && lookValue.y==0)
            {
                angle = 0;
            }
            else
            {
                angle = getJoypadAngle(lookValue);
                angle = refineAngle(angle);
            }
            
        }   
        transform.localRotation = Quaternion.Euler(new Vector3(0f, 0f, angle));
    }

    //function used to constrain the angle to 0 45 and 90 degres depending on the angle
    private float refineAngle(Vector2 positionOnScreen, Vector2 mouseOnScreen, float angle)
    {
        if(mouseOnScreen.x - positionOnScreen.x<0.1f)
        {
            if (angle >= 0 && angle < 45 || angle < 0)
            {
                angle = 0;
            }
            else if (angle >= 45 && angle < 85 && (mouseOnScreen.y - positionOnScreen.y < 0.4))
            {
                angle = 45;
            }
            else if (angle >= 85 || (mouseOnScreen.y - positionOnScreen.y >= 0.4))
            {
                if(!isPlayerMoving)
                {
                    angle = 90;
                }
                else
                {
                    angle = 45;
                }
                
            }
        }
        else
        {
            if (mouseOnScreen.y - positionOnScreen.y <= 0.2)
            {
                angle = 0;
            }
            else if (mouseOnScreen.y - positionOnScreen.y < 0.4)
            {
                angle = 45;
            }
            else if (mouseOnScreen.y - positionOnScreen.y >= 0.4)
            {
                if (!isPlayerMoving)
                {
                    angle = 90;
                }
                else
                {
                    angle = 45;
                }
            }
        }
        return angle;
    }


    //function used to constrain the angle to 0 45 and 90 degres depending on the angle
    private float refineAngle(float angle)
    {
        if (angle >= 0 && angle < 45 || angle < 0)
        {
            angle = 0;
        }
        else if (angle >= 45 && angle < 85 )
        {
            angle = 45;
        }
        else if (angle >= 85)
        {
            angle = 90;
        }

        return angle;
    }

    private float getTheAngle(Vector2 positionOnScreen, Vector2 mouseOnScreen)
    {
        float angle;
        //to get the correct rotation the code should check if the mouse is in front or behind the player
        if (positionOnScreen.x - mouseOnScreen.x < 0.0f)
        {
            angle = AngleBetweenTwoPoints(mouseOnScreen, positionOnScreen);
        }
        else
        {
            //angle = AngleBetweenTwoPoints(mouseOnScreen, positionOnScreen);
            angle = -AngleBetweenTwoPoints(positionOnScreen, mouseOnScreen);
        }

        return angle;
    }

    float getJoypadAngle(Vector2 lookValue)
    {
        if (transform.parent.localScale.x < 0)
        {
            return Mathf.Atan2(lookValue.y, -lookValue.x) * Mathf.Rad2Deg;
        }
        else
        {
            return Mathf.Atan2(lookValue.y, lookValue.x) * Mathf.Rad2Deg;
        }
        
    }

    float AngleBetweenTwoPoints(Vector3 a, Vector3 b)
    {
        return Mathf.Atan2(a.y - b.y, a.x - b.x) * Mathf.Rad2Deg;
    }
}

