using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Protagonist : MonoBehaviour
{
    Spell_Manager SpM;
    public PlayerStateList pState;
    Rigidbody2D Rig;
    Collider2D Cldr;
    Animator Amt;
    [SerializeField] Paint Paint;
    public Vector2 LastStandingPos = Vector2.zero;
    public Vector2 LandingPos = Vector2.zero;
    bool Jumped = false;
    Camera Level_Camera;
    void Start()
    {
        if (pState == null)
        {
            pState = GetComponent<PlayerStateList>();
        }
        Amt = GetComponent<Animator>();
        Rig = GetComponent<Rigidbody2D>();
        Cldr = GetComponent<Collider2D>();
        SpM = GameObject.Find("Spell_Manager").GetComponent<Spell_Manager>();
        Level_Camera = GameObject.Find("Level_Camera").GetComponent<Camera>();
    }

    void Update()
    {
        Amt.SetBool("Is_Running", Mathf.Abs(Rig.velocity.x) > 0.1f ? true : false);
        //Amt.SetBool("Is_Conjuring", Paint.Is_Painting);

        if (pState.grouneded)
        {
            Cldr.sharedMaterial.friction = 100;
        }
        else
        {
            Cldr.sharedMaterial.friction = 0;
        }
        
        Check_Falling_State();
        
        if (Input.GetMouseButtonDown(0))
        {
            //SpM.Cast_Spell(1, Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, transform.position.z - Camera.main.transform.position.z)));
            //SpM.Cast_Spell(1, transform.position);
        }

    }

    int Jump_Time = 0;
    void Jump()
    {
        if (Input.GetKeyDown(KeyCode.W))
        {
            Amt.SetTrigger("Jump_Up");
        }
        if (Input.GetKey(KeyCode.W) && !Jumped)
        {
            Rig.AddForce(Vector2.up * 10000 * Time.deltaTime);
            Jump_Time++;
            if(Jump_Time >= 7)
            {
                Jumped = true;
            }
        }
        if (Input.GetKeyUp(KeyCode.W))
        {
            Jumped = true;
        }
    }

    /// <summary>
    /// 0: Stand, 1: Up, 2: Down
    /// </summary>
    public int Falling_State = 0;
    void Check_Falling_State()
    {
        if(pState.grouneded)
        {
            if(Falling_State != 0)
            {
                Falling_State = 0;
                Jumped = false;
                Jump_Time = 0;
                Amt.SetTrigger("Stand");
            }
        }
        else if (Rig.velocity.y > 0)
        {
            if (Falling_State != 1)
            {
                Falling_State = 1;
                Amt.SetTrigger("Jump_Up");
            }
        }
        else if (Rig.velocity.y < 0)
        {
            if (Falling_State != 2)
            {
                Falling_State = 2;
                Amt.SetTrigger("Jump_Down");
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Platform" && collision.contacts[0].normal.y > 0.5f)
        {
            LastStandingPos = new Vector2(collision.contacts[0].point.x, collision.contacts[0].point.y);
        }
    }
}
