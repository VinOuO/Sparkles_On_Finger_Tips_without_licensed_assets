using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Spell : MonoBehaviour
{
    public int Index = 0;
    Image Img;
    void Start()
    {
        
    }

    void Update()
    {
        
    }

    public void Init(Sprite _Img)
    {
        Img = GetComponent<Image>();
        Img.sprite = _Img;
    }
}
