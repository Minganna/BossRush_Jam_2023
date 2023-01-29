using System.Collections;
using UnityEngine;

public class Health : MonoBehaviour
{
    //the maximum health the gameobject can have
    [SerializeField]
    float maxHealth = 400.0f;
    //float that represent when the boss should change stage
    float stageHealth;
    private float currentHealth;
    //variable that define how long the death animation should last
    float deathAnimation = 5.0f;
    //reference to the gameManager instance
    GameManager manager;
    //float that represent at what health value the boss should change stage
    float stages;
    //bool indicating if the gameobject with this script attached is boss
    bool isBoss = false;
    //int that indicates what stage the boss is currently in
    int BossPhase=0;
    // reference to the boss logic script if is a boss
    BossLogic bl;
    // Start is called before the first frame update
    void Start()
    {
        manager = GameManager.instance;
        bl = GetComponentInChildren<BossLogic>();
        if(bl)
        {
            isBoss = true;
        }    
        currentHealth = maxHealth;
        stageHealth = maxHealth;
        stages = maxHealth / 3;
    }

    public void Damage(float hitDamage)
    {
        if(bl.canBeHit)
        {
            Debug.Log(currentHealth);
            currentHealth -= hitDamage;
        }
        if(isBoss)
        {
            //different stages
            if (currentHealth <= (stageHealth - stages))
            {
                stageHealth = stageHealth - stages;
                if(BossPhase<2)
                {
                    bl.playHealthAnimation(0.0f, "isNewPhase", true);
                    BossPhase++;
                    if(BossPhase==1)
                    {
                        bl.phaseAttacks*= 2;
                    }
                    Debug.Log("entering phase: " + BossPhase);

                }

            }
        }
        if(currentHealth <= 0)
        {
            if(isBoss)
            {
                StartCoroutine(waitForDeathAnimation());
            }
            else
            {
                Destroy(gameObject);
            }
            
        }
    }


    IEnumerator waitForDeathAnimation()
    {
        Debug.Log("Death Animation");
        bl.playHealthAnimation(0.0f,"isDeath",true);
        //yield on a new YieldInstruction that waits for 5 seconds.
        yield return new WaitForSeconds(deathAnimation);
        manager.LoadScene(0);
    }
}
