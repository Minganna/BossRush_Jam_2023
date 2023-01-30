using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SocksLogic : MonoBehaviour
{
    Rigidbody2D rigidbody2D;
    Vector2 power;
    public Transform boss;
    public float maxDistance;
    [SerializeField]
    int socksDamage;

    float throwPower;

    private void Update() {
        if(Vector3.Distance(transform.position,boss.position)>maxDistance)
        {
            Destroy(gameObject);
        }
    }

    public void setPower(float currentPower)
    {
        rigidbody2D=GetComponent<Rigidbody2D>();
        throwPower=currentPower;
        power= new Vector2(throwPower,0.0f);
        if(rigidbody2D)
        {
            rigidbody2D.AddForce(power);
        }  
    }

    public void setPower(float currentPower, float YPower )
    {
        rigidbody2D=GetComponent<Rigidbody2D>();
        throwPower=currentPower;
        power= new Vector2(throwPower,YPower);
        if(rigidbody2D)
        {
            rigidbody2D.AddForce(power);
        }  
    }



   private void OnCollisionEnter2D(Collision2D other)
    {
        playerHealth playerHP= other.gameObject.GetComponent<playerHealth>();
        if (playerHP)
        {
            playerHP.Damage(socksDamage);
            Destroy(gameObject);
        }
        if(gameObject.tag=="rightDrawer")
        {
            Destroy(gameObject);
        }

    }
}
