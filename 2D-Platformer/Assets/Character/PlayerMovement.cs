using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerMovement : MonoBehaviour
{
    #region Variables
    public Sprite PlayerIcon;
    [HideInInspector] public float horizontalMove = 0f;
    private float verticalMove = 0f;
    private bool CanIncrease;
    [HideInInspector] public bool InLadder;
    private bool IsClimbing;
    public bool InAction;

    [Header("Player")]
    public float Speed;
    public float ClimbingSpeed;
    private float OriginalSpeed;
    public int Gold;
    public float Armor;
    public float MagicResist;
    [HideInInspector] public bool PlayerFreeze;
    [HideInInspector] public bool Jump;
    [HideInInspector] public bool Crouch;
    [HideInInspector] public bool Invisible;
    [HideInInspector] public bool Death;

    [Header("Health")]
    public float Health;
    public float MaxHealth;

    [Header("Mana")]
    public float Mana;
    public float MaxMana;

    [Header("Stamina")]
    public float Stamina;
    public float MaxStamina;
    public float StaminaRegent;
    public float IncreaseDuration;

    [Header("Dash")]
    public bool CanDash;
    public float DashSpeed;
    public float DashTimer;
    [HideInInspector] public float Dash;

    [Header("Slide")]
    public bool CanSlide;
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
    private InputManager IM;
    [HideInInspector] public AnimController animController;

    [Header("UI")]
    public Slider HealthSlider;
    public Slider ManaSlider;
    public Slider StaminaSlider;
    public TextMeshProUGUI[] UIText;
    #endregion

    private void Start()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        cController = GetComponent<CharacterController>();
        IM = GameObject.Find("/MaxPrefab/GameScripts").GetComponent<InputManager>();
        OriginalSpeed = Speed;
    }

    private void Update()
    {
        if (PlayerFreeze == false && Health > 0)
        {
            #region Player Inputs
            if (PlayerFreeze == false)
            {
                horizontalMove = Input.GetAxisRaw("Horizontal") * Speed;
                verticalMove = Input.GetAxisRaw("Vertical") * ClimbingSpeed;

                if (InAction == false)
                {
                    if (Input.GetKeyDown(IM.Jump) && Stamina >= 15)
                    {
                        InAction = true;
                        animController.animator.SetTrigger("Jump");
                        animController.animator.SetBool("InAir", true);
                        Jump = true;
                        Stamina -= 15f;
                    }

                    if (Input.GetKeyDown(IM.Dash) && Stamina >= 15 && CanDash == true)
                    {
                        Dash = Input.GetAxisRaw("Horizontal") * DashSpeed;
                        Stamina -= 15f;
                        StartCoroutine(DashReset());
                    }

                    if (Input.GetKeyDown(IM.Slide) && Stamina >= 15 && CanSlide == true)
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

                if (InLadder && Mathf.Abs(verticalMove) > 0f)
                {
                    IsClimbing = true;
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
                StopCoroutine(StaminaIncrease());
            }

            if (Stamina > 100f)
            {
                Stamina = 100f;
            }
            #endregion

            #region Assing UI
            HealthSlider.maxValue = MaxHealth;
            HealthSlider.value = Health;
            ManaSlider.maxValue = MaxMana;
            ManaSlider.value = Mana;
            StaminaSlider.maxValue = MaxStamina;
            StaminaSlider.value = Stamina;
            #endregion
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

        if (IsClimbing == true)
        {
            cController.m_Rigidbody2D.gravityScale = 1f;
            cController.m_Rigidbody2D.velocity = new Vector2(cController.m_Rigidbody2D.velocity.x, verticalMove * ClimbingSpeed);
        }
        else
        {
            cController.m_Rigidbody2D.gravityScale = 6f;
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
            if(InAction == false)
            {
                Timer += Time.deltaTime;
                float Step = Timer / IncreaseDuration;

                Stamina = Mathf.Lerp(Stamina, StaminaEndValue, Step);

                yield return null;
            }
            else
            {
                Timer = IncreaseDuration;
            }
        }
        CanIncrease = true;
    }
    #endregion

    #region HealAndManaRegend
    public IEnumerator StatsRegend(int Amount, bool Heal)
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
                if (Health > MaxHealth)
                {
                    Health = MaxHealth;
                    Timer = Duration;
                }
            }
            else
            {
                Mana = Mathf.Lerp(Mana, NewMana, Step);
                if (Mana > MaxMana)
                {
                    Mana = MaxMana;
                    Timer = Duration;
                }
            }

            yield return null;
        }
    }
    #endregion

    #region Take Damage
    public void TakeDamage(float Value, bool PhysicalDamage)
    {
        animController.animator.SetTrigger("Hit");

        if (PhysicalDamage == false)
        {
            float NewValue = Value - Armor;
            if (NewValue > 0)
            {
                Health -= NewValue;
            }
        }
        else
        {
            float NewValue = Value - MagicResist;
            if (NewValue > 0)
            {
                Health -= NewValue;
            }
        }

        if (Health <= 0)
        {
            Death = true;
            PlayerFreeze = true;
            animController.animator.SetTrigger("Death");
            Speed = 0;
        }
    }
    #endregion

    #region On Triggers
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Ladder"))
        {
            InLadder = true;
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Ladder"))
        {
            InLadder = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Ladder"))
        {
            InLadder = false;
            IsClimbing = false;
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
