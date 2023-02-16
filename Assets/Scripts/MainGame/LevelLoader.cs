using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class LevelLoader : MonoBehaviour
{
    StaticValues sv = new StaticValues();
    [SerializeField]
    TextMeshProUGUI loadingText;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(LoadAsynchronously());
    }

    IEnumerator LoadAsynchronously()
    {
        yield return new WaitForSeconds(0.3f);
        AsyncOperation operation= SceneManager.LoadSceneAsync(sv.getSceneToLoad());
        string dots=".";
        int numberOfDots=0;
        while(!operation.isDone)
        {
            if(loadingText)
            {
                loadingText.text="Loading "+dots;
            }
            numberOfDots++;
            if(numberOfDots==0)
            {
                dots= ".";
            }
            if(numberOfDots==1)
            {
                dots= ". .";
            }
            if(numberOfDots==3)
            {
                dots= ". . .";
                numberOfDots=0;
            }
            yield return null;
        }
    }

}
