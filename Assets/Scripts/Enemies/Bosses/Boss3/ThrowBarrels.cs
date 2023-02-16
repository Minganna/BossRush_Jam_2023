using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrowBarrels : MonoBehaviour
{
    [SerializeField]
    Transform SpawningThrowPoint;
    [SerializeField]
    Transform SpawningThrowPointBig;
    [SerializeField]
    BoxCollider2D BossCollider;
    private GameObject smallBarrel;
    private GameObject bigBarrel;
    Transform player;
    Vector2 currentOffset;
    // Start is called before the first frame update
    void Start()
    {
        smallBarrel = Resources.Load<GameObject>("Prefab/Boss3/Barrell");
        bigBarrel = Resources.Load<GameObject>("Prefab/Boss3/BigBarrell Variant");
        player= GameObject.FindWithTag("Player").transform;
        currentOffset=BossCollider.offset;

    }

    public void ThrowBarrel()
    {
        if(smallBarrel && SpawningThrowPoint)
        {
            GameObject tmpbarrelObject= Instantiate(smallBarrel,SpawningThrowPoint.position,SpawningThrowPoint.rotation);
            BarrelLogic temp = tmpbarrelObject.GetComponent<BarrelLogic>();
            temp.Throw();
        }
    }

    public void ThrowBigBarrel()
    {
        if(bigBarrel && SpawningThrowPointBig)
        {
            GameObject tmpbarrelObject= Instantiate(bigBarrel,SpawningThrowPointBig.position,SpawningThrowPointBig.rotation);
            BarrelLogic temp = tmpbarrelObject.GetComponent<BarrelLogic>();
            temp.Throw(-500);
        }
    }

    public int punchOrThrow () 
    {
        if(Vector3.Distance(transform.position,player.position) > 8.0f)
        {
            if(BossCollider)
            {

                BossCollider.offset = new Vector2(currentOffset.x, currentOffset.y);
            }
            return 1;
        } 
        else
        {
            if(BossCollider)
            {

                BossCollider.offset = new Vector2(-3.51f, currentOffset.y);
            }
            return 2;
        }
        
    }
    
    
}
