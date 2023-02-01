using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class faceAnimation : MonoBehaviour
{
    //reference of the gameobject anim
    Animator anim;
    [SerializeField]
    GameObject face;

    public bool isLastAttack=false;
    // Start is called before the first frame update
    void Start()
    {
        anim =face.GetComponent<Animator>();
    }

    public void startFaceAttackAnim()
    {
        anim.SetBool("isAttacking",true);
    } 

    public void stopFaceAttackAnim()
    {
        if(isLastAttack)
        {
            anim.SetBool("isAttacking",false);
        }
    }

    public void defeatedFace()
    {
        anim.SetBool("isDefeated",true);
    }

    public void startKnockedFace()
    {
        anim.SetBool("isKnocked",true);
    }
    
    public void stopKnockedFace()
    {
        anim.SetBool("isKnocked",false);
    }
}
