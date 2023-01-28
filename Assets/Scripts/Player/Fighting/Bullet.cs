using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField]
    float speed=20f;
    public bool isRight;
    public Transform player;

    public float maxDistance = 20.0f;
    public float bulletDamage = 1.0f;

    // Update is called once per frame
    void Update()
    {
        if(isRight)
        {
            transform.position += transform.right * Time.deltaTime * speed;
        }
        else
        {
            transform.position += -transform.right * Time.deltaTime * speed;
        }
        if(Vector3.Distance(transform.position,player.position)>maxDistance)
        {
            Destroy(gameObject);
        }

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("collided");
        Health enemyHealth= collision.gameObject.GetComponent<Health>();
        if (enemyHealth)
        {
            enemyHealth.Damage(bulletDamage);
        }
        Destroy(gameObject);
    }
}
