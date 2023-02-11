using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class BossLogic : MonoBehaviour
{
    //reference of the gameobject anim
    Animator anim;
    //reference of the current animation key playing
    string currentAnimPlay;
    //bool that checks if the animation can be played again
    bool animShouldPlay = true;
    [SerializeField]
    float animationTime=1.0f;
    //float used to restore the time between attack for the boss battle
    float tempAnimationtime;
    // int used to check how many attacks the boss did
    int numbOfAttacks = 0;

    // keep a copy of the executing script
    private IEnumerator coroutine;

    public static BossLogic instance;

    //boolean that indicates if the health animation is playing
    bool playHealthAnim=false;
    //boolean that indicates if the attack animation is playing
    bool playAttackAnim = false;
    //boolean that indicates if the health animation should have played during an attack animation
    bool healthAnimRequest = false;
    //boolean that indicates if the boss can be damaged
    public bool canBeHit = true;
    //string that keep track of the health animation request name
    string requestName;
    // int that might change depending on phases 
    [SerializeField]
    public int phaseAttacks;
    //the attack animation that the boss should do
    int currentBossAttack;
    // integer that is used to check the current boss phase
    int currentPhase = 1;

    bool firstAttack = true;
    // refernce to the faceAnimation script, only first boss has it
    faceAnimation face;
    public int Boss;
        
    private void Awake()
    {
        instance = this;

    }
    // Start is called before the first frame update
    void Start()
    {
        anim = this.GetComponent<Animator>();
        face = this.GetComponent<faceAnimation>();
        tempAnimationtime=animationTime;
        currentBossAttack = 1;
        if(gameObject.tag=="Boss1")
        {
            Boss = 0;
        }
        if(gameObject.tag=="Boss2")
        {
            Boss = 1;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(animShouldPlay)
        {
            animShouldPlay = false;
            coroutine = playAttackAnimation(animationTime,"attacks",currentBossAttack);
            StartCoroutine(coroutine);
            if(Boss ==0 && firstAttack && phaseAttacks > 0)
            {
                firstAttack = false;
                animationTime=0.5f;
            }
        }
    }
    //function called by the Health Script
    public void playHealthAnimation(float secondsToWait, string animationName, bool animationParam)
    {
        animShouldPlay = false;
        StartCoroutine(playAnimation(secondsToWait, animationName, animationParam));
    }
    //function called by the death or knocked animations
    public void stopAnim()
    {
        animShouldPlay = true;
        anim.SetBool(currentAnimPlay, false);
        playHealthAnim = false;
        canBeHit = true;
    }
    //function called by the attacks animations
    public void stopAttackAnim()
    {
        anim.SetInteger(currentAnimPlay, 0);
        canBeHit = true;
        if(currentBossAttack==3)
        {
            currentBossAttack = 1;
        }
        if(numbOfAttacks <= (phaseAttacks-1))
        {
            if(face)
            {
                face.isLastAttack = false;
            }
            numbOfAttacks++;
            if(Boss != 1)
            {
                animationTime=0.5f;
            }
            else
            {
                animationTime=3.0f;
            }
            if(currentPhase > 1)
            {
                int randNumber=Random.Range(0, 2);
                if(randNumber==0)
                {
                    currentBossAttack = 1;
                }
                if(randNumber==1)
                {
                    currentBossAttack = 2;
                }
                if(numbOfAttacks== phaseAttacks && currentPhase==3)
                {
                    currentBossAttack = 3;
                }  
            }
        }
        else
        {
            animationTime=tempAnimationtime;
            numbOfAttacks=0;
            playAttackAnim=false;
            if(face)
            {
                face.isLastAttack = true;
            }
            if(healthAnimRequest)
            {
                StartCoroutine(playAnimation(0.0f, requestName, true));
                healthAnimRequest = false;
            }
        }

        animShouldPlay = true;
    }

    public void StopHiding()
    {
        StartCoroutine(stopRatHide()); 
    }

    public void changePhase(int BossPhase)
    {
        currentPhase++;
        if(BossPhase == 1)
        {
            if(Boss==0)
            {
                phaseAttacks*= 2;
            }
            if(Boss==1)
            {
                phaseAttacks = 1;
            }   
        }
    }

    IEnumerator playAnimation( float secondsToWait,string animationName, bool animationParam)
    {
        StopCoroutine(coroutine);
        anim.SetInteger("attacks", 0);
        playAttackAnim=false;
        numbOfAttacks=0;
        animationTime=tempAnimationtime;
        playHealthAnim = true;
        if(Boss==0 && animationName=="isNewPhase" && face)
        {
            face.startKnockedFace();
        }
        if(Boss==0 && animationName=="isDeath" && face)
        {
            face.defeatedFace();
            secondsToWait =0.5f;
        }
        if(!playAttackAnim)
        {
            yield return new WaitForSeconds(secondsToWait);
            canBeHit = false;
            anim.SetBool(animationName, animationParam);
            currentAnimPlay = animationName;
        }
        else
        {
            healthAnimRequest = true;
            requestName = animationName;
        }    

    }
    IEnumerator playAttackAnimation(float secondsToWait, string animationName, int animationParam)
    {        
        if(!playHealthAnim)
        {
            yield return new WaitForSeconds(secondsToWait);

            if(Boss==1 && currentBossAttack == 2)
            {
                canBeHit = false;
            }
            anim.SetInteger(animationName, animationParam);
            currentAnimPlay = animationName;
            playAttackAnim = true;
        }

    }

    IEnumerator stopRatHide()
    {
        currentBossAttack = 1;
        numbOfAttacks=0;
        yield return new WaitForSeconds(2.0f);
        animShouldPlay = true;
        anim.SetInteger(currentAnimPlay, 0);
        canBeHit = true;
        animationTime=tempAnimationtime;
        
    }

    public int getCurrentPhase()
    {
        return currentPhase;
    }
}
