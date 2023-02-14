using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrowBarrels : MonoBehaviour
{
    [SerializeField]
    Transform SpawningThrowPoint;
    private GameObject smallBarrel;
    // Start is called before the first frame update
    void Start()
    {
        smallBarrel = Resources.Load<GameObject>("Prefab/Boss3/Barrell");
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
}
