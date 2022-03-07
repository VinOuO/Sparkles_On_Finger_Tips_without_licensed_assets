using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tornado : MonoBehaviour
{
    float _Swamn_Time;
    [SerializeField] LayerMask Ground_Layer;
    void Start()
    {
        _Swamn_Time = Time.time;
    }

    void Update()
    {
        if (!Is_Ending)
        {
            if (Time.time - _Swamn_Time > 2f || !Physics2D.Raycast(transform.position, Vector2.down, 0.3f, Ground_Layer) || Physics2D.Raycast(transform.position + Vector3.up, (transform.localScale.x > 0 ? Vector2.right : Vector2.left), 0.5f, Ground_Layer))
            {
                StartCoroutine(Ending());
            }
            transform.Translate((transform.localScale.x > 0 ? Vector3.right : Vector3.left) * 0.2f);
        }
    }


    void OnDrawGizmos()
    {
        Gizmos.color = Color.cyan;

        Gizmos.DrawLine(transform.position, transform.position + new Vector3(0, -0.3f));
    }
    bool Is_Ending = false;
    IEnumerator Ending()
    {
        Is_Ending = true;
        float _Timer = Time.time;
        GetComponent<BoxCollider2D>().enabled = false;
        AudioSource _AdS = GetComponent<AudioSource>();
        SpriteRenderer _SpR= GetComponent<SpriteRenderer>();
        float _Value = _AdS.volume / 10;
        while (Time.time - _Timer < 1f)
        {
            Color _Color = _SpR.color;
            _Color.a -= 0.1f;
            _SpR.color = _Color;
            _AdS.volume -= _Value;
            yield return new WaitForSeconds(0.1f);
        }
        Destroy(gameObject);
    }
}
