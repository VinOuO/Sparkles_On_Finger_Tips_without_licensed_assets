using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Dialog : MonoBehaviour
{
    [SerializeField] int State_Num;
    [SerializeField] GameObject Canves;
    [SerializeField] GameObject Protagonist;
    [SerializeField] float Start_Pos_P, End_Pos_P;
    [SerializeField] List<string> Lines;
    int Smoother = 1000;
    RectTransform Rct;
    Text Txt;
    [TextArea(2, 8)][SerializeField] string Full_Artical;

    void Start()
    {
        Rct = GetComponent<RectTransform>();
        Txt = GetComponent<Text>();
        Lines = new List<string>();
        Start_Pos = Rct.localPosition.y;
        End_Pos = Canves.GetComponent<RectTransform>().rect.height / 2;
        Lines = Get_Lines();
        State_Num = Lines.Count * Smoother;
    }

    void Update()
    {
        Set_Text();
    }


    float Start_Pos, End_Pos;
    void Set_Pos()
    {
        Rct.localPosition = Vector2.up * (Start_Pos + Get_ProgessPercentage() * (End_Pos - Start_Pos));
    }

    List<string> Get_Lines()
    {
        List<string> _Lines = new List<string>();
        _Lines.AddRange(Full_Artical.Split(new char[] { '\n' }));
        return _Lines;
    }

    void Set_Text()
    {
        int _State = Mathf.RoundToInt(Get_ProgessPercentage() * State_Num);
        int _Index = _State / Smoother;
        int _Smooth_Index = _State % Smoother;
        //Debug.Log("Percent: " + Get_ProgessPercentage() + ", Index: " + _Index);
        if (_Index < Lines.Count && _Index >= 0)
        {
            Txt.text = Lines[_Index];
        }
        else
        {
            Txt.text = "";
        }
        Color _Color = Txt.color;
        float _Liner_Alpha = 1 - (Mathf.Abs(_Smooth_Index - (float)Smoother / 2) / ((float)Smoother / 2));
        _Color.a = _Liner_Alpha * 2;
        Txt.color = _Color;
    }

    /// <summary>
    /// Return 0.0f ~ 1.0f
    /// </summary>
    /// <returns></returns>
    float Get_ProgessPercentage()
    {
        return (Protagonist.transform.position.x - Start_Pos_P) / (End_Pos_P - Start_Pos_P);
    }
}
