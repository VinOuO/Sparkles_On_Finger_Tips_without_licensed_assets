using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fire_Ball : MonoBehaviour
{
    float _Swamn_Time;
    void Start()
    {
        Vector3 _Vec3 = transform.localScale;
        _Vec3.x = (GameObject.Find("Protagonist").transform.localScale.x >= 0 ? 1 : -1);
        transform.localScale = _Vec3;

        _Swamn_Time = Time.time;
    }

    void Update()
    {
        if(Time.time - _Swamn_Time > 1.5f)
        {
            Destroy(gameObject);
        }
        transform.Translate((transform.localScale.x > 0 ? Vector3.right : Vector3.left) * Time.deltaTime * 50);
    }
}
