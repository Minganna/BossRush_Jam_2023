using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StrawFollow : MonoBehaviour
{
    public Transform strawPivot;
    //change pivot point to idle one
  public void changeStrawPosOne()
  {
    strawPivot.localPosition = new Vector3(0.1888F, 0.0553f, 0.0f);
  }
  //change pivot point to idle two
  public void changeStrawPosTwo()
  {
    strawPivot.localPosition = new Vector3(0.1844f, 0.0508f, 0.0f);
  }
    //change pivot point to idle two
  public void changeStrawPosTree()
  {
    strawPivot.localPosition = new Vector3(0.1921f, 0.0491f, 0.0f);
  }
  
  public void changeStrawWalkingOne()
  {
    strawPivot.localPosition = new Vector3(0.1816f, 0.054f, 0.0f);
  }


  public void changeStrawWalkingTwo()
  {
    strawPivot.localPosition = new Vector3(0.1893f, 0.0411f, 0.0f);
  }

  public void changeStrawWalkingTree()
  {
    strawPivot.localPosition = new Vector3(0.1832f, 0.0411f, 0.0f);
  }
  
  public void changeStrawWalkingFour()
  {
    strawPivot.localPosition = new Vector3(0.179f, 0.0431f, 0.0f);
  }

  public void changeStrawWalkingFive()
  {
    strawPivot.localPosition = new Vector3(0.1706f, 0.0431f, 0.0f);
  }

}
