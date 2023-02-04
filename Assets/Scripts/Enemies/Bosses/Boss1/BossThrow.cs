using UnityEngine;

public class BossThrow : MonoBehaviour
{
    //the spawning point for bullets
    [SerializeField]
    Transform SpawningPointDrawerOne;
    [SerializeField]
    Transform SpawningPointDrawerTwo;
    [SerializeField]
    Transform SpawningPointDrawerBottom;

    
    private GameObject socks;
    private GameObject rightDrawer;
    private GameObject bottomDrawer;

    private Transform player;

    private void Start()
    {
        socks = Resources.Load<GameObject>("Prefab/Boss1/socksObj");
        rightDrawer = Resources.Load<GameObject>("Prefab/Boss1/drawerOBJ");
        bottomDrawer = Resources.Load<GameObject>("Prefab/Boss1/bottomDrawer");
        player= GameObject.FindWithTag("Player").transform;
    }
    //function used for the first boss first throw
    public void throwOne()
    {
        if(socks && SpawningPointDrawerOne)
        {
            GameObject tmpThrowObject= Instantiate(socks,SpawningPointDrawerOne.position,SpawningPointDrawerOne.rotation);
            SocksLogic socktmp = tmpThrowObject.GetComponent<SocksLogic>();
            if(socktmp)
            {
                socktmp.boss=this.transform;
                socktmp.setPower(-2000.0f);
            }
        }
        
    }
    //function used for the first boss second throw
    public void throwTwo()
    {
        if(socks && SpawningPointDrawerTwo)
        {
            GameObject tmpThrowObject= Instantiate(rightDrawer,SpawningPointDrawerTwo.position,SpawningPointDrawerTwo.rotation);
            SocksLogic socktmp = tmpThrowObject.GetComponent<SocksLogic>();
            if(socktmp)
            {
                socktmp.boss=this.transform;
                float power=-300.0f;
                //300 middle 100 close 500 bottom screen
                if(Vector2.Distance(this.transform.position,player.position) <= 15.0f)
                {
                    power = -100.0f;
                }
                else if(Vector2.Distance(this.transform.position,player.position) <= 25.0f)
                {
                    power = -300.0f;
                }
                else if(Vector2.Distance(this.transform.position,player.position) <= 35.0f)
                {
                    power = -500.0f;
                }
                
                socktmp.setPower(power,1000.0F);
            }
        }
        
    }

    //function used for the first boss last throw
    public void throwBottom()
    {
        if(socks && SpawningPointDrawerBottom)
        {
            GameObject tmpThrowObject= Instantiate(bottomDrawer,SpawningPointDrawerBottom.position,SpawningPointDrawerBottom.rotation);
            SocksLogic socktmp = tmpThrowObject.GetComponent<SocksLogic>();
            if(socktmp)
            {
                socktmp.boss=this.transform;
                socktmp.setPower(-3500.0f);
            }
        }
        
    }
}
