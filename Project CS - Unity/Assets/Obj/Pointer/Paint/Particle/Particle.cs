using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Particle : MonoBehaviour
{
    void Start()
    {
        
    }

    void Update()
    {
        
    }
    public bool Is_Playing = false;
    public void Stop_Particle()
    {
        GetComponent<ParticleSystem>().Stop(true);
    }

    public void Play_Particle()
    {
        GetComponent<ParticleSystem>().Play(true);
    }
}
