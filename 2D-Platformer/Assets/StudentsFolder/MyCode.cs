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
    Equal (==)
    Higher(>)
    Lower (<)
    Higher or equal (>=)
    Lower or equal  (<=)
    Not Equal (!=)
    */

    //Example
    void Example()
    {
        int Number = 10;

        if (Number > 10)
            Debug.Log("Higher than 10");
        else if (Number > 0)
            Debug.Log("Lower than 10");
        else
            Debug.Log("Negative Number");
/////////////////////////////////////////////////////////////////////////////////
        bool AttackPlayer = false;
        float Distance = 15;

        if (Distance < 20)
            AttackPlayer = true;
        else
            AttackPlayer = false;
    }
    #endregion

    #region Multiple If
    // (&& And)
    // (|| Or)

    void MultipleIf()
    {
        float Health = 75;
        string Condition;
        bool Dead = false;

        if (Health > 100)
            Condition = "Very well";
        else if (Health > 50 && Health < 75)
            Condition = "Good";
        else if (Health > 25 && Health < 50)
            Condition = "Okay";
        else if (Health < 25)
            Condition = "Bad";

        if (Health <= 0 && Dead == false)
            Dead = true;
    }
    #endregion

    #region For Loop
    /*
    int i  > How many times i repeat the code
    Length > How many times i need to repeat the code
    i++    > Increaser
    break; > Stops the for
    */

    public void ForLoopExample()
    {
        int Number = 0;
        int LoopTimes = 10;

        for (int i = 0; i < LoopTimes; i++)
        {
            Number += 5;
        }
//////////////////////////////////////////////////////////////////

        float Health = 0;
        LoopTimes = 35;

        for (int i = 0; i < LoopTimes; i++)
        {
            Health += 2.5f;

            if (Health > 100)
                break;
        }
    }
    #endregion
 
    #region Animator
    Animator animator; //The name of the animator
    void AnimatorController()
    {
        //How to change the variables of an animator
        animator.SetInteger("Number", 10);
        animator.SetFloat("Number", 2.6f);
        animator.SetBool("Open", true);
        animator.SetTrigger("Attack");   //Sets the trigger to true
        animator.ResetTrigger("Attack"); //Sets the trigger to false

        //How to get variables from the animator
        int Number = animator.GetInteger("Number");
        float State = animator.GetFloat("State");
        bool Open = animator.GetBool("Open");

        //How to change animator's speed
        animator.speed = 1;

        //How to enable/disable the animator
        animator.enabled = false; //true > enables the animator
    }
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

/*
Health - Mana Potion
Golden - Silver - Diamon Key
*/
