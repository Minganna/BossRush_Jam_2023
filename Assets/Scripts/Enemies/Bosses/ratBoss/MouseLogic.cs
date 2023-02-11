using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseLogic : MonoBehaviour
{
    //the spawning point for bullets
    [SerializeField]
    Transform SpawningThrowPoint;
    private GameObject ThrowObject;
    BoxCollider2D parentCollider;
    BossLogic bl;

    bool rightOrLeft =true;
    // Start is called before the first frame update
    void Start()
    {
        ThrowObject = Resources.Load<GameObject>("Prefab/Boss2/ThrowObject");
        parentCollider= GetComponentInParent(typeof(BoxCollider2D)) as BoxCollider2D;
        bl=this.GetComponent<BossLogic>();
    }

    //function used for the second boss to throw
    public void throwOne()
    {
        if(ThrowObject && SpawningThrowPoint)
        {
            parentCollider.enabled = false;
            GameObject tmpThrowObject= Instantiate(ThrowObject,SpawningThrowPoint.position,SpawningThrowPoint.rotation);
            RubbishLogic temp= tmpThrowObject.GetComponent<RubbishLogic>();
            int itemToThrow = 1;
            if(bl && bl.getCurrentPhase() == 3)
            {
                itemToThrow++;
            }
            temp.Throw(rightOrLeft,itemToThrow);
            rightOrLeft = !rightOrLeft;
        }
        
    }

    public void ReactivateCollider()
    {
        parentCollider.enabled = true;
    }

}
