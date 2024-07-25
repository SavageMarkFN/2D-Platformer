using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trap : MonoBehaviour
{
    #region Variables
    [Header("Variables")]
    public bool PhysicalDamage;
    public float Damage;
    private bool DealDamage = true;

    [Header("References")]
    private PlayerMovement PM;
    #endregion

    // Start is called before the first frame update
    void Start()
    {
        PM = GameObject.Find("/MaxPrefab/Player").GetComponent<PlayerMovement>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player" && DealDamage == true)
        {
            DealDamage = false;
            PM.TakeDamage(Damage, PhysicalDamage);
            StartCoroutine(DamageReset());
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.tag == "Player" && DealDamage == true)
        {
            DealDamage = false;
            PM.TakeDamage(Damage, PhysicalDamage);
            StartCoroutine(DamageReset());
        }
    }

    IEnumerator DamageReset()
    {
        yield return new WaitForSeconds(0.2f);
        DealDamage = true;
    }
}
