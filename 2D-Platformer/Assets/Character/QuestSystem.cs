using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class QuestSystem : MonoBehaviour
{
    #region Variables
    [Header("For Main Quest System")]
    public TextMeshProUGUI[] MainQuest;
    public GameObject[] MainQuestObject;
    private string[] MainQuestName;
    private bool[] HasMainQuest;
    private int MaxMainQuest;
    public int MainQuestCount;

    [Header("For Side Quest System")]
    public TextMeshProUGUI[] SideQuest;
    public GameObject[] SideQuestObject;
    private string[] SideQuestName;
    private bool[] HasSideQuest;
    private int MaxSideQuest;
    public int SideQuestCount;

    [Header("References")]
    public AudioClip[] Clip;
    public AudioSource Audio;
    private Animator CanvasAnimator;
    #endregion

    void Start()
    {
        MaxMainQuest = MainQuest.Length;
        MainQuestName = new string[MainQuest.Length];
        HasMainQuest = new bool[MainQuest.Length];
        GameObject Canvas = GameObject.Find("/MaxPrefab/Canvas");
        CanvasAnimator = Canvas.GetComponent<Animator>();
    }

    #region Assign Quest
    public void AssignMainQuest(string Name)
    {
        CanvasAnimator.SetTrigger("AddQuest");
        Audio.clip = Clip[0];
        Audio.Play();
        for (int i = 0; i < MainQuest.Length; i++) 
        { 
            if (MainQuestName[i] == null)
            {
                MainQuestObject[i].SetActive(true);
                MainQuest[i].text = Name;
                MainQuestName[i] = Name;
                HasMainQuest[i] = true;
                MainQuestCount += 1;
                Debug.Log(MainQuestName);
                break;
            }
        }
    }
    #endregion

    #region Complete Quest
    public void CompleteMainQuest(string Name)
    {
        CanvasAnimator.SetTrigger("CompleteQuest");
        Audio.clip = Clip[1];
        Audio.Play();

        string[] CurrentMainQuestName = new string[MainQuest.Length];

        for (int i = 0; i < MainQuest.Length; i++) 
        {
            CurrentMainQuestName[i] = MainQuestName[i];
            Debug.Log(MainQuestName[i]);
            MainQuestObject[i].SetActive(false);
            MainQuestName[i] = null;
            HasMainQuest[i] = false;
            MainQuestCount = 0;
        }

        for (int i = 0; i < CurrentMainQuestName.Length; i++) 
        {
            if (CurrentMainQuestName[i] != Name && CurrentMainQuestName[i] != null)
            {
                StockQuest(CurrentMainQuestName[i]);
            }
        }
    }
    #endregion

    #region Stock Quests Again
    void StockQuest(string Name)
    {
        for (int i = 0; i < MainQuest.Length; i++)
        {
            if (MainQuestName[i] == null)
            {
                MainQuestObject[i].SetActive(true);
                MainQuest[i].text = Name;
                MainQuestName[i] = Name;
                HasMainQuest[i] = true;
                MainQuestCount += 1;
                break;
            }
        }
    }
    #endregion
}
