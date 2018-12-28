using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class DestroySoundAfterPlay : MonoBehaviour {
    
	void Start () {
        AudioClip audioClip = GetComponent<AudioSource>().clip;

        if (audioClip == null) {
            Debug.Log(gameObject.name + " | AudioSource clip == null");
            Destroy(gameObject, 2f);
        } else { 
            Destroy(gameObject, audioClip.length);
        }
	}
}
