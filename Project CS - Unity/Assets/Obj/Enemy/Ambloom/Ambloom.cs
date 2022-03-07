using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ambloom : MonoBehaviour
{
    Animator Amt;
    Rigidbody2D Rig;
    void Start()
    {
        Amt = GetComponent<Animator>();
        Rig = GetComponent<Rigidbody2D>();
        StartCoroutine(Walking());
    }

    void Update()
    {
        Update_Anim_State();
        //transform.Translate(Vector3.right * 1 * Time.deltaTime * _Dir);
    }

    void Update_Anim_State()
    {
        Amt.SetBool("Is_Walking", Mathf.Abs(Rig.velocity.x) > 0.05f ? true : false);
        Vector3 _Temp = transform.localScale;
        if(_Temp.x > 0 && Rig.velocity.x > 0 || _Temp.x < 0 && Rig.velocity.x < 0)
        {
            _Temp.x *= -1;
        }
        transform.localScale = _Temp;
    }
    
    IEnumerator Walking()
    {
        int _Dir = 1;
        while (true)
        {
            float _Timer = Time.time;
            while (Time.time - _Timer < 3)
            {
                Rig.velocity = Vector2.right * 100 * _Dir * Time.deltaTime;
                yield return new WaitForEndOfFrame();
            }
            _Dir *= -1;
            _Timer = Time.time;
        }
    }

}
