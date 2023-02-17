using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreMenu : MonoBehaviour
{
    StaticValues sv = new StaticValues();
    [SerializeField]
    TextMeshProUGUI TimeText;
    [SerializeField]
    TextMeshProUGUI patchUpText;
    [SerializeField]
    TextMeshProUGUI damageText;
    [SerializeField]
    TextMeshProUGUI gradeText;
    

    // Start is called before the first frame update
    void Start()
    {
        int damageTaken=sv.getdamageReceived();
        int healingDone=sv.getHealing();
        int minutes= Mathf.RoundToInt((float)sv.getPlayTime());
        string Score="C";

        if(patchUpText)
        {
            patchUpText.text=healingDone.ToString();
        }
        if(damageText)
        {
            damageText.text=damageTaken.ToString();
        }
        if(TimeText)
        {
            TimeText.text=minutes.ToString() +" min";
        }
        if(minutes < 15)
        {
            if(healingDone<=10 &&damageTaken<=10)
            {
                Score="B";
            }
            if(healingDone<=3 &&damageTaken<=3)
            {
                Score="A";
            }
            if(healingDone==0 &&damageTaken==0)
            {
                Score="S";
            }
            if(healingDone>10 &&damageTaken>10)
            {
                Score="C";
            }
            
        }
        else
        {
            Score="C";
        }
        gradeText.text=Score;
        
    }

}
