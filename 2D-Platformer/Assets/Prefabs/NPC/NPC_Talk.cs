using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class NPC_Talk : MonoBehaviour
{
    #region Variables
    [Header("Variables")]
    public Sprite NPCIcon;
    private bool CanInteract;
    private bool Talking;
    private GameObject[] Messages;

    [Header("References")]
    private PlayerMovement PM;
    private InputManager IM;
    private AIMove aiMove;

    [Header("UI")]
    public GameObject ChattingCanvas;
    public Image NPCFrame;
    public Image PlayerFrame;
    public GameObject[] NPCLine;
    public GameObject PlayerLine;
    #endregion
    // Start is called before the first frame update
    void Start()
    {
        aiMove = GetComponent<AIMove>();
        PM = GameObject.Find("/MaxPrefab/Player").GetComponent<PlayerMovement>();
        IM = GameObject.Find("/MaxPrefab/GameScripts").GetComponent<InputManager>();
        Messages = GameObject.Find("/MaxPrefab/GameScripts").GetComponent<UIController>().UIMessages;
        NPCFrame.sprite = NPCIcon;
        PlayerFrame.sprite = PM.PlayerIcon;
    }

    #region On Triggers
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            CanInteract = true;
            Messages[8].SetActive(true);
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.tag == "Player" && Talking == false)
        {
            CanInteract = true;
            Messages[8].SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            CanInteract = false;
            Messages[8].SetActive(false);
        }
    }
    #endregion

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(IM.Interaction) && CanInteract == true)
        {
            ResetChatting();
            OpenChat();
        }

        if (Input.GetKeyDown(KeyCode.Escape) && Talking == true)
        {
            CloseChat();
            ResetChatting();
        }
    }

    #region Open and Close Chat
    public void OpenChat()
    {
        CanInteract = false;
        Messages[8].SetActive(false);
        ChattingCanvas.SetActive(true);
        Talking = true;
        PM.PlayerFreeze = true;
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        if (aiMove.Speed > 0)
        {
            aiMove.animator.SetFloat("Movement", 0);
            aiMove.AIFreeze = true;
        }
    }

    public void CloseChat()
    {
        Talking = false;
        PM.PlayerFreeze = false;
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        if (aiMove.Speed > 0) 
        {
            aiMove.animator.SetFloat("Movement", 1);
            aiMove.AIFreeze = false;
        }
        ChattingCanvas.SetActive(false);
    }
    #endregion

    #region Chat with NPC
    public void Chatting(int Number)
    {
        PlayerFrame.color = Color.grey;
        NPCFrame.color = Color.white;

        PlayerLine.SetActive(false);

        NPCLine[Number].SetActive(true);
    }

    public void ResetChatting()
    {
        PlayerFrame.color = Color.white;
        NPCFrame.color = Color.grey;

        PlayerLine.SetActive(true);

        for (int i = 0; i < NPCLine.Length; i++) 
        {
            NPCLine[i].SetActive(false);
        }
    }
    #endregion
}
