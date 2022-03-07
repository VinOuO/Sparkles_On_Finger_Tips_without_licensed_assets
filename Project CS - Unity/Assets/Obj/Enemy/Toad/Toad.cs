using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Toad : MonoBehaviour
{
    Animator Amt;
    Rigidbody2D Rig;
    GameObject Player;
    public GameObject Slime_Ball_Pre;
    Enemy_Behavior EB;

    [Header("Protagonist Checking")]
    [SerializeField] Transform pTransform;
    [SerializeField] float pCheckY = 1;
    [SerializeField] float pCheckX = 10f;
    [SerializeField] LayerMask pLayer;

    [Header("Attack Checking")]
    [SerializeField] float aCheckY = 1;
    [SerializeField] float aCheckX = 10f;

    [Header("atFall Checking")]
    [SerializeField] Transform fTransform;
    [SerializeField] float fCheckY = 1;
    [SerializeField] float fCheckX = 10f;
    [SerializeField] LayerMask gLayer;


    void Start()
    {
        Player = GameObject.Find("Protagonist");
        EB = GetComponent<Enemy_Behavior>();
        Amt = GetComponent<Animator>();
        Rig = GetComponent<Rigidbody2D>();
        StartCoroutine(Walking());
        StartCoroutine(Checking_Attacking());
    }

    void Update()
    {
        Update_Anim_State();
        //transform.Translate(Vector3.right * 1 * Time.deltaTime * _Dir);
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            //Amt.SetTrigger("Die");
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
            if (!Is_Attacking)
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
                    if (Is_Attacking)
                    {
                        break;
                    }
                }
                Dir *= -1;
                _Duraction = Random.Range(0.5f, 3f);
                _Speed = (Random.Range(-1, 1) >= 0) ? 200 : 0;
                _Timer = Time.time;
            }
            else
            {
                bool _Stop = false;
                int _Dir = Dir;
                while (Is_Attacking)
                {
                    if (Is_about_to_Fall() && !_Stop)
                    {
                        _Stop = true;
                    }
                    else if (!Is_about_to_Fall() && _Stop)
                    {
                        _Stop = false;
                    }
                    if ((Player.transform.position.x < transform.position.x ? -1 : 1) != _Dir)
                    {
                        _Dir = (Player.transform.position.x < transform.position.x ? -1 : 1);
                        _Stop = false;
                    }

                    if (Vector2.Distance(Player.transform.position, transform.position) < 10 && !Is_Attack_CDing)
                    {
                        Is_Attack_CDing = true;
                        Amt.SetTrigger("Attack");
                    }
                    Rig.velocity = Vector2.right * (_Stop || Is_Playing_Attack ? 0 : 300) * _Dir * Time.deltaTime;
                    
                    yield return new WaitForEndOfFrame();
                }
            }
        }
    }

    public bool Is_Attack_CDing = false;
    public bool Is_Playing_Attack = false;
    public void Attack()
    {
        if (Attack_Protagonist())
        {
            Player.GetComponent<Protagonist_Movement>().Set_Hit(transform.position);
        }
        StartCoroutine(Attack_CDing());
    }
    /*
    public void Set_Is_Attacking()
    {
        Is_Playing_Attack = !Is_Playing_Attack;
    }
    */
    public void Play_Attack_Sound_Effect()
    {
        GetComponent<AudioSource>().Play();
    }


    IEnumerator Attack_CDing()
    {
        yield return new WaitForSeconds(5f);
        Is_Attack_CDing = false;
    }

    public void Die()
    {
        Destroy(gameObject);
    }

    public bool Is_Attacking = false;
    IEnumerator Checking_Attacking()
    {
        while (true)
        {
            Is_Attacking = Found_Portagonist();
            if (Is_Attacking)
            {
                yield return new WaitForSeconds(5);
                Is_Attacking = false;
            }
            yield return new WaitForEndOfFrame();
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

    public bool Attack_Protagonist()
    {
        if (Physics2D.Raycast(fTransform.position, (transform.localScale.x > 0 ? Vector2.left : Vector2.right), aCheckX, pLayer) || Physics2D.Raycast(fTransform.position + new Vector3(0, -aCheckY), (transform.localScale.x > 0 ? Vector2.left : Vector2.right), aCheckX, pLayer) || Physics2D.Raycast(fTransform.position + new Vector3(0, aCheckY), (transform.localScale.x > 0 ? Vector2.left : Vector2.right), aCheckX, pLayer))
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

        Gizmos.DrawLine(fTransform.position, fTransform.position + new Vector3((transform.localScale.x > 0 ? -1 : 1) * aCheckX, 0));
        Gizmos.DrawLine(fTransform.position + new Vector3(0, pCheckY), fTransform.position + new Vector3((transform.localScale.x > 0 ? -1 : 1) * aCheckX, aCheckY));
        Gizmos.DrawLine(fTransform.position + new Vector3(0, -pCheckY), fTransform.position + new Vector3((transform.localScale.x > 0 ? -1 : 1) * aCheckX, -aCheckY));

        Gizmos.color = Color.yellow;

        Gizmos.DrawLine(pTransform.position, pTransform.position + new Vector3((transform.localScale.x > 0 ? -1 : 1) * pCheckX, 0));

        Gizmos.color = Color.blue;
        Gizmos.DrawLine(fTransform.position, fTransform.position + new Vector3(0, -1 * fCheckY, 0));
        Gizmos.DrawLine(fTransform.position + new Vector3(fCheckX, 0), fTransform.position + new Vector3(fCheckX, 0) + new Vector3(0, -1 * fCheckY, 0));
        Gizmos.DrawLine(fTransform.position + new Vector3(-fCheckX, 0), fTransform.position + new Vector3(-fCheckX, 0) + new Vector3(0, -1 * fCheckY, 0));
    }

}
