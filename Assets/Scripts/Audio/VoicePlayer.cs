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

    protected bool PlaySoundFX(AudioClip audioClip, float volume = 1.0f) {
        //Debug.Log("Attempt to play : " + audioClip + "    isplaying? : " + isTalking);
        if (isTalking) return false;
        isTalking = true;
        audioSource.volume = volume;
        
        audioSource.clip = audioClip;
        audioSource.Play();
        return true;
    }

}
