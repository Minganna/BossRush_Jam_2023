using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrowBarrels : MonoBehaviour
{
    [SerializeField]
    Transform SpawningThrowPoint;
    [SerializeField]
    Transform SpawningThrowPointBig;
    private GameObject smallBarrel;
    private GameObject bigBarrel;
    // Start is called before the first frame update
    void Start()
    {
        smallBarrel = Resources.Load<GameObject>("Prefab/Boss3/Barrell");
        bigBarrel = Resources.Load<GameObject>("Prefab/Boss3/BigBarrell Variant");
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
}
