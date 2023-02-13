using UnityEngine;
using System.Collections;
using UnityEngine.InputSystem;

public class Shooting : MonoBehaviour
{
    //the spawning point for bullets
    [SerializeField]
    Transform SpawningPoint;
    //reference to the new input system action
    [SerializeField]
    Player_Actions playerActions;

    // Assign a Rigidbody component in the inspector to instantiate
    private GameObject projectile;
    // reference to the action used to jump
    private InputAction fire;
    //the time the player needs to wait between shoots
    public float shootTime = 0.2f;
    //boolean that indicates if the player is requesting to shoot
    bool isFiring = false;
    //boolean that determine if the time has passed and the player can now shoot
    bool canShoot = true;

    public bool isDeath=false;
    [SerializeField]
    playerFx playerSounds;

    private void Awake()
    {
        playerActions = new Player_Actions();
        projectile=Resources.Load<GameObject>("Prefab/bullet");
        if(!projectile)
        {
            Debug.Log("Asset not found");
        }
    }
    private void OnEnable()
    {

        fire = playerActions.Player.Fire;
        fire.Enable();
        fire.performed += fireBullet;
        fire.canceled += stopFireBullet;
    }
    

    private void OnDisable()
    {
        fire.Disable();
    }

    private void Update()
    {
        if(!isDeath)
        {
            if(isFiring && canShoot)
            {
                StartCoroutine(waitForShootAgain());
                if(projectile)
                {
                    playerSounds.playAttack();
                    var currentProjectile = Instantiate(projectile, SpawningPoint.position, SpawningPoint.rotation);
                    Bullet tempBullet = currentProjectile.GetComponent<Bullet>();
                    if (tempBullet)
                    {
                        tempBullet.player = transform.parent;
                        if (transform.parent.localScale.x > 0)
                        {
                            tempBullet.isRight = true;
                        }
                        else
                        {
                            tempBullet.isRight = false;
                        }
                    }
                }
            }

        }
        else
        {
            Destroy(gameObject);
        }

    }

    void fireBullet(InputAction.CallbackContext context)
    {
        isFiring = true;
    }
    void stopFireBullet(InputAction.CallbackContext context)
    {
        isFiring = false;
    }

    IEnumerator waitForShootAgain()
    {
        canShoot = false;
        //yield on a new YieldInstruction that waits for 5 seconds.
        yield return new WaitForSeconds(shootTime);
        canShoot = true;
    }
}
