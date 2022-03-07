using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Hurt_Mask : MonoBehaviour
{
    void Start()
    {
        
    }

    void Update()
    {
        
    }

    public IEnumerator Shining(float _Duraction)
    {
        Color _Color;
        Image _Img = GetComponent<Image>();
        float _Timer = Time.time;
        int _Trend = 1;
        while(Time.time - _Timer <= _Duraction)
        {
            _Color = _Img.color;
            _Color.a += (_Trend > 0 ? 1 : -1) * 0.2f;
            _Img.color = _Color;
            if(_Img.color.a > 0.5f && _Trend > 0)
            {
                _Trend = -1;
            }
            else if (_Img.color.a < 0f && _Trend < 0)
            {
                _Trend = 1;
            }
            yield return new WaitForSeconds(0.05f);
        }
        _Color = _Img.color;
        _Color.a = 0;
        _Img.color = _Color;
    }
}
