using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerHealth : MonoBehaviour
{
    //the maximum health the gameobject can have
    [SerializeField]
    int maxHealth = 3;
    private int currentHealth;
    private bool isDeath=false;

    private bool canBeHit=true;

    public GameObject[] images;
    public GameObject[] scratchImages;
    public GameObject[] repairs;
    //reference to the gameManager instance
    GameManager manager;
    
    PlayerMovements playerMovements;

    float deathAnimation =3.0f;

    void Start()
    {
        manager = GameManager.instance; 
        currentHealth = maxHealth;
        playerMovements = this.GetComponent<PlayerMovements>();
    }

    public void Damage(int damageTaken)
    {
        if(canBeHit)
        {
            canBeHit = false;
            currentHealth--;
            if(currentHealth==2)
            {
                scratchImages[0].SetActive(true);
                if(repairs[0].activeSelf)
                {
                    repairs[0].SetActive(false);
                }
            }
            if(currentHealth==1)
            {
                scratchImages[1].SetActive(true);
                if(repairs[1].activeSelf)
                {
                    repairs[1].SetActive(false);
                }
            }
            if(currentHealth >= 0 && currentHealth < maxHealth)
            {
                images[currentHealth].SetActive(false);
            }
            if(currentHealth <= 0 && !isDeath)
            {
                scratchImages[0].SetActive(false);
                scratchImages[1].SetActive(false);
                isDeath=true;
                playerMovements.death();
                StartCoroutine(waitForDeathAnimation());
            } 
            StartCoroutine(waitForCanBeHit());
        }

    }

    IEnumerator waitForDeathAnimation()
    {
        Debug.Log("Death Animation");
        manager.playEndSound(false);
        //yield on a new YieldInstruction that waits for 5 seconds.
        yield return new WaitForSeconds(deathAnimation);
        manager.LoadScene(0);
    }

    IEnumerator waitForCanBeHit()
    {
        //yield on a new YieldInstruction that waits for 5 seconds.
        yield return new WaitForSeconds(0.5f);
        canBeHit = true;
    }


    private void OnCollisionEnter2D(Collision2D other)
    {
        if(other.gameObject.tag =="Boss")
        {
            Damage(1);
        }
    }

    public void addRepair()
    {
        if(currentHealth==2)
        {
            images[currentHealth].SetActive(true);
            currentHealth++;
            repairs[0].SetActive(true);
            scratchImages[0].SetActive(false);
        }
        if(currentHealth==1)
        {
            images[currentHealth].SetActive(true);
            currentHealth++;
            repairs[1].SetActive(true);
            scratchImages[1].SetActive(false);
        }

    }
}
