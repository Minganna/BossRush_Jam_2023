using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateStraw : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //Get the Screen positions of the object
        Vector2 positionOnScreen = Camera.main.WorldToViewportPoint(transform.position);
        new Vector3(Input.GetAxisRaw("RightHoriz"), 0, Input.GetAxisRaw("RightVert"));
        Debug.Log(new Vector2(Input.GetAxisRaw("RightHoriz"), Input.GetAxisRaw("RightVert")));
        //Get the Screen position of the mouse
        Vector2 mouseOnScreen = (Vector2)Camera.main.ScreenToViewportPoint(Input.mousePosition);
        //float used to get the angle between the points
        float angle = getTheAngle(positionOnScreen, mouseOnScreen);
        angle = refineAngle(positionOnScreen, mouseOnScreen, angle);

        transform.localRotation = Quaternion.Euler(new Vector3(0f, 0f, angle));
    }

    //function used to constrain the angle to 0 45 and 90 degres depending on the angle
    private static float refineAngle(Vector2 positionOnScreen, Vector2 mouseOnScreen, float angle)
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

    float AngleBetweenTwoPoints(Vector3 a, Vector3 b)
    {
        return Mathf.Atan2(a.y - b.y, a.x - b.x) * Mathf.Rad2Deg;
    }
}

