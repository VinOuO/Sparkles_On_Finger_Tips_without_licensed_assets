using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spell_Behavior : MonoBehaviour
{
    [SerializeField] string Spell_Name;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            collision.gameObject.GetComponent<Animator>().SetTrigger("Hurt");
            switch (Spell_Name)
            {
                case "Fire_Ball":
                    collision.gameObject.GetComponent<Enemy_Behavior>().Hurt(50);
                    break;
                case "Tornado":
                    collision.gameObject.GetComponent<Enemy_Behavior>().Hurt(50);
                    break;
                case "Lightning":
                    collision.gameObject.GetComponent<Enemy_Behavior>().Hurt(50);
                    break;
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            collision.gameObject.GetComponent<Animator>().SetTrigger("Hurt");
            switch (Spell_Name)
            {
                case "Fire_Ball":
                    collision.gameObject.GetComponent<Enemy_Behavior>().Hurt(50);
                    break;
                case "Tornado":
                    collision.gameObject.GetComponent<Enemy_Behavior>().Hurt(50);
                    break;
                case "Lightning":
                    collision.gameObject.GetComponent<Enemy_Behavior>().Hurt(50);
                    break;
            }
        }
    }
}
