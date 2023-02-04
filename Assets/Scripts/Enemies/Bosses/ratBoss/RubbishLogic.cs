using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RubbishLogic : MonoBehaviour
{
    Rigidbody2D rigidbody2D;
    Vector2 power;   
    [SerializeField]
    int Damage;
    [SerializeField]
    Transform SpawningPoint;
    private GameObject Rubbish;

    GameObject tmpThrowObject; 

    float throwPower;

    bool direction;

    public void Throw(bool rightOrLeft)
    {
        direction=rightOrLeft;
        rigidbody2D=GetComponent<Rigidbody2D>();
        throwPower=-1000.0f;
        power= new Vector2(0.0f,throwPower);
        if(rigidbody2D)
        {
            rigidbody2D.AddForce(power);
        }  
        Rubbish = Resources.Load<GameObject>("Prefab/Boss2/rubbish");
    }

    public void bounceUp(float xPower)
    {
        rigidbody2D=GetComponent<Rigidbody2D>();
        throwPower=1000.0f;
        power= new Vector2(xPower,throwPower);
        if(rigidbody2D)
        {
            rigidbody2D.AddForce(power);
        }  
        Rubbish = Resources.Load<GameObject>("Prefab/Boss2/rubbish");
    }

    private void OnCollisionEnter2D(Collision2D other) {
        if(other.gameObject.tag=="ratPlatforms")
        {
            if(Rubbish && SpawningPoint)
            {
                tmpThrowObject = Instantiate(Rubbish,SpawningPoint.position,SpawningPoint.rotation);
                RubbishLogic temp= tmpThrowObject.GetComponent<RubbishLogic>();
                float directionBounce = 500.0f;
                if(direction)
                {
                    directionBounce = -500.0f;
                }
                temp.bounceUp(directionBounce);
            }
            StartCoroutine(destroyAll());
        }
        playerHealth playerHP= other.gameObject.GetComponent<playerHealth>();
        if (playerHP)
        {
            playerHP.Damage(Damage);
            Destroy(gameObject);
            if(tmpThrowObject)
            {
                Destroy(tmpThrowObject);
            }
        }
    }

    IEnumerator destroyAll()
    {
        yield return new WaitForSeconds(2.0f);
        Destroy(tmpThrowObject);
        Destroy(gameObject);
    }
}
