using UnityEngine;
using System.Collections;

public class voicePlayer_Stalin : VoicePlayer_PlayerCar {

    public AudioClip[] onHit;
    public AudioClip[] intro;

	// Use this for initialization
	public override void Start () {
        audioSource = GetComponent<AudioSource>();
        GameManager.instance.voices_stalin = this;
	}



    public void PlayonHit() {
        AudioClip random = onHit[Random.Range(0, onHit.Length)];
        PlaySoundFX(random);
    }

    public void PlayIntro() {
        AudioClip random = intro[Random.Range(0, intro.Length)];
        PlaySoundFX(random);
    }
}
