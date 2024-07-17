using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Cursor = UnityEngine.Cursor;

public class UIController : MonoBehaviour
{
    #region Variables
    private bool InUI;
    private bool InInventory;

    [Header("Reference")]
    private PlayerMovement PM;
    private Animator CanvasAnimator;
    private InputManager IM;
    #endregion

    private void Start()
    {
        PM = GameObject.Find("/MaxPrefab/Player").GetComponent<PlayerMovement>();
        CanvasAnimator = GameObject.Find("/MaxPrefab/Canvas").GetComponent<Animator>();
        IM = GetComponent<InputManager>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(IM.InventoryKey) && InUI == false)
        {
            OpenCloseInventory();
        }
        else if (Input.GetKeyDown(IM.InventoryKey) && InInventory == true)
        {
            OpenCloseInventory();
        }
    }

    #region Open and Close Inventory
    public void OpenCloseInventory()
    {
        if (InInventory == false)
        {
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
            CanvasAnimator.SetTrigger("Inventory");
            InInventory = true;
            InUI = true;
        }
        else
        {
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
            CanvasAnimator.SetTrigger("Inventory");
            InInventory = false;
            InUI = false;
        }
    }
    #endregion
}
