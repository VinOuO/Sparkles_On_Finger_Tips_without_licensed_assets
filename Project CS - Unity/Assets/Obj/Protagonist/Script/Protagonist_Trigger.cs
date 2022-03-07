using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Protagonist_Trigger : MonoBehaviour
{
    GameObject Protagonist;
    void Start()
    {
        Protagonist = GameObject.FindWithTag("Player");
    }

    void Update()
    {
        transform.position = Protagonist.transform.position;
    }
}
