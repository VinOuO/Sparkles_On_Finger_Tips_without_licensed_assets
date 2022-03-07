using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class River : MonoBehaviour
{
    SpriteRenderer SptR;
    void Start()
    {
        SptR = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        transform.Translate(Vector3.right * 0.05f);
        if(transform.localPosition.x > 5.76f)
        {
            Vector3 _Temp = transform.localPosition;
            _Temp.x = 0;
            transform.localPosition = _Temp;
        }
    }
}
