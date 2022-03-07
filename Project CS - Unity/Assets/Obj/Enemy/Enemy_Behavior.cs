using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Behavior : MonoBehaviour
{
    Animator Amt;
    public int Health = 100;


    void Start()
    {
        Amt = GetComponent<Animator>();
    }

    void Update()
    {
        
    }

    public void Hurt(int _Damage)
    {
        Health -= _Damage;
        if(Health <= 0)
        {
            Amt.SetTrigger("Die");
            Amt.SetBool("Dying", true);
        }
    }
}
