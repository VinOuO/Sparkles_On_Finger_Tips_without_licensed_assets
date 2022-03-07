using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Far_Low : MonoBehaviour
{
    GameObject Protagonist;

    void Start()
    {
        Protagonist = GameObject.FindWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Platform")
        {
            Protagonist.GetComponent<Protagonist>().LandingPos = transform.position;
        }
    }
}
