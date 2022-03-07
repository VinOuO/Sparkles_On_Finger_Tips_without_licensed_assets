using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HR_Imformation;

public class Point : MonoBehaviour
{
    public int Index = 0;
    public Hand_Node HN;
    Camera LookingCamera;
    SpriteRenderer SptR;
    void Start()
    {
        SptR = GetComponent<SpriteRenderer>();
        LookingCamera = GameObject.Find("Level_Camera").GetComponent<Camera>();
        //Set_Color_Finger();
    }

    void Update()
    {
        Set_Color_Finger_State();
        HN.Screen_Pos = new Vector2(HR.Pos[HN.Index].x * Screen.width, (-HR.Pos[HN.Index].y + 1) * Screen.height);
        Vector3 _Temp = LookingCamera.ScreenToWorldPoint(new Vector3(HN.Screen_Pos.x, HN.Screen_Pos.y, 0));
        _Temp.z = 0;
        transform.position = _Temp;
        _Temp = transform.position;
        _Temp.z = HR.Pos[HN.Index].z * Screen.width * -1;
        HN.World_Pos = transform.position;
        transform.localScale = Vector3.one * ((HR.Pos[HN.Index].z < 0 ? HR.Pos[HN.Index].z * 0.7f * -10 : 0) + 0.3f);
    }

    bool Is_Showing = false;
    void Set_Color_Finger_State()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            Is_Showing = !Is_Showing;
            if (SptR.color.a != 0)
            {
                Color _Temp = SptR.color;
                _Temp.a = 0;
                SptR.color = _Temp;
                return;
            }
            else
            {
                Color _Temp = SptR.color;
                _Temp.a = 1;
                SptR.color = _Temp;
                return;
            }
        }
        if (Is_Showing)
        {
            switch ((int)HN.Finger_State)
            {
                case 1:
                    SptR.color = Color.red;
                    break;
                case 2:
                    SptR.color = Color.green;
                    break;
                case 3:
                    SptR.color = Color.blue;
                    break;
            }
        }

        if(Is_Showing && HN.Index == 0)
        {
            if (HR.Check_Hand_State(Hand_State.Fist))
            {
                SptR.color = Color.black;
            }
            else if (HR.Check_Hand_State(Hand_State.Five) || HR.Check_Hand_State(Hand_State.SoftFive))
            {
                SptR.color = Color.white;
            }
            else
            {
                SptR.color = Color.cyan;
            }
        }
    }

    void Set_Color_Finger()
    {
        switch ((int)HN.Finger)
        {
            case 1:
                SptR.color = Color.red;
                break;
            case 2:
                SptR.color = Color.green;
                break;
            case 3:
                SptR.color = Color.blue;
                break;
            case 4:
                SptR.color = Color.cyan;
                break;
            case 5:
                SptR.color = Color.white;
                break;
        }
            
    }
}
