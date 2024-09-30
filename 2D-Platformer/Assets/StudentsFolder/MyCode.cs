//Libraries
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Code
public class MyCode : MonoBehaviour
{
    #region Variables
    /*
    Attributes
     private --> Only the script that contains the variable have access to change the value
     public  --> All scripts have access to this variable to change the value

    Variable Types
     int       > Takes all integer numbers
     float     > Takes all the numbers
     bool      > true/false
     string    > "Text"
     GameObject> Variable that contains an object in the scene
     We can enable and disable this object with the ObjectName.SetActive(true/false);
     Transform > Variable that contains the Position,Rotation,Scale
     */
    private int Number = 5;
    public float _Number = -2.5f;
    private bool Check = true;
    public string Item = "Sword";
    public GameObject Chest;
    private Transform NewPosition;
    #endregion

    #region Single if Condition

    #endregion

    #region Multiple If Conditions

    #endregion

    #region Collisions

    #endregion
}
