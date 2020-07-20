using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destroy_On_Particle_Completion : MonoBehaviour
{
    ParticleSystem pSystem;
    // Start is called before the first frame update
    void Start()
    {
        pSystem = gameObject.GetComponent<ParticleSystem>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!(pSystem.IsAlive()))
        {
            Destroy(gameObject);
        }
    }
}
