using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Mana : MonoBehaviour
{
    Image Img;
    float MaxAmount = 100;
    public float Amount = 100;
    void Start()
    {
        Img = GetComponent<Image>();
        StartCoroutine(Regening_Mana());
    }

    void Update()
    {
        
    }

    public void Reduce_Mana(float _Amount)
    {
        Amount -= _Amount;
        Amount = Amount < 0 ? 0 : Amount;
        Img.fillAmount = Amount / MaxAmount;
    }

    public void Gen_Mana(float _Amount)
    {
        Amount += _Amount;
        Amount = Amount > MaxAmount ? MaxAmount : Amount; 
        Img.fillAmount = Amount/ MaxAmount;
    }

    IEnumerator Regening_Mana()
    {
        while (true)
        {
            Gen_Mana(0.5f);
            yield return new WaitForSeconds(0.1f);
        }
    }
}
