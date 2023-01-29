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
    // Start is called before the first frame update
    void Start()
    {
        rigidbody2D=GetComponent<Rigidbody2D>();

        power= new Vector2(-1000.0f,0.0f);
        if(rigidbody2D)
        {
            rigidbody2D.AddForce(power);
        }   
    }

    private void Update() {
        if(Vector3.Distance(transform.position,boss.position)>maxDistance)
        {
            Destroy(gameObject);
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

    }
}
