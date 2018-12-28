using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(ParticleSystem))]
public class DestroyParticlesAfterEmission : MonoBehaviour {
    
	private void Start () {
        ParticleSystem ps = GetComponent<ParticleSystem>();
        float totalDuration = ps.main.duration + ps.main.startLifetime.constantMax;
        Destroy(gameObject, totalDuration);
    }
}
