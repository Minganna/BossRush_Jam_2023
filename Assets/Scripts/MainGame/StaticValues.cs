using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StaticValues
{
    static int sceneToLoad;
    static int damageReceived = 0;
    static int numbOfHealing = 0;
    static double playTime =0.0;
    static bool doTraining=true;

    public void setSceneToLoad(int scene)
    {
        sceneToLoad = scene;
    }

    public int getSceneToLoad()
    {
        return sceneToLoad;
    }

    public void addValueTodamageReceived(int damage)
    {
        if(damage != 0)
        {
            damageReceived += damage;
        }
        else
        {
            damageReceived = damage;
        }
        
    }

    public int getdamageReceived()
    {
        return damageReceived;
    }

    public void addValueToHealing(int heal)
    {
        if(heal != 0)
        {
            numbOfHealing += heal;
        }
        else
        {
             numbOfHealing = heal;
        }
        
    }

    public int getHealing()
    {
        return numbOfHealing;
    }
    
    public void addValueToTime(double time)
    {
        playTime += time;
    }

    public double getPlayTime()
    {
        return playTime;
    }

    public void setDoTraining(bool training)
    {
        doTraining = training;
    }

    public bool getDoTraining()
    {
        return doTraining;
    }
}
