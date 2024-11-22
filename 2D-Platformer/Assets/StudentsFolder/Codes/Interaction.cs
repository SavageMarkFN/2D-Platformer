using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interaction : MonoBehaviour
{
    #region Variables
    public enum InteractionType {Item,Chest,Door,Gather}
    public InteractionType Type;

    [Header("Interaction")]
    private bool CanInteract;
    public GameObject[] Message;

    [Header("Type = Item")]
    public string Item;

    [Header("Type = Chest,Door")]
    public bool Locked;
    public string Key;

    [Header("References")]
    private InventoryController IC;
    private Animator animator;
    #endregion

    void Start()
    {
        IC = GameObject.Find("/MaxPrefab/Player").GetComponent<InventoryController>();
    
        if (Type == InteractionType.Chest || Type == InteractionType.Door)
            animator = GetComponent<Animator>();
    }

    #region Interaction
    void OnTriggerEnter2D(Collider2D Object)
    {
        if (Object.tag == "Player")
        {
            CanInteract = true;

            if (Type == InteractionType.Item)
                Message[0].SetActive(true);
            else if (Type == InteractionType.Gather)
                Message[1].SetActive(true);
            else if (Locked == true)
                Message[2].SetActive(true);
            else if (Locked == false)
                Message[3].SetActive(true);
        }
    }

    void OnTriggerExit2D(Collider2D Object)
    {
        if (Object.tag == "Player")
        {
            CanInteract = false;
            for (int i = 0; i < Message.Length; i++)
                Message[i].SetActive(false);
        }
    }
    #endregion

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && CanInteract == true)
        {
            CanInteract = false;
            for (int i = 0; i < Message.Length; i++)
                Message[i].SetActive(false);

            if (Type == InteractionType.Item || Type == InteractionType.Gather)
            {
                IC.AddItem(Item);
                this.gameObject.SetActive(false);
            }
            else
            {
                ChestOrDoor();
            }
        }
    }

    void ChestOrDoor()
    {
        if (Locked == false)
        {
            animator.SetTrigger("Open");
            IC.AddItem(Item);
        }
        else
        {
            IC.CheckForItem(Key);
            if (IC.Check == true)
            {
                IC.RemoveItem(Key);
                Locked = false; 
            }            
        }
    }
}
