using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeScreenOrientation : MonoBehaviour
{
    public void ChangeOrientation()
    {
       if (Screen.orientation == ScreenOrientation.Portrait)
       {
           Screen.orientation = ScreenOrientation.LandscapeLeft;
       }
       else
       {
           Screen.orientation = ScreenOrientation.Portrait;
       }
       Debug.Log("Orientation: " + Screen.orientation);
    }
}
