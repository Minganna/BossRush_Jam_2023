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
    float deathAnimation = 2.5f;
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

    [SerializeField]
    Material[] BossMaterials;

    [SerializeField]
    playerFx playerSounds;

    Color damageColor;
    bool colorHasChanged = false;

    bool isDeath=false;

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
        damageColor = new Color(0.4941176f,0.09019608f,0.0f);   
        changeMaterialColor(Color.white);
    }

    void changeMaterialColor(Color color)
    {
        foreach(Material mt in BossMaterials)
        {
            if(mt)
            {
                mt.SetColor("Color_7e558cfd03154e4e812ec7aa32c8ae53",color);
            } 
        }

    }

    public void Damage(float hitDamage)
    {
        if(bl.canBeHit )
        {
            changeMaterialColor(damageColor);
            if(!colorHasChanged)
            {
                StartCoroutine(changeColorBack());
            }
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
                    bl.changePhase(BossPhase);
                    Debug.Log("entering phase: " + BossPhase);

                }

            }
        }
        if(currentHealth <= 0 && !isDeath)
        {
            isDeath=true;
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
        bl.playHealthAnimation(0.0f,"isDeath",true);
        manager.addValueToTime(Time.realtimeSinceStartup/60.0);
        //yield on a new YieldInstruction that waits for 5 seconds.
        yield return new WaitForSeconds(deathAnimation);
        manager.playEndSound(true);
        if(playerSounds)
        {
            playerSounds.playWin();
        }
        yield return new WaitForSeconds(deathAnimation);

        if(bl.Boss == 0)
        {
            manager.LoadScene(4);
        }
        else if(bl.Boss == 1)
        {
            manager.LoadScene(5);
        }
        else if(bl.Boss == 2)
        {
            manager.LoadScene(6);
        }
        
    }

    IEnumerator changeColorBack()
    {
        colorHasChanged = true;
        yield return new WaitForSeconds(0.2f);
        colorHasChanged = false;
        changeMaterialColor(Color.white);
    }
}
