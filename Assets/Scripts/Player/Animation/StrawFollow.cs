using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StrawFollow : MonoBehaviour
{
    public Transform strawPivot;
    //change pivot point to idle one
  public void changeStrawPosOne()
  {
    strawPivot.localPosition = new Vector3(0.1869f, 0.0479f, 0.0f);
  }
  //change pivot point to idle two
  public void changeStrawPosTwo()
  {
    strawPivot.localPosition = new Vector3(0.171f, 0.056f, 0.0f);
  }

}
