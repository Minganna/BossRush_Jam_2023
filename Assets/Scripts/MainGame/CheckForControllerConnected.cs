using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CheckForControllerConnected : MonoBehaviour
{
    //reference to the straw class
    RotateStraw straw;

    private void Awake()
    {
        straw = FindObjectOfType<RotateStraw>();
    }
    // Start is called before the first frame update
    void Start()
    {
        if (Gamepad.all.Count > 0)
        {
            var gamePad = Gamepad.current;
            straw.isDeviceConnected = gamePad.enabled;
            Debug.Log(gamePad.name);
        }
        else
        {
            straw.isDeviceConnected = false;
        }
        Debug.Log("device connected: " + straw.isDeviceConnected);
        InputSystem.onDeviceChange +=
        (device, change) =>
        {
            switch (change)
            {
                case InputDeviceChange.Added:
                    straw.isDeviceConnected = true;
                    Debug.Log("New device added: " + device + ":"+ straw.isDeviceConnected);
                    break;

                case InputDeviceChange.Removed:
                    straw.isDeviceConnected = false;
                    Debug.Log("Device removed: " + device + ":" + straw.isDeviceConnected);
                    break;
            }
        };
    }
}
