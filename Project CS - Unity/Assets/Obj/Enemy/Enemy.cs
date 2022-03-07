using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    Animator Amt;
    Rigidbody2D Rig;

    [Header("Protagonist Checking")]
    [SerializeField] Transform pTransform; 
    [SerializeField] float pCheckY = 1;
    [SerializeField] float pCheckX = 10f;
    [SerializeField] LayerMask pLayer;

    [Header("atFall Checking")]
    [SerializeField] Transform fTransform;
    [SerializeField] float fCheckY = 1;
    [SerializeField] float fCheckX = 10f;
    [SerializeField] LayerMask gLayer;


    void Start()
    {
        Amt = GetComponent<Animator>();
        Rig = GetComponent<Rigidbody2D>();
        StartCoroutine(Walking());
    }

    void Update()
    {
        Update_Anim_State();
        //transform.Translate(Vector3.right * 1 * Time.deltaTime * _Dir);
        if (Found_Portagonist())
        {
            //Debug.Log("Attack!");
        }
    }

    void Update_Anim_State()
    {
        Amt.SetBool("Is_Walking", Mathf.Abs(Rig.velocity.x) > 0.05f ? true : false);
        Vector3 _Temp = transform.localScale;
        if (_Temp.x > 0 && Rig.velocity.x > 0 || _Temp.x < 0 && Rig.velocity.x < 0)
        {
            _Temp.x *= -1;
        }
        transform.localScale = _Temp;
    }

    int Dir = 1;
    IEnumerator Walking()
    {

        float _Duraction = Random.Range(0.5f, 3f);
        float _Speed = 100;
        while (true)
        {
            float _Timer = Time.time;
            while (Time.time - _Timer < _Duraction)
            {
                if (Is_about_to_Fall())
                {
                    Dir *= -1;
                }
                Rig.velocity = Vector2.right * _Speed * Dir * Time.deltaTime;
                yield return new WaitForEndOfFrame();
            }
            Dir *= -1;
            _Duraction = Random.Range(0.5f, 3f);
            _Speed = (Random.Range(-1, 1) >= 0) ? 200 : 0;
            _Timer = Time.time;
        }
    }

    public bool Found_Portagonist()
    {
        if (Physics2D.Raycast(pTransform.position, (transform.localScale.x > 0 ? Vector2.left : Vector2.right), pCheckX, pLayer) || Physics2D.Raycast(pTransform.position + new Vector3(0, -pCheckY), (transform.localScale.x > 0 ? Vector2.left : Vector2.right), pCheckX, pLayer) || Physics2D.Raycast(pTransform.position + new Vector3(0, pCheckY), (transform.localScale.x > 0 ? Vector2.left : Vector2.right), pCheckX, pLayer))
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public bool Is_about_to_Fall()
    {
        if (!Physics2D.Raycast(fTransform.position, Vector2.down, fCheckY, gLayer) || !Physics2D.Raycast(fTransform.position + new Vector3(fCheckX, 0), Vector2.down, fCheckY, gLayer) || !Physics2D.Raycast(fTransform.position + new Vector3(-fCheckX, 0), Vector2.down, fCheckY, gLayer))
        {
            Debug.Log("!");
            return true;
        }
        else
        {
            return false;
        }
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;

        Gizmos.DrawLine(pTransform.position, pTransform.position + new Vector3((transform.localScale.x > 0 ? -1 : 1) * pCheckX, 0));
        Gizmos.DrawLine(pTransform.position + new Vector3(0, pCheckY), pTransform.position + new Vector3((transform.localScale.x > 0 ? -1 : 1) * pCheckX, pCheckY));
        Gizmos.DrawLine(pTransform.position + new Vector3(0, -pCheckY), pTransform.position + new Vector3((transform.localScale.x > 0 ? -1 : 1) * pCheckX, -pCheckY));

        Gizmos.color = Color.blue;
        Gizmos.DrawLine(fTransform.position, fTransform.position + new Vector3(0, -1 * fCheckY, 0));
        Gizmos.DrawLine(fTransform.position + new Vector3(fCheckX, 0), fTransform.position + new Vector3(fCheckX, 0) + new Vector3(0, -1 * fCheckY, 0));
        Gizmos.DrawLine(fTransform.position + new Vector3(-fCheckX, 0), fTransform.position + new Vector3(-fCheckX, 0) + new Vector3(0, -1 * fCheckY, 0));
    }

}
