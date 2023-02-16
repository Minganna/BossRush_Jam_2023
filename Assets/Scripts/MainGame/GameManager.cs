using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(AudioSource))]
public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    Camera cam;
    Transform camTransf;

    public AudioSource fxSource;
    public AudioClip[] audioEndLevel;

    BossLogic boss;

    float tiltAngle=7.36f;
    float smooth = 1.0f;
    float camX;
    StaticValues sv = new StaticValues();

    private void Awake()
    {
        instance = this;

    }
    
    private void Start() 
    {
        boss= BossLogic.instance;
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
        sv.setSceneToLoad(scene);
        SceneManager.LoadScene(2);
    }

    public void isPlayerMoving(float movement)
    {
        if(camTransf)
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
            float xCamera= 0.0f;
            if(boss.Boss == 0)
            {
                xCamera= 9.77f;
            }
            if(boss.Boss == 1)
            {
                xCamera= -12.3f;
            }
            if(boss.Boss == 2)
            {
                xCamera= 0.0f;
            }
            Quaternion targetRot = Quaternion.Euler(xCamera, tiltAngle, 0.0f);
            camTransf.rotation = Quaternion.Slerp(camTransf.rotation, targetRot, Time.deltaTime * smooth);
        }
        else
        {
            cam= Camera.main;
            if(cam)
            {
                camTransf = cam.GetComponent<Transform>(); 

            }
            boss= BossLogic.instance;
        }
       
    }

    public void playEndSound(bool victory)
    {
        int audioToPlay =0;
        if(victory)
        {
            audioToPlay = 1;
        }
        if(audioEndLevel[audioToPlay] && fxSource)
        {
            fxSource.PlayOneShot(audioEndLevel[audioToPlay]);
        }
    }
}
