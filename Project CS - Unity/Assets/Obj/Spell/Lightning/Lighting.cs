using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lighting : MonoBehaviour
{
    Animator Amt;
    AudioSource AdS;
    float Timer;
    void Start()
    {
        Amt = GetComponent<Animator>();
        AdS = GetComponent<AudioSource>();
        Timer = Time.time;
        StartCoroutine(Lightinging());
    }

    void Update()
    {
        if(Time.time - Timer > 3)
        {
            Destroy(gameObject);
        }
    }

    IEnumerator Lightinging()
    {
        
        while(Amt.GetCurrentAnimatorStateInfo(0).normalizedTime <= 1)
        {
            yield return new WaitForFixedUpdate();
        }
        Amt.SetTrigger("Next_Step");
        while (!Amt.GetCurrentAnimatorStateInfo(0).IsName("Lightning"))
        {
            yield return new WaitForFixedUpdate();
        }
        while (AdS.time < 2.6f)
        {
            yield return new WaitForSeconds(0.1f);
        }
        Amt.SetTrigger("Next_Step");
        while (!Amt.GetCurrentAnimatorStateInfo(0).IsName("Lightning_Ending"))
        {
            yield return new WaitForFixedUpdate();
        }
        SpriteRenderer _SpR = GetComponent<SpriteRenderer>();
        while (Amt.GetCurrentAnimatorStateInfo(0).normalizedTime <= 1)
        {
            Color _Color = _SpR.color;
            _Color.a = 1 - Amt.GetCurrentAnimatorStateInfo(0).normalizedTime;
            _SpR.color = _Color;
            yield return new WaitForFixedUpdate();
        }
        Destroy(gameObject);
    }
}
