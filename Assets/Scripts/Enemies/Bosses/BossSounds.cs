using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class BossSounds : MonoBehaviour
{
    public AudioSource audioSource;
    public AudioClip[] audioAttackClipArray;
    public AudioClip[] audioDrawersClipArray;
    public AudioClip[] audioMouseHidingClipArray;
    public AudioClip[] audioKnockBackClipArray;
    public AudioClip audioDeath;

    BossLogic bl;
    
    private void Start() 
    {
        audioSource=this.GetComponent<AudioSource>();   
        bl=BossLogic.instance;
    }

    public void playAttackSound1()
    {
        if(audioDrawersClipArray.Length>0)
        {
            if(audioDrawersClipArray[0])
            {
                audioSource.PlayOneShot(audioDrawersClipArray[0]);
            }
        }
        if(audioAttackClipArray.Length>0)
        {
            audioSource.PlayOneShot(RandomAttackClip());
        }
    }
    public void playAttackSound2()
    {
        if(audioDrawersClipArray[1])
        {
            audioSource.PlayOneShot(audioDrawersClipArray[1]);
        }
        if(audioAttackClipArray.Length>0)
        {
            audioSource.PlayOneShot(RandomAttackClip());
        }
    }
    public void playAttackSound3()
    {
        if(audioDrawersClipArray[2])
        {
            audioSource.PlayOneShot(audioDrawersClipArray[2]);
        }
        if(audioAttackClipArray.Length>0)
        {
            audioSource.PlayOneShot(RandomAttackClip());
        }
    }

    public void playHidingSounds()
    {
        if(audioMouseHidingClipArray.Length>0)
        {
            audioSource.PlayOneShot(RandomHidingClip());
        }
    }

    public void playKnockBack1()
    {
        Debug.Log(bl.getCurrentPhase()-2);
        if(audioKnockBackClipArray[bl.getCurrentPhase()-2])
        {
            audioSource.PlayOneShot(audioKnockBackClipArray[bl.getCurrentPhase()-2]);
        }
    }
    
    public void playDeath()
    {
        if(audioDeath)
        {
            audioSource.PlayOneShot(audioDeath);
        }
    }

    AudioClip RandomAttackClip()
    {
        return audioAttackClipArray[Random.Range(0, audioAttackClipArray.Length-1)];
    }
    
    AudioClip RandomHidingClip()
    {
        return audioMouseHidingClipArray[Random.Range(0, audioMouseHidingClipArray.Length-1)];
    }
}
