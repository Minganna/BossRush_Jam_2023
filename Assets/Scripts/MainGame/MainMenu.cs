using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [SerializeField]
    Player_Actions playerActions;
    // reference to the action used 
    private InputAction menuMovements;
    private InputAction actionPressed;

    [SerializeField]
    GameObject StartGame;
    [SerializeField]
    GameObject exitGame;
    [SerializeField]
    GameObject pointer;
    StaticValues sv = new StaticValues();

    int selection=0;

    bool isThisContinueMenu;

    bool isThisScoreMenu;

    private void Awake()
    {
        playerActions = new Player_Actions();
    }

    private void OnEnable()
    {
        menuMovements = playerActions.MainMenu.MoveDown;
        menuMovements.Enable();
        actionPressed = playerActions.MainMenu.MenuClick;
        actionPressed.Enable();
        actionPressed.performed += actionMenu;
    }

    private void OnDisable()
    {
        menuMovements.Disable();
        actionPressed.Disable();
    }

    private void Start() {
        if(this.gameObject.name =="Main Menu")
        {
            isThisContinueMenu = false;
            isThisScoreMenu=false;
        }
        if(this.gameObject.name =="Continue Menu")
        {
            isThisContinueMenu = true;
            isThisScoreMenu=false;
            sv.addValueTodamageReceived(0);
            sv.addValueToHealing(0);
        }
        if(this.gameObject.name =="ScoreMenu" || this.gameObject.name =="CreditsMenuMenu")
        {
            isThisScoreMenu=true;
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        if(isThisScoreMenu == false)
        {
             Vector2 moveValue = menuMovements.ReadValue<Vector2>();
            if(moveValue.y == 1 || moveValue.x == -1)
            {
                if(pointer && StartGame)
                {
                    pointer.transform.SetParent(StartGame.transform);
                    if(!isThisContinueMenu)
                    {
                        pointer.transform.localPosition = new Vector3(pointer.transform.localPosition.x, 9.0f, 0.0f);
                    }
                    else
                    {
                        pointer.transform.localPosition = new Vector3(-80.0f, 30.0f, 0.0f);
                    }
                    
                }
                selection = 0;
            }
            if(moveValue.y == -1 || moveValue.x == 1)
            {
                if(pointer && exitGame)
                {
                    pointer.transform.SetParent(exitGame.transform);
                    if(!isThisContinueMenu) 
                    {
                        pointer.transform.localPosition = new Vector3(pointer.transform.localPosition.x, 9.0f, 0.0f);
                    }
                    else
                    {
                        pointer.transform.localPosition = new Vector3(-80.0f, 30.0f, 0.0f);
                    }
                }   
                selection = 1;
            }
        }
       
    }

    void actionMenu(InputAction.CallbackContext context)
    {
        bool quitting=false;
        int scene=1;
        if(selection==0 && !isThisScoreMenu)
        {
            if(isThisContinueMenu)
            {
                sv.setSceneToLoad(3);
            }
            else
            {
                sv.setSceneToLoad(8);
            }
            
        }
        if(isThisScoreMenu)
        {
            if(this.gameObject.name =="CreditsMenuMenu")
            {
                Debug.Log("Here How");
                SceneManager.LoadScene(0);
            }
            else
            {
                SceneManager.LoadScene(7);
            }
        }
        if(selection==1 &&!isThisContinueMenu)
        {
            Application.Quit();
            quitting=true;
        }
        if(selection==1 &&isThisContinueMenu)
        {
            Debug.Log("Here");
           sv.setSceneToLoad(0);
        }
        if(!isThisScoreMenu &&!quitting)
        {
            SceneManager.LoadScene(2);
        }
        
    }
}
