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

    bool playHealthAnim=false;
    bool playAttackAnim = false;
    bool healthAnimRequest = false;

    public bool canBeHit = true;

    string requestName;
    // Start is called before the first frame update
    void Start()
    {
        anim = this.GetComponent<Animator>();
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
        canBeHit = true;
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
