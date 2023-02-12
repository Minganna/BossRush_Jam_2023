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
        Rubbish = Resources.Load<GameObject>("Prefab/Boss2/rubbish");
    }

    public void bounceUp(float xPower)
    {
        throwPower=1000.0f;
        power= new Vector2(xPower,throwPower);
        if(rigidbody2D)
        {
            rigidbody2D.AddForce(power);
        }    
    }

    private void OnCollisionEnter2D(Collision2D other) {
        if(gameObject.tag=="trashBag" && other.gameObject.tag=="ratPlatforms")
        {
            if(Rubbish && SpawningPoint)
            {
                for(int i=0; i < objectToTrow;i++)
                {
                    tmpThrowObject.Add((GameObject)Instantiate(Rubbish,SpawningPoint.position,SpawningPoint.rotation));
                    RubbishLogic temp= tmpThrowObject[i].GetComponent<RubbishLogic>();
                    Rigidbody2D body2D=tmpThrowObject[i].GetComponent<Rigidbody2D>();
                    temp.setRigidbody(body2D);
                    float directionBounce = 500.0f;
                    if(direction)
                    {
                        directionBounce = -500.0f;
                    }
                    if(temp)
                    {
                        temp.bounceUp(directionBounce);
                    }
                    direction= !direction;
                }

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

    void setRigidbody(Rigidbody2D body)
    {
        rigidbody2D = body;
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
        Destroy(gameObject);
    }
}
