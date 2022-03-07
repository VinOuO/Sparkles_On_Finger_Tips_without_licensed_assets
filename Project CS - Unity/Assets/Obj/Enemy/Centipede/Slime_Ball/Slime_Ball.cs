using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slime_Ball : MonoBehaviour
{
    public int Dir = 1;
    float Birth_Time;
    void Start()
    {
        Birth_Time = Time.time;
    }

    void Update()
    {
        transform.Translate(Vector3.right * -Dir * Time.deltaTime * 10);
        if(Time.time - Birth_Time > 5)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            collision.GetComponent<Protagonist_Movement>().Set_Hit(transform.position);
            Destroy(gameObject);
        }
    }
}
