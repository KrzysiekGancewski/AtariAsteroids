using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class BackgroundMusicPlayer : MonoBehaviour {

    [SerializeField]
    private AudioClip[] audioClipNotes;
    private AudioSource audioSource;

    //Delays between next notes
    private const float initialDelay = 1f;
    private const float minDelay = 0.2f;
    private const float delayDecreaseRate = 0.05f;

    private float currentDelay = 1f;
    private float playTimer = 0f;
    private int clipIndex = 0;
    private bool isPlaying = true;

    private void Start () {
        audioSource = GetComponent<AudioSource>();
	}
	
	void Update () {
		if (isPlaying) {
            playTimer += Time.deltaTime;
            if (playTimer > currentDelay) {
                PlayNote();
                playTimer = 0f;
                if (currentDelay > minDelay) {
                    currentDelay -= delayDecreaseRate;
                }
            }
        }
	}

    public void Stop() {
        isPlaying = false;
    }

    public void Play() {
        isPlaying = true;
    }

    void PlayNote() {
        if (++clipIndex == audioClipNotes.Length) {
            clipIndex = 0;
        }

        audioSource.clip = audioClipNotes[clipIndex];
        audioSource.Play();
    }
}
