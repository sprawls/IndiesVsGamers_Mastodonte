using UnityEngine;
using System.Collections;

public class VoicePlayer : MonoBehaviour {

    public AudioSource audioSource;

    protected bool isTalking = false;

	public virtual void Start () {
        audioSource = GetComponent<AudioSource>();
	}
	

	void Update () {
        if (isTalking) {
            if (audioSource.isPlaying == false) isTalking = false;
        }
	}

    protected void PlaySoundFX(AudioClip audioClip) {
        if (isTalking) return;
        isTalking = true;
        audioSource.clip = audioClip;
        audioSource.Play();
    }

}
