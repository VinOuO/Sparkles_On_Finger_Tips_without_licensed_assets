using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Ending : MonoBehaviour
{

    [SerializeField] Image Ending_Img;
    [SerializeField] Text Ending_Text;
    [SerializeField] AudioClip Wining_Sound;

    void Start()
    {
        StartCoroutine(Checking_for_Ending());
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            StartCoroutine(Game_Ending());
        }
    }

    IEnumerator Checking_for_Ending()
    {
        GameObject[] Enemies;
        while (true)
        {
            yield return new WaitForSeconds(1);

            Enemies = GameObject.FindGameObjectsWithTag("Enemy");
            if(Enemies.Length <= 0)
            {
                Debug.Log("End!");
                StartCoroutine(Game_Ending());
                break;
            }
        }
    }

    IEnumerator Game_Ending()
    {
        Camera.main.GetComponent<AudioSource>().Stop();
        yield return new WaitForSeconds(0.5f);
        GetComponent<AudioSource>().Play();
        while (Ending_Img.color.a < 1)
        {
            yield return new WaitForSeconds(0.01f);
            Color _Temp = Ending_Img.color;
            _Temp.a += 0.005f;
            Ending_Img.color = _Temp;
        }
        yield return new WaitForSeconds(1f);
        while (Ending_Text.color.a < 1)
        {
            yield return new WaitForSeconds(0.01f);
            Color _Temp = Ending_Text.color;
            _Temp.a += 0.005f;
            Ending_Text.color = _Temp;
        }
        yield return new WaitForSeconds(5f);
        Application.Quit();
    }
}
