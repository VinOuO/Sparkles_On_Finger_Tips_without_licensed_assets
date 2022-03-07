using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HR_Imformation;

public class Hand : MonoBehaviour
{
    [SerializeField]
    GameObject Point_Pre;

    void Start()
    {
        Build_Hand();
    }

    void Update()
    {
        //Get_Finger_Bending_State(HR.Index_Nodes);
        Update_Finger_State();
    }

    void Build_Hand()
    {
        for(int i = 0; i < 21; i++)
        {
            GameObject _Temp;
            _Temp = Instantiate(Point_Pre, transform);
            _Temp.GetComponent<Point>().Index = i;
            _Temp.GetComponent<Point>().HN = HR.Hand_Nodes[i];
        }
    }

    void Update_Finger_State()
    {
        Get_Finger_Bending_State(HR.Thumb_Nodes);
        Get_Finger_Bending_State(HR.Index_Nodes);
        Get_Finger_Bending_State(HR.Middle_Nodes);
        Get_Finger_Bending_State(HR.Ring_Nodes);
        Get_Finger_Bending_State(HR.Pinky_Nodes);
    }

    void Get_Finger_Bending_State(List<Hand_Node> _Finger)
    {
        Vector2 Upper_Vec, Upper_Vec2, Downer_Vec;
        Upper_Vec = _Finger[3].World_Pos - _Finger[0].World_Pos;
        Upper_Vec2 = _Finger[3].World_Pos - _Finger[1].World_Pos;
        Downer_Vec = _Finger[0].World_Pos - HR.Hand_Nodes[0].World_Pos;

        float _Upper_Projection = Vector2.Dot(Upper_Vec2, Downer_Vec) / Downer_Vec.magnitude;
        float _Whole_Projection = Vector2.Dot(Upper_Vec, Downer_Vec) / Downer_Vec.magnitude;
        float _Len_Ratio = Upper_Vec.magnitude / Downer_Vec.magnitude;
        float _Upper_Cos = Vector2.Dot(Upper_Vec2, Downer_Vec) / (Upper_Vec2.magnitude * Downer_Vec.magnitude);
        float _Whole_Cos = Vector2.Dot(Upper_Vec, Downer_Vec) / (Upper_Vec.magnitude * Downer_Vec.magnitude);

        Finger_State _FS = Finger_State.Straight;
        //Debug.Log("LenR: " + _Len_Ratio);
        if (_Upper_Cos <= 0.7f || _Len_Ratio < 0.85f)
        {
            _FS = Finger_State.Bending;
        }
        
        if (_Whole_Cos <= 0.7f || _Len_Ratio < 0.4f)
        {
            _FS = Finger_State.Bended;
        }

        foreach(Hand_Node _HN in _Finger)
        {
            _HN.Finger_State = _FS;
        }
    }

}
