using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cam : MonoBehaviour
{
    GameObject Protagonist;
    [SerializeField] Platform Plt;
    void Start()
    {
        Protagonist = GameObject.FindWithTag("Player");
    }

    Vector2 Target_Pos = Vector2.zero;

    void Update()
    {
        if(Protagonist == null)
        {
            Protagonist = GameObject.FindWithTag("Player");
        }

        if (Plt == null)
        {
            Plt = GameObject.Find("Platform").GetComponent<Platform>();
        }

        if (Protagonist.GetComponent<Protagonist>().Falling_State != 2)
        {
            Target_Pos = new Vector2(Protagonist.transform.position.x, Protagonist.GetComponent<Protagonist>().LastStandingPos.y);
        }
        else
        {
            //Target_Pos = new Vector2(Protagonist.transform.position.x, Protagonist.GetComponent<Protagonist>().LandingPos.y);
            Target_Pos = new Vector2(Protagonist.transform.position.x, Protagonist.transform.position.y + Protagonist.GetComponent<Rigidbody2D>().velocity.y * Time.deltaTime);
        }
        Vector2 _Temp = Vector2.zero;
        _Temp.x = Target_Pos.x - transform.position.x;
        _Temp.y = Target_Pos.y - transform.position.y;
        _Temp.x = _Temp.x > 0 && transform.position.x > Plt.Borders[3] ? 0 : _Temp.x;
        _Temp.x = _Temp.x < 0 && transform.position.x < Plt.Borders[2] ? 0 : _Temp.x;
        _Temp.y = _Temp.y < 0 && transform.position.y < Plt.Borders[1] ? 0 : _Temp.y;
        _Temp.y = _Temp.y > 0 && transform.position.y > Plt.Borders[0] ? 0 : _Temp.y;
        transform.position += (Vector3.right * _Temp.x + Vector3.up * _Temp.y) * 2 * Time.deltaTime;
    }

    
}
