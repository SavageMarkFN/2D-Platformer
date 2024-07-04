using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEditor;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    //Privates
    [SerializeField] public float horizontalMove = 0f;
    [SerializeField] private float verticalMove = 0f;
    [SerializeField] private bool CanIncrease;
    [SerializeField] private bool InAction;

    //Player Variables
    public bool Invisible;
    public float IncreaseLerp;
    public float ReduceLerp;
    public float Stamina = 100f;
    public float Health;
    public float Mana;
    public int Gold;

    //WeaponVariables
    public bool HasWeapon;
    public string ActivatedSword;
    public bool Wado;       
    public bool Enma;       
    public bool Kitetsu;

    //Publics
    public bool PlayerFreeze = true;
    public float Speed = 4f;
    public bool Jump = false;
    public bool Crouch = false;
    public bool Death;

    //Stamina Variables
    public float StaminaRecude;
    public float StaminaRegent;

    //Dash Variables
    public float Dash;
    public float DashSpeed;
    public float DashTimer;

    //Slide Variables
    public float Slide;
    public float SlideSpeed;
    public float SlideTimer;

    //AttackVariables
    public float Attack;
    public float AttackSpeed;
    public float Damage;

    //Scripts
    public CharacterController characterController;
    public AnimController animController;

    //Animators

    private void Start()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void Update()
    {
        #region Player Inputs
        if (PlayerFreeze == false)
        {
            horizontalMove = Input.GetAxisRaw("Horizontal") * Speed;

            //if (Input.GetKeyUp(KeyCode.W) || Input.GetKeyUp(KeyCode.S))
            //{
                //horizontalMove = 0;
            //}

            if (Jump == false && Dash == 0 && Slide == 0 && Attack == 0 && Stamina >= 15 && InAction == false)
            {
                if (Input.GetKeyDown(KeyCode.Space) && Speed > 0)
                {
                    InAction = true;
                    Jump = true;
                    StartCoroutine(StaminaReduce());
                }

                if (Input.GetKeyDown(KeyCode.LeftShift) && Speed > 0)
                {
                    InAction = true;
                    Dash = Input.GetAxisRaw("Horizontal") * DashSpeed;
                    StartCoroutine(StaminaReduce());
                }

                if (Input.GetKeyDown(KeyCode.LeftControl) && Speed > 0)
                {
                    InAction = true;
                    Slide = Input.GetAxisRaw("Horizontal") * SlideSpeed;
                    StartCoroutine(StaminaReduce());
                }

                if (Input.GetMouseButtonDown(0) && horizontalMove != 0 && HasWeapon == true && InAction == false)
                {
                    InAction = true;
                    Attack = Input.GetAxisRaw("Horizontal") * AttackSpeed;
                    //animController.NormalAttackCall();
                    StartCoroutine(StaminaReduce());
                }

                if (Input.GetMouseButtonDown(1) && horizontalMove != 0 && HasWeapon == true && InAction == false)
                {
                    InAction = true;
                    Attack = (Input.GetAxisRaw("Horizontal") * AttackSpeed) * 2;
                    //animController.HeavyAttackCall();
                    StartCoroutine(StaminaReduce());
                }
            }
        }
        #endregion

        #region Stamina System
        //Stamina Increase
        if (InAction == false)
        {
            if (Stamina < 100 && CanIncrease == true)
            {
                StartCoroutine(StaminaIncrease());
            }
        }
        else
        {
            StopCoroutine(StaminaIncrease());
        }

        if (Stamina > 100f)
        {
            Stamina = 100f;
        }
        #endregion

        if (Health <= 0)
        {
            Death = true;
            PlayerFreeze = true;
        }
    }

    #region Lerp Increase
    IEnumerator StaminaReduce()
    {
        CanIncrease = false;
        float Timer = 0f;
        float StaminaEndValue = Stamina - StaminaRecude;
        while (Timer < ReduceLerp)
        {
            Timer += Time.deltaTime;
            float Step = Timer / ReduceLerp;

            Stamina = Mathf.Lerp(Stamina, StaminaEndValue, Step);

            yield return null;
        }
        CanIncrease = true;
        InAction = false;
    }

    IEnumerator StaminaIncrease()
    {
        CanIncrease = false;
        float Timer = 0f;
        float StaminaEndValue = Stamina + StaminaRegent;
        while (Timer < IncreaseLerp)
        {
            Timer += Time.deltaTime;
            float Step = Timer / IncreaseLerp;

            Stamina = Mathf.Lerp(Stamina, StaminaEndValue, Step);

            yield return null;
        }
        CanIncrease = true;
    }
    #endregion

    private void FixedUpdate()
    {
        if (PlayerFreeze == false)
        {
            characterController.Move(horizontalMove * Time.fixedDeltaTime, Crouch, Jump);
        }

        if (Dash != 0)
        {
            PlayerFreeze = true;
            characterController.Move(Dash * Time.fixedDeltaTime, Crouch, Jump);
            StartCoroutine(Countdown(DashTimer));
        }

        if (Slide != 0)
        {
            PlayerFreeze = true;
            characterController.Move(Slide * Time.fixedDeltaTime, Crouch, Jump);
            StartCoroutine(Countdown(SlideTimer));
        }

        if (Attack != 0)
        {
            PlayerFreeze = true;
            characterController.Move(Attack, Crouch, Jump);
            StartCoroutine(Countdown(AttackSpeed));
        }
    }

    IEnumerator Countdown(float Timer)
    {
        yield return new WaitForSeconds(Timer);
        Dash = 0f;
        Slide = 0f;
        Attack = 0f;
        PlayerFreeze = false;
    }

    public void JumpCheckFunction()
    {
        Jump = false;
    }

    #region HealAndManaRegend
    public void Regend(int Amount, bool Heal)
    {
        StartCoroutine(StatsRegend(Amount, Heal));
    }

    IEnumerator StatsRegend(int Amount, bool Heal)
    {
        float Duration = 0.3f;
        float Timer = 0;
        float NewHealth = Health + Amount;
        float NewMana = Mana + Amount;
        while (Timer < Duration)
        {
            Timer += Time.deltaTime;
            float Step = Timer / Duration;

            if (Heal == true)
            {
                Health = Mathf.Lerp(Health, NewHealth, Step);
                if (Health > 100)
                {
                    Health = 100;
                }
            }
            else
            {
                Mana = Mathf.Lerp(Mana, NewMana, Step);
                if (Mana > 100)
                {
                    Mana = 100;
                }
            }

            yield return null;
        }
    }
    #endregion

    public void TakeItem(string Item)
    {

    }
}
