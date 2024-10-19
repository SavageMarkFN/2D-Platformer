using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyCode : MonoBehaviour
{
    #region Variables
    /*
    Basic Variables
    int = (-2, -1, 0, 1, 2)
    float = (-3.2, -1. 0, 0.5, 1.4)
    bool = true/false
    string = "Text"

    Unity's Variables
    GameObject = ObjectName.SetActive(true/false);
    Transform = Position, Rotation, Scale

    private = A variable that can be accessed from this script
    public  = A variable that can be accessed from all the scripts
    */

    //Examples
    public int Number = 10;
    private float Health = 100;
    public bool Check = true;
    private string Text = "Code";
    public GameObject Chest;
    private Transform Player;
    #endregion

    #region Single If
    /*
    The if statement allows us to make decisions based on certain condition
    Basics conditions
    Equals to        (==)
    Higher than      (>)
    Lower  than      (<)
    Higher and equal (>=)
    Lower and equal  (<=)
    not equal        (!=)
     */

    //Examples
    void Example()
    {
        int Number = 7;

        if (Number > 10)
            Debug.Log("Number is higher than 10");
        else if (Number < 10)
            Debug.Log("Number is smaller to 10");
        else if (Number > 5)
            Debug.Log("Number is higher than 5");
        else
            Debug.Log("Number is smaller than 5");
/////////////////////////////////////////////////////////////////////////////////////////
        bool AttackPlayer = false;
        float Distance = 0;

        if (Distance >= 15)
            AttackPlayer = false;
        else
            AttackPlayer = true;
    }
    #endregion

    #region Multiple If

    #endregion

    #region For Loop

    #endregion

    #region Animator
    /*
    In order to change a variable inside to an animator we need to Set the variable
    Animator TestAnimator;
    TestAnimator.SetInteger("Number", 10);
    TestAnimator.SetFloat("Number", 1.1);
    TestAnimator.SetBool("Locked", true);

    A Trigger Variable in animator is  a bool that is reset by the controller when consumed by a transition 
    (When you change an animation state with her as a condition)
    TestAnimator.SetTrigger("Open");   //It turns the Open to true
    TestAnimator.ResetTrigger("Open"); //It turns the Open back to false)
     */
    #endregion

    #region Collision

    #endregion

    #region Mouse and Keyboard Inputs

    #endregion

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
