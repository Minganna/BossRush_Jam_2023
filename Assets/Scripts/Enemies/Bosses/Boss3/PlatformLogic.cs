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
    // Start is called before the first frame update
    void Start()
    {
        if(platformToLower)
        {
            originalPos= platformToLower.transform.position;
            stopper = originalPos.y - 8.0f;
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
                platformToLower.transform.position =new Vector3(platformToLower.transform.position.x,platformToLower.transform.position.y -5.0f *Time.deltaTime,platformToLower.transform.position.z);
            }
        }
    }



    private void OnTriggerEnter2D(Collider2D other) {
        if(other.gameObject.tag=="Player")
        {
            lowerPlatform = true;
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
