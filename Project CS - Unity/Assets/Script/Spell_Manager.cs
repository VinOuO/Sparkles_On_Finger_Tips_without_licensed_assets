using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spell_Manager : MonoBehaviour
{
    /// <summary>
    /// 0: Lightning 1: Fire_Ball
    /// </summary>
    public GameObject[] Spell_Pres;

    [SerializeField] LayerMask Ground_Layer;
    GameObject Protagonist;
    void Start()
    {
        Protagonist = GameObject.Find("Protagonist");
    }

    void Update()
    {

    }


    public void Cast_Spell(int _Spell, Vector2 _Pos)
    {
        //Debug.Log("Index: " + _Spell);
        RaycastHit2D _Hit;
        switch (_Spell)
        {
            case 0:
                _Hit = Physics2D.Raycast(_Pos, Vector2.down, 1000, Ground_Layer);
                Instantiate(Spell_Pres[_Spell], transform).transform.position = new Vector3(_Hit.point.x, _Hit.point.y, transform.position.z);
                break;
            case 1:
                Instantiate(Spell_Pres[_Spell], transform).transform.position = new Vector3(Protagonist.transform.position.x, Protagonist.transform.position.y, transform.position.z);
                break;
            case 2:
                _Hit = Physics2D.Raycast(_Pos, Vector2.down, 1000, Ground_Layer);
                GameObject _Temp = Instantiate(Spell_Pres[_Spell], transform);
                _Temp.transform.position = new Vector3(_Hit.point.x, _Hit.point.y, transform.position.z);
                _Temp.transform.localScale = Protagonist.transform.localScale.x >= 0 ? new Vector3(1, 1, 1) : new Vector3(-1, 1, 1);
                break;
        }
        
    }
}


