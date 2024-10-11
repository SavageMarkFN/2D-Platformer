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
    public int Number = 10;
    private float Health = 100;
    public bool Check = true;
    private string Text = "Code";
    public GameObject Chest = Chest.SetActive(false);
    private Transform Player;
    #endregion
    
    #region Collision
    private void OnTriggerEnter2D(Collider2D collision)
    {
        //collision.tag == Player
        //Code
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        //collision.tag == Player
        //Code
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        //collision.tag == Player
        //Code
    }
    #endregion
    /*
    Follow Up

    1)OnTrigger Collision
    2)Interaction with update method
    3)Adding the Items into the Player (Check Player's Inspector)
    4)Adding Item into the player inventory
    */
    
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
