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


    int selection=0;

    bool isThisContinueMenu;


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
        }
        if(this.gameObject.name =="Continue Menu")
        {
            isThisContinueMenu = true;
        }
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 moveValue = menuMovements.ReadValue<Vector2>();
        if(moveValue.y == 1)
        {
            if(pointer && StartGame)
            {
                pointer.transform.SetParent(StartGame.transform);
                pointer.transform.localPosition = new Vector3(pointer.transform.localPosition.x, 9.0f, 0.0f);
            }
            selection = 0;
        }
        if(moveValue.y == -1)
        {
            if(pointer && exitGame)
            {
                pointer.transform.SetParent(exitGame.transform);
                pointer.transform.localPosition = new Vector3(pointer.transform.localPosition.x, 9.0f, 0.0f);
            }   
            selection = 1;
        }
    }

    void actionMenu(InputAction.CallbackContext context)
    {
        int scene=1;
        if(selection==0)
        {
            scene = 2;
        }
        if(selection==1 &&!isThisContinueMenu)
        {
            Application.Quit();
        }
        if(selection==1 &&isThisContinueMenu)
        {
            scene = 0;
        }
        SceneManager.LoadScene(scene);
    }
}
