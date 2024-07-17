using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class PickUpItem : MonoBehaviour
{
    #region Variables
    private bool CanInteract;
    public enum ItemType {Chest,Item}
    public ItemType Type;

    [Header("Chest")]
    public float Tier;
    private bool Opened;
    public bool Locked;
    public string RequiredItem;

    [Header("Item")]
    public string ItemName;

    [Header("Messages")]
    public GameObject[] Message;

    [Header("Texts")]
    public TextMeshProUGUI NeedText;
    public TextMeshProUGUI TookText;
    public TextMeshProUGUI UsedText;

    [Header("References")]
    private Animator animator;
    private InventoryController IC;
    private PlayerMovement PM;
    private InputManager IM;
    private BoxCollider2D BC2D;
    private Animator CanvasAnimator;
    #endregion

    // Start is called before the first frame update
    void Start()
    {
        IM = GameObject.Find("/MaxPrefab/GameScripts").GetComponent<InputManager>();
        IC = GameObject.Find("/MaxPrefab/Player").GetComponent<InventoryController>();
        PM = GameObject.Find("/MaxPrefab/Player").GetComponent<PlayerMovement>();
        CanvasAnimator = GameObject.Find("/MaxPrefab/Canvas").GetComponent<Animator>();
        BC2D = GetComponent<BoxCollider2D>();

        if (Type == ItemType.Chest) 
        { 
            animator = GetComponent<Animator>();
            animator.SetFloat("Chest", Tier);
        }
    }

    #region On Triggers
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player" && Type == ItemType.Item)
        {
            Message[0].SetActive(true);
            CanInteract = true;
        }
        else if (collision.tag == "Player" && Type == ItemType.Chest && Locked == true)
        {
            Message[1].SetActive(true);
            CanInteract = true;
        }
        else if (collision.tag == "Player" && Type == ItemType.Chest && Locked == false)
        {
            Message[2].SetActive(true);
            CanInteract = true;
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.tag == "Player" && Type == ItemType.Item)
        {
            Message[0].SetActive(true);
            CanInteract = true;
        }
        else if (collision.tag == "Player" && Type == ItemType.Chest && Locked == true)
        {
            Message[1].SetActive(true);
            CanInteract = true;
        }
        else if (collision.tag == "Player" && Type == ItemType.Chest && Locked == false)
        {
            Message[2].SetActive(true);
            CanInteract = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            Message[0].SetActive(false);
            Message[1].SetActive(false);
            Message[2].SetActive(false);
            CanInteract = false;
        }
    }
    #endregion

    // Update is called once per frame
    void Update()
    {
        if (CanInteract == true)
        {
            if (Input.GetKeyDown(IM.Interaction) && Type == ItemType.Item)
            {
                if (IC.SlotAvailable > 0)
                {
                    TakeItem();
                }
                else
                {
                    CanvasAnimator.SetTrigger("InventoryFull");
                }
            }
            else if (Input.GetKeyDown(IM.Interaction) && Type == ItemType.Chest && Locked == true)
            {
                UnlockChest();
            }
            else if (Input.GetKeyDown(IM.Interaction) && Type == ItemType.Chest && Locked == false)
            {
                LootChest();
            }
        }
    }

    void TakeItem()
    {
        IC.AddItem(ItemName);
        TookText.text = ItemName;
        CanvasAnimator.SetTrigger("Took");
        Message[0].SetActive(false);
        this.gameObject.SetActive(false);
    }

    void UnlockChest()
    {
        IC.CheckForItem(RequiredItem);
        
        if(IC.Check == true)
        {
            IC.RemoveItem(RequiredItem);
            CanvasAnimator.SetTrigger("Used");
            UsedText.text = RequiredItem;
            Locked = false;
            Message[1].SetActive(false);
        }
        else
        {
            CanvasAnimator.SetTrigger("Need");
            NeedText.text = RequiredItem;
        }
    }

    void LootChest()
    {
        animator.SetTrigger("Open");
        IC.AddItem(ItemName);
        TookText.text = ItemName;
        CanvasAnimator.SetTrigger("Took");
        Message[2].SetActive(false);
        BC2D.enabled = false;
    }
}
