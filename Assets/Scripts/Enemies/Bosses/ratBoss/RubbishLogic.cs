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

    List<GameObject> tmpThrowObject; 

    float throwPower;

    bool direction;

    int objectToTrow = 1;

    public void Throw(bool rightOrLeft, int itemToThrow)
    {
        objectToTrow = itemToThrow;
        tmpThrowObject = new List<GameObject>();
        direction=rightOrLeft;
        rigidbody2D=GetComponent<Rigidbody2D>();
        throwPower=-1000.0f;
        power= new Vector2(0.0f,throwPower);
        if(rigidbody2D)
        {
            rigidbody2D.AddForce(power);
        }  
        if(rightOrLeft)
        {
            Rubbish = Resources.Load<GameObject>("Prefab/Boss2/rubbish");
        }
        else
        {
            Rubbish = Resources.Load<GameObject>("Prefab/Boss2/rubbishCan");
        }
    }

    public void bounceUp(bool rightOrLeft)
    {
        throwPower=1000.0f;
        float xPower = 500.0f;
        if(rightOrLeft)
        {
            xPower = -500.0f;
        }
        power= new Vector2(xPower,throwPower);
        if(rigidbody2D)
        {
            rigidbody2D.AddForce(power);
        }    
        else
        {
            rigidbody2D=GetComponent<Rigidbody2D>();
            rigidbody2D.AddForce(power);
        }
    }

    private void OnCollisionEnter2D(Collision2D other) {
        if(gameObject.tag=="trashBag" && other.gameObject.tag=="ratPlatforms")
        {
            if(Rubbish && SpawningPoint)
            {
                
                StartCoroutine(spawnDelay());
            }
            StartCoroutine(destroyAll());
        }
        playerHealth playerHP= other.gameObject.GetComponent<playerHealth>();
        if (playerHP)
        {
            playerHP.Damage(Damage);
            if(tmpThrowObject != null)
            {
                foreach(GameObject tmp in tmpThrowObject)
                {
                    if(tmp)
                    {
                        Destroy(tmp);
                    }
                }
            }
            Destroy(gameObject);

        }
    }

    IEnumerator spawnDelay()
    {
        for(int i=0; i < objectToTrow;i++)
        {
            GameObject tempGameObject=(GameObject)Instantiate(Rubbish,SpawningPoint.position,SpawningPoint.rotation);
            RubbishLogic temp= tempGameObject.GetComponent<RubbishLogic>();
            if(temp)
            {
                temp.bounceUp(direction);
            }
            direction= !direction;
            tmpThrowObject.Add(tempGameObject);
            yield return new WaitForSeconds(0.3f);
        }
    }



    IEnumerator destroyAll()
    {
        yield return new WaitForSeconds(2.0f);
        foreach(GameObject tmp in tmpThrowObject)
        {
            if(tmp)
            {
                Destroy(tmp);
            }
        }
        tmpThrowObject = new List<GameObject>();
        Destroy(gameObject);
    }
}
