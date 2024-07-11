using System.Collections;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    #region Variables
    [HideInInspector] public float horizontalMove = 0f;
    private float verticalMove = 0f;
    private bool CanIncrease;
    public bool InAction;

    [Header("Player")]
    public float Speed;
    public bool PlayerFreeze = true;
    public bool Jump;
    public bool Crouch;
    [HideInInspector] public bool Invisible;
    [HideInInspector]public bool Death;

    [Header("Stats")]
    public float Health;
    public float Mana;
    public int Gold;

    [Header("Stamina")]
    public float Stamina;
    public float StaminaRegent;
    public float IncreaseDuration;

    [Header("Dash")]
    public float DashSpeed;
    public float DashTimer;
    [HideInInspector] public float Dash;

    [Header("Slide")]
    public float SlideSpeed;
    public float SlideTimer;
    [HideInInspector] public float Slide;

    [Header("Attack")]
    public bool HasWeapon;
    public float AttackSpeed;
    public float Damage;
    [HideInInspector] public float Attack;

    [Header("References")]
    private CharacterController cController;
    [HideInInspector] public AnimController animController;
    #endregion

    private void Start()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        cController = GetComponent<CharacterController>();

    }

    private void Update()
    {
        #region Player Inputs
        if (PlayerFreeze == false)
        {
            horizontalMove = Input.GetAxisRaw("Horizontal") * Speed;
            verticalMove = Input.GetAxisRaw("Vertical") * Speed;

            if (InAction == false)
            {
                if (Input.GetKeyDown(KeyCode.Space))
                {
                    InAction = true;
                    animController.animator.SetTrigger("Jump");
                    animController.animator.SetBool("InAir", true);
                    Jump = true;
                    Stamina -= 15f;
                }

                if (Input.GetKeyDown(KeyCode.LeftShift) && Stamina >= 15)
                {
                    Dash = Input.GetAxisRaw("Horizontal") * DashSpeed;
                    Stamina -= 15f;
                    StartCoroutine(DashReset());
                }

                if (Input.GetKeyDown(KeyCode.LeftControl) && Stamina >= 15)
                {
                    InAction = true;
                    Slide = Input.GetAxisRaw("Horizontal") * SlideSpeed;
                    Stamina -= 15f;
                    StartCoroutine(SlideReset());
                }

                if (Input.GetMouseButtonDown(0) && Stamina >= 30)
                {
                    PlayerFreeze = true;
                    InAction = true;
                    Stamina -= 30f;
                    animController.animator.SetTrigger("Light Attack");
                }

                if (Input.GetMouseButtonDown(1) && Stamina >= 30)
                {
                    PlayerFreeze = true;
                    InAction = true;
                    Stamina -= 30f;
                    animController.animator.SetTrigger("Heavy Attack");
                }
            }
        }
        #endregion

        #region Stamina System
        if (InAction == false)
        {
            if (Stamina < 100 && CanIncrease == true)
            {
                StartCoroutine(StaminaIncrease());
            }
        }
        else
        {
            CanIncrease = true;
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

    #region Move The Player
    private void FixedUpdate()
    {
        if (PlayerFreeze == false)
        {
            cController.Move(horizontalMove * Time.fixedDeltaTime, Crouch, Jump);
        }

        if (Dash != 0)
        {
            PlayerFreeze = true;
            cController.Move(Dash * Time.fixedDeltaTime, Crouch, Jump);
        }

        if (Slide != 0)
        {
            PlayerFreeze = true;
            cController.Move(Slide * Time.fixedDeltaTime, Crouch, Jump);
        }

        if (Attack != 0)
        {
            PlayerFreeze = true;
            cController.Move(Attack, Crouch, Jump);
        }
    }
    #endregion

    #region Resets
    public IEnumerator DashReset()
    {
        CanIncrease = false;
        InAction = true;
        animController.animator.SetTrigger("Dash");
        yield return new WaitForSeconds(DashTimer);
        MovementReset();
    }

    public IEnumerator SlideReset()
    {
        CanIncrease = false;
        InAction = true;
        animController.animator.SetTrigger("Slide");
        yield return new WaitForSeconds(SlideTimer);
        MovementReset();
    }

    public void MovementReset()
    {
        CanIncrease = true;
        animController.animator.SetFloat("State", 0);
        Dash = 0f;
        Slide = 0f;
        InAction = false;
        PlayerFreeze = false;
    }
    #endregion

    #region Stamina Lerp
    IEnumerator StaminaIncrease()
    {
        CanIncrease = false;
        float Timer = 0f;
        float StaminaEndValue = Stamina + StaminaRegent;
        while (Timer < IncreaseDuration)
        {
            Timer += Time.deltaTime;
            float Step = Timer / IncreaseDuration;

            Stamina = Mathf.Lerp(Stamina, StaminaEndValue, Step);

            yield return null;
        }
        CanIncrease = true;
    }
    #endregion

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

    public void JumpCheckFunction()
    {
        Jump = false;
        animController.animator.SetBool("InAir", false);
        MovementReset();
    }
}
