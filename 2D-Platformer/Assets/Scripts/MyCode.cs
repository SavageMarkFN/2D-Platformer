//Libraries
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Code
public class MyCode : MonoBehaviour
{
    #region Variables
    //Attributes
    //private -->Other scripts dont have access
    //public  -->Other scripts have access

    //Types
    //int --> (..-3, -2, -1, 0, 1, 2, 3..)
    //float --> (..-2.3, -1, 0, 1.5, 3..)
    //bool --> (true/false)
    //string --> "Text"
    //GameObject --> One object in the scene
    //Transform --> Position,Rotation,Scale

    //Vector2 --> Contains x , y --> Vector2(x,y)

    public int Number = 15;
    private float Health = 85.5f;
    public bool Check = false;
    private string Name = "Unknown";
    public GameObject Player;
    private Transform PlayerT;
    public Vector2 Location; // = new Vector2(Number x,Number y);

    //Array --> One list with the same variable type
    public bool[] Full; // = new bool[Array Number];
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
