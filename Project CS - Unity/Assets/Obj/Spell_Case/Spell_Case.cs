using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Spell_Case : MonoBehaviour
{
    [SerializeField]GameObject Spell_Pre;
    public Sprite[] Spell_Images = new Sprite[5];
    List<GameObject> Spells = new List<GameObject>();
    public GameObject Selected_Spell;
    void Start()
    {
        //Create_Spell(0);
        //Create_Spell(1);
        Create_Spell(0);
        Create_Spell(1);
        Create_Spell(2);
        Create_Spell(0);
        Create_Spell(1);
        Create_Spell(2);
        Create_Spell(0);
        Create_Spell(1);
        Create_Spell(2);
        
    }
    public float MaxSpeed = 0;
    float Speed = 0;
    void Update()
    {
        if (Input.mouseScrollDelta.y != 0)
        {
            Set_Scroll_Speed(Input.mouseScrollDelta.y * Time.deltaTime * 1000);
        }
        Speed -= Speed * 8f * Time.deltaTime;
        Scroll_Spell_Case(Speed);
    }

    public void Create_Spell(int _Spell)
    {
        GameObject _Temp;
        _Temp = Instantiate(Spell_Pre, transform);
        _Temp.GetComponent<Spell>().Init(Spell_Images[_Spell]);
        _Temp.GetComponent<Spell>().Index = _Spell;
        Add_to_Spell_Case(_Temp);

        Selected_Spell = Spells[0];
        float _Min_x = float.MaxValue;
        foreach (GameObject _S in Spells)
        {
            if (Mathf.Abs(_S.GetComponent<RectTransform>().anchoredPosition.x) < Mathf.Abs(_Min_x))
            {
                Selected_Spell = _S;
                _Min_x = _S.GetComponent<RectTransform>().anchoredPosition.x;
            }
        }
        foreach (GameObject _S in Spells)
        {
            _S.GetComponent<RectTransform>().anchoredPosition -= Vector2.right * _Min_x;
        }
    }

    public void Set_Scroll_Speed(float _Speed)
    {
        MaxSpeed = _Speed;
        Speed = MaxSpeed;
    }

    void Scroll_Spell_Case(float _Speed)
    {
        if(Spells.Count > 0)
        {
            if (Speed / MaxSpeed > 0.05f)
            {
                if (_Speed < 0 && Spells[Spells.Count - 1].GetComponent<RectTransform>().anchoredPosition.x <= 0)
                {
                    return;
                }
                if (_Speed > 0 && Spells[0].GetComponent<RectTransform>().anchoredPosition.x >= 0)
                {
                    return;
                }
                foreach (GameObject _Spell in Spells)
                {
                    _Spell.GetComponent<RectTransform>().anchoredPosition += Vector2.right * _Speed;
                }
            }   
            else if(Speed != 0)
            {
                Speed = 0;
                Selected_Spell = Spells[0];
                float _Min_x = float.MaxValue;
                foreach (GameObject _Spell in Spells)
                {
                    if(Mathf.Abs(_Spell.GetComponent<RectTransform>().anchoredPosition.x) < Mathf.Abs(_Min_x))
                    {
                        Selected_Spell = _Spell;
                        _Min_x = _Spell.GetComponent<RectTransform>().anchoredPosition.x;
                    }
                }
                foreach (GameObject _Spell in Spells)
                {
                    _Spell.GetComponent<RectTransform>().anchoredPosition -= Vector2.right * _Min_x;
                }
            }
        }
    }

    void Add_to_Spell_Case(GameObject _Spell)
    {
        RectTransform _Spell_Rect = _Spell.GetComponent<RectTransform>();
        if (Spells.Count != 0)
        {
            RectTransform _Last_Spell_Rect = Spells[Spells.Count - 1].GetComponent<RectTransform>();
            Vector2 _Temp = _Spell_Rect.anchoredPosition;
            _Temp.x = _Last_Spell_Rect.anchoredPosition.x + _Last_Spell_Rect.rect.width / 2 + _Spell_Rect.rect.width / 2;
            _Spell_Rect.anchoredPosition = _Temp;
        }
        else
        {
            _Spell_Rect.anchoredPosition = Vector2.zero;
        }
        
        Spells.Add(_Spell);
    }

    public void Remove_from_Spell_Case(GameObject _Spell)
    {
        int _Remove_Index = Spells.FindIndex(_S => _S == _Spell);
        for(int i = _Remove_Index + 1; i < Spells.Count; i++)
        {
            RectTransform _Spell_Rect = Spells[i].GetComponent<RectTransform>();
            if (i - 2 >= 0)
            {
                RectTransform _Last_Spell_Rect = Spells[i - 2].GetComponent<RectTransform>();
                Vector2 _Temp = _Spell_Rect.anchoredPosition;
                _Temp.x = _Last_Spell_Rect.anchoredPosition.x + _Last_Spell_Rect.rect.width / 2 + _Spell_Rect.rect.width / 2;
                _Spell_Rect.anchoredPosition = _Temp;
            }
            else
            {
                _Spell_Rect.anchoredPosition = Vector2.zero;
            }
        }
        Spells.RemoveAt(_Remove_Index);
        Destroy(_Spell);

        if (Spells.Count > 0)
        {
            Selected_Spell = Spells[0];
            float _Min_x = float.MaxValue;
            foreach (GameObject _S in Spells)
            {
                if (Mathf.Abs(_S.GetComponent<RectTransform>().anchoredPosition.x) < Mathf.Abs(_Min_x))
                {
                    Selected_Spell = _S;
                    _Min_x = _S.GetComponent<RectTransform>().anchoredPosition.x;
                }
            }
            foreach (GameObject _S in Spells)
            {
                _S.GetComponent<RectTransform>().anchoredPosition -= Vector2.right * _Min_x;
            }
        }  
    }

    [SerializeField] GameObject Creating_Spell_Pre;
    public void Creating_Spell(int _Spell)
    {
        GameObject _Temp = Instantiate(Creating_Spell_Pre, transform.parent);
        _Temp.GetComponent<Image>().sprite = Spell_Images[_Spell];
        StartCoroutine(_Temp.GetComponent<Creating_Spell>().Going_to_Spell_Case(_Spell));
    }
}
