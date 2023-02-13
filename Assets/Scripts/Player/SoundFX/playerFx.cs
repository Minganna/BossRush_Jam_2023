using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerFx : MonoBehaviour
{
    AudioSource audioSource;
    public AudioClip[] audioAttackClipArray;
    public AudioClip[] audioStartBattleArray;
    public AudioClip[] audioJumpArray;
    public AudioClip[] audioHitArray;
    public AudioClip audioDeath;
    public AudioClip audioWin;
    // Start is called before the first frame update
    void Start()
    {
        audioSource= this.GetComponent<AudioSource>();   
        if(audioStartBattleArray.Length>0)
        {
            audioSource.PlayOneShot(RandomBattleCry());
        }

    }

    public void playAttack()
    {
        if(audioAttackClipArray.Length>0)
        {
            audioSource.PlayOneShot(RandomAttackClip());
        }
    }

    public void playWin()
    {
        if(audioWin)
        {
            audioSource.PlayOneShot(audioWin);
        }
    }

    public void playJump()
    {
        if(audioJumpArray.Length>0)
        {
            audioSource.PlayOneShot(RandomJump());
        }
    }

    public void playDamagesSound(bool death)
    {
        if(audioJumpArray.Length>0 && !death)
        {
            audioSource.PlayOneShot(RandomHit());
        }
        if(audioDeath && death)
        {
            audioSource.PlayOneShot(audioDeath);
        }
    }

    AudioClip RandomAttackClip()
    {
        return audioAttackClipArray[Random.Range(0, audioAttackClipArray.Length-1)];
    }

    AudioClip RandomBattleCry()
    {
        return audioStartBattleArray[Random.Range(0, audioStartBattleArray.Length-1)];
    }

    AudioClip RandomJump()
    {
        return audioJumpArray[Random.Range(0, audioJumpArray.Length-1)];
    }

    AudioClip RandomHit()
    {
        return audioHitArray[Random.Range(0, audioHitArray.Length-1)];
    }

}
