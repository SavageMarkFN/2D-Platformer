using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyCode : MonoBehaviour
{
    #region Basics
    /*
     Attribute private-public
     Variable Type int-float-bool-string-GameObject
     Create a variable-->Attribute + Variable Type + Name + ;
     */
    [Header("Basic Variables")]
    public int Number = 1;
    public float Health = 4.2f;
    private bool Open = true;
    private string Name = "Door";
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
