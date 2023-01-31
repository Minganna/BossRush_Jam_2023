using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    Camera cam;
    Transform camTransf;

    float tiltAngle=7.36f;
    float smooth = 1.0f;
    float camX;

    private void Awake()
    {
        instance = this;

    }

    private void OnEnable()
    {
        cam= Camera.main;
        if(cam)
        {
            camTransf = cam.GetComponent<Transform>(); 

        }
        
    }
    public void LoadScene(int scene)
    {
        SceneManager.LoadScene(scene);
    }

    public void isPlayerMoving(float movement)
    {
        if(movement > 0)
        {
            tiltAngle = 7.36f;
        }
        if(movement < 0)
        {
            tiltAngle = -7.57f;
        }
        float tiltY= movement * tiltAngle;
        Quaternion targetRot = Quaternion.Euler(9.77f, tiltAngle, 0.0f);
        camTransf.rotation = Quaternion.Slerp(camTransf.rotation, targetRot, Time.deltaTime * smooth);

    }
}
