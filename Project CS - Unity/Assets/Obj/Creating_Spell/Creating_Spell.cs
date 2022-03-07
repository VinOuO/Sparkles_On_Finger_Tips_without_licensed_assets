using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Creating_Spell : MonoBehaviour
{
    Image Img;
    Spell_Case SpC;

    public IEnumerator Going_to_Spell_Case(int _Spell)
    {
        Img = GetComponent<Image>();
        SpC = GameObject.Find("Spell_Case").GetComponent<Spell_Case>();
        float _Timer = 0, _Max_Timer = 40;
        Color _Color = Img.color;
        while (Img.color.a < 1)
        {
            _Color.a += 0.01f * (_Max_Timer == 40 ? 1 : -1);
            Img.color = _Color;
            _Timer++;
            if(_Timer >= _Max_Timer)
            {
                _Timer = 0;
                _Max_Timer = (_Max_Timer == 40 ? 20 : 40);
            }
            yield return new WaitForSeconds(0.01f);
        }
        GameObject _SpC = GameObject.Find("Creating_Spell_Des");
        Vector2 _Des = _SpC.GetComponent<RectTransform>().anchoredPosition;
        RectTransform _Rect = GetComponent<RectTransform>();
        float _Max_Dis = Vector2.Distance(_Des, _Rect.anchoredPosition);
        float _Dis = _Max_Dis;
        Vector2 _Max_Delta = _Rect.sizeDelta;
        while (_Dis > 10)
        {
            _Rect.anchoredPosition += (_Des - _Rect.anchoredPosition).normalized * (_Max_Dis / _Dis);
            _Dis = Vector2.Distance(_Des, _Rect.anchoredPosition);
            _Rect.sizeDelta = ((_Dis - 10) / _Max_Dis) * _Max_Delta;
            yield return new WaitForSeconds(0.01f);
        }
        SpC.Create_Spell(_Spell);
        Destroy(gameObject);
    }
}
