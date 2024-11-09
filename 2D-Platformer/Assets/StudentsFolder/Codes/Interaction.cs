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
    public GameObject Message;

    [Header("Type = Item")]
    public string Item;

    [Header("References")]
    private InventoryController IC;
    #endregion

    void Start()
    {
        IC = GameObject.Find("/MaxPrefab/Player").GetComponent<InventoryController>();
    }

    #region Interaction
    void OnTriggerEnter2D(Collider2D Object)
    {
        if (Object.tag == "Player" && Type == InteractionType.Item)
        {
            CanInteract = true;
            Message.SetActive(true);
        }
    }

    void OnTriggerExit2D(Collider2D Object)
    {
        if (Object.tag == "Player" && Type == InteractionType.Item)
        {
            CanInteract = false;
            Message.SetActive(false);
        }
    }
    #endregion

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && CanInteract == true)
        {
            if (Type == InteractionType.Item)
            {
                IC.AddItem(Item);
                this.gameObject.SetActive(false);
            }
        }
    }
}
