using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarrelLogic : MonoBehaviour
{
    [SerializeField]
    int Damage;

    Rigidbody2D rigidBody2D;
    float oppositeDir = -500.0f;

    int touchedSafeColliders=0;

    private void Start() {
        rigidBody2D = this.GetComponent<Rigidbody2D>();
    }

    private void OnCollisionEnter2D(Collision2D other) 
    {
        playerHealth playerHP= other.gameObject.GetComponent<playerHealth>();
        if (playerHP)
        {
            playerHP.Damage(Damage);
            Destroy(gameObject);
        }
        if(other.gameObject.tag=="SafeCollider")
        { 
            Throw();
            touchedSafeColliders++;
            if(touchedSafeColliders>=3)
            {
                Destroy(gameObject);
            }
        }
    }

    public void Throw()
    {
        if(GetComponent<Rigidbody2D>())
        {
            Vector2 power= new Vector2(oppositeDir,0.0f);
            GetComponent<Rigidbody2D>().AddForce(power);
            oppositeDir = -oppositeDir;
        }  
        else
        {
            rigidBody2D = this.GetComponent<Rigidbody2D>();
            Vector2 power= new Vector2(oppositeDir,0.0f);
            GetComponent<Rigidbody2D>().AddForce(power);
            oppositeDir = -oppositeDir;
        }
    }
}
