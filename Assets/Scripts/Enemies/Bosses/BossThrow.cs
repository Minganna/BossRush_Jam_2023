using UnityEngine;

public class BossThrow : MonoBehaviour
{
    //the spawning point for bullets
    [SerializeField]
    Transform SpawningPointDrawerOne;
    
    private GameObject socks;

    private void Start()
    {
        socks=Resources.Load<GameObject>("Prefab/Boss1/socksObj");
    }
    //function used for the first boss first throw
    public void throwOne()
    {
        Debug.Log("is throwing");
        if(socks && SpawningPointDrawerOne)
        {
            GameObject tmpThrowObject= Instantiate(socks,SpawningPointDrawerOne.position,SpawningPointDrawerOne.rotation);
            SocksLogic socktmp = tmpThrowObject.GetComponent<SocksLogic>();
            if(socktmp)
            {
                socktmp.boss=this.transform;
            }
        }
        
    }
}
