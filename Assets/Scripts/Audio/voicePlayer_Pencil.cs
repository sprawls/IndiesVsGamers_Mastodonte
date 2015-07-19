using UnityEngine;
using System.Collections;

public class voicePlayer_Pencil : VoicePlayer_PlayerCar {


    public AudioClip[] intro;

    public override void Start() {
        audioSource = GetComponent<AudioSource>();
        GameManager.instance.voices_pencil = this;
    }

    public void PlayIntro() {
        AudioClip random = intro[Random.Range(0, intro.Length)];
        PlaySoundFX(random);
    }
}
