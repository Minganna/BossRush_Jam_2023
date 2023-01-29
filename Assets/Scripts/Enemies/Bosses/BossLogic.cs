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
    public int phaseAttacks;


    // Start is called before the first frame update
    void Start()
    {
        anim = this.GetComponent<Animator>();
        tempAnimationtime=animationTime;
    }

    // Update is called once per frame
    void Update()
    {
        if(animShouldPlay)
        {
            animShouldPlay = false;
            StartCoroutine(playAttackAnimation(animationTime,"attacks",1));
        }
    }

    public void playHealthAnimation(float secondsToWait, string animationName, bool animationParam)
    {
        StartCoroutine(playAnimation(secondsToWait, animationName, animationParam));
    }

    public void stopAnim()
    {
        anim.SetBool(currentAnimPlay, false);
        playHealthAnim = false;
        canBeHit = true;
    }
    public void stopAttackAnim()
    {
        anim.SetInteger(currentAnimPlay, 0);
        Debug.Log(numbOfAttacks);
        canBeHit = true;
        if(numbOfAttacks < phaseAttacks)
        {
            animationTime=0.5f;
            numbOfAttacks++;
        }
        else
        {
            animationTime=tempAnimationtime;
            numbOfAttacks=0;
        }
        if(healthAnimRequest)
        {
            StartCoroutine(playAnimation(0.0f, requestName, true));
            healthAnimRequest = false;
        }

    }

    IEnumerator playAnimation( float secondsToWait,string animationName, bool animationParam)
    {
        yield return new WaitForSeconds(secondsToWait);
        if(playAttackAnim)
        {
            canBeHit = false;
            anim.SetBool(animationName, animationParam);
            currentAnimPlay = animationName;
            playHealthAnim = true;
        }
        else
        {
            healthAnimRequest = true;
            requestName = animationName;
        }    

    }
    IEnumerator playAttackAnimation(float secondsToWait, string animationName, int animationParam)
    {
        yield return new WaitForSeconds(secondsToWait);
        if(!playHealthAnim)
        {
            canBeHit = false;
            anim.SetInteger(animationName, animationParam);
            currentAnimPlay = animationName;
            playAttackAnim = true;
        }
        animShouldPlay = true;
    }
}
