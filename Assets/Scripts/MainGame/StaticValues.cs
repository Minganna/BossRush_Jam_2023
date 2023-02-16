using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StaticValues
{
    static int sceneToLoad;
    static int damageReceived;
    static int numbOfHealing;

    public void setSceneToLoad(int scene)
    {
        sceneToLoad = scene;
    }

    public int getSceneToLoad()
    {
        return sceneToLoad;
    }
}
