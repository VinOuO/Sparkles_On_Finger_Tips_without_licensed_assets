using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HR_Imformation;
using Management;
using MLNet;

public class Cast : MonoBehaviour
{
    Spell_Manager SpM;
    Spell_Case SplC;
    void Start()
    {
        SplC = GameObject.Find("Spell_Case").GetComponent<Spell_Case>();
        SpM = GameObject.Find("Spell_Manager").GetComponent<Spell_Manager>();
        StartCoroutine(Swipping_Spells());
    }
    bool Is_Casting = false;
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            Is_Casting = !Is_Casting;
        }
        if (!Cheaking_Casting_Spell && Is_Casting)
        {
            if (HR.Check_Hand_State(Hand_State.Fist))
            {
                Cheaking_Casting_Spell = true;
                StartCoroutine(Casting_Spell());
            }
        }

    }

    bool Is_Swipping = false;
    //float Last_Two_Time = 0;
    float Last_Two_Posx;
    Coroutine Crt;
    IEnumerator Swipping_Spells()
    {
        while (true)
        {
            if (Is_Casting)
            {
                if (HR.Check_Hand_State(Hand_State.SoftTwo))
                {
                    if (!Is_Swipping)
                    {
                        Is_Swipping = true;
                        Last_Two_Posx = HR.Middle_Nodes[3].Screen_Pos.x;
                    }
                    yield return new WaitForSeconds(0.1f);
                    if (Is_Swipping)
                    {
                        SplC.Set_Scroll_Speed((Last_Two_Posx - HR.Middle_Nodes[3].Screen_Pos.x) * -0.02f);
                    }
                }
                else
                {
                    Is_Swipping = false;
                }
            }
            yield return new WaitForEndOfFrame();
        }
    }
    public int Num = 0;
    public bool Cheaking_Casting_Spell = false;
    IEnumerator Casting_Spell()
    {
        Num++;
        float _Start_Time = Time.realtimeSinceStartup;
        while (Time.realtimeSinceStartup - _Start_Time < 2f)
        {
            if (HR.Check_Hand_State(Hand_State.SoftFive))
            {
                //Debug.Log("Cast: " + SplC.Selected_Spell.GetComponent<Spell>().Index);
                if(SplC.Selected_Spell != null)
                {
                    if(SpM == null)
                    {
                        SpM = GameObject.Find("Spell_Manager").GetComponent<Spell_Manager>();
                    }
                    Vector2 _Pos = HR.Get_Hand_Pos();
                    SpM.Cast_Spell(SplC.Selected_Spell.GetComponent<Spell>().Index, Camera.main.ScreenToWorldPoint(new Vector3(_Pos.x, _Pos.y, 24 - Camera.main.transform.position.z)));
                    SplC.Remove_from_Spell_Case(SplC.Selected_Spell);
                }
                break;
            }
            yield return new WaitForEndOfFrame();
        }
        Cheaking_Casting_Spell = false;
        Num--;
    }
}
