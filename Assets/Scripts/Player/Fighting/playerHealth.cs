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

    public GameObject[] images;
    //reference to the gameManager instance
    GameManager manager;

    float deathAnimation =3.0f;

    void Start()
    {
        manager = GameManager.instance; 
        currentHealth = maxHealth;
    }

    public void Damage(int damageTaken)
    {
       currentHealth--;
       if(currentHealth > 0 && currentHealth < maxHealth)
       {
            images[currentHealth].SetActive(false);
       }
       if(currentHealth <= 0 && !isDeath)
       {
         isDeath=true;
         StartCoroutine(waitForDeathAnimation());
       } 
    }

    IEnumerator waitForDeathAnimation()
    {
        Debug.Log("Death Animation");
        //yield on a new YieldInstruction that waits for 5 seconds.
        yield return new WaitForSeconds(deathAnimation);
        manager.LoadScene(0);
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if(other.gameObject.tag =="Boss")
        {
            Damage(1);
        }
    }
}
