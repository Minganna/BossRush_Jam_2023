using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StrawFollow : MonoBehaviour
{
  public Transform strawPivot;
  public PlayerMovements pm;
    //change pivot point to idle one
  public void changeStrawPosOne()
  {
    strawPivot.localPosition = new Vector3(0.1849f, 0.052f, 0.0f);
  }
  //change pivot point to idle two
  public void changeStrawPosTwo()
  {
    strawPivot.localPosition = new Vector3(0.1849f, 0.0458f, 0.0f);
  }
    //change pivot point to idle two
  public void changeStrawPosTree()
  {
    strawPivot.localPosition = new Vector3(0.1888f, 0.0468f, 0.0f);
  }
  
  public void changeStrawWalkingOne()
  {
    strawPivot.localPosition = new Vector3(0.196f, 0.0447f, 0.0f);
  }


  public void changeStrawWalkingTwo()
  {
    strawPivot.localPosition = new Vector3(0.1848f, 0.0455f, 0.0f);
  }

  public void changeStrawWalkingTree()
  {
    strawPivot.localPosition = new Vector3(0.1928f, 0.0295f, 0.0f);
  }
  
  public void changeStrawWalkingFour()
  {
    strawPivot.localPosition = new Vector3(0.1824f, 0.0335f, 0.0f);
  }

  public void changeStrawWalkingFive()
  {
    strawPivot.localPosition = new Vector3(0.1672f, 0.0335f, 0.0f);
  }

  public void changeStrawJumpingOne()
  {
    strawPivot.localPosition = new Vector3(0.19f, 0.023f, 0.0f);
  }

  public void CrouchOne()
  {
    strawPivot.localPosition = new Vector3(0.1898f, 0.041f, 0.0f);
  }

  public void CrouchTwo()
  {
    strawPivot.localPosition = new Vector3(0.2001f, 0.0228f, 0.0f);
  }

  public void CrouchTree()
  {
    strawPivot.localPosition = new Vector3(0.2071f, 0.0037f, 0.0f);
  }


  public void CrouchCompleted()
  {
    strawPivot.localPosition = new Vector3(0.2166f, -0.0859f, 0.0f);
    pm.crouchingNoWalk = false;
  }
  

}
