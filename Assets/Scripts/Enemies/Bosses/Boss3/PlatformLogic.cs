using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformLogic : MonoBehaviour
{
    [SerializeField]
    GameObject platformToLower;  
    Vector3 originalPos;
    bool lowerPlatform=false;
    float stopper;
    [SerializeField]
    GameObject topFloor;
    // Start is called before the first frame update
    void Start()
    {
        if(platformToLower)
        {
            originalPos= platformToLower.transform.position;
            stopper = originalPos.y - 12.0f;
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        if(!lowerPlatform && platformToLower.transform.position.y <= originalPos.y)
        {
            if(platformToLower)
            {
                platformToLower.transform.position =new Vector3(platformToLower.transform.position.x,platformToLower.transform.position.y +5.0f *Time.deltaTime,platformToLower.transform.position.z);
            }
        }
        if(lowerPlatform && platformToLower.transform.position.y > stopper)
        {
            if(platformToLower)
            {
                platformToLower.transform.position =new Vector3(platformToLower.transform.position.x,platformToLower.transform.position.y -7.0f *Time.deltaTime,platformToLower.transform.position.z);
            }
        }
    }



    private void OnTriggerEnter2D(Collider2D other) {
        if(other.gameObject.tag=="Player")
        {
            lowerPlatform = true;
            if(topFloor)
            {
                if(this.gameObject.name=="Button1")
                {
                    int LayerToIgnore = LayerMask.NameToLayer("ignorePlayer");
                    topFloor.layer = LayerToIgnore;
                }
                else
                {
                    int LayerNotToIGnore = LayerMask.NameToLayer("Enviroment");
                    topFloor.layer = LayerNotToIGnore;
                }
            }

        }
    }

    private void OnTriggerExit2D(Collider2D other) 
    {
        if(other.gameObject.tag=="Player")
        {
            lowerPlatform = false;
        }
    }
}
