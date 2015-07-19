using UnityEngine;
using System.Collections;

public class VoicePlayer_PlayerCar : VoicePlayer {

    public AudioClip[] AirDowns;
    public AudioClip[] CarHit;
    public AudioClip[] PedHit;
    public AudioClip[] Yeahs;
    public AudioClip[] BehindBar;
    public AudioClip[] LikeNew;
    public AudioClip[] GameOver;
    public AudioClip[] Pause;
    public AudioClip[] Scores;
    public AudioClip[] Taunts;
    public AudioClip[] PoliceAlarm;
    public AudioClip[] Justice;
    public AudioClip nineThousands;
    public AudioClip Carl;

    public override void Start() {
        audioSource = GetComponent<AudioSource>();
        GameManager.instance.voices_player = this;
    }

    public void PlayAirDown() {
        AudioClip random = AirDowns[Random.Range(0,AirDowns.Length)];
        PlaySoundFX(random);
    }

    public void PlayCarHit() {
        int i = Random.Range(0, CarHit.Length);
        //Debug.Log(i);
        AudioClip random = CarHit[i];
        PlaySoundFX(random);
    }

    public void PlayPedHit() {
        AudioClip random = PedHit[Random.Range(0, PedHit.Length)];
        PlaySoundFX(random);
    }

    public void PlayYeahs() {
        AudioClip random = Yeahs[Random.Range(0, Yeahs.Length)];
        PlaySoundFX(random);
    }

    public void PlayBehindBars() {
        AudioClip random = BehindBar[Random.Range(0, BehindBar.Length)];
        PlaySoundFX(random);
    }

    public void PlayScores() {
        AudioClip random = Scores[Random.Range(0, Scores.Length)];
        PlaySoundFX(random);
    }

    public void PlayTaunts() {
        AudioClip random = Taunts[Random.Range(0, Taunts.Length)];
        PlaySoundFX(random);
    }

    public void PlayPause() {
        AudioClip random = Pause[Random.Range(0, Pause.Length)];
        PlaySoundFX(random);
    }

    public void PlayPolice() {
        AudioClip random = PoliceAlarm[Random.Range(0, PoliceAlarm.Length)];
        PlaySoundFX(random);
    }

    public void PlayJustice() {
        AudioClip random = Justice[Random.Range(0, Justice.Length)];
        PlaySoundFX(random);
    }

    public void PlayLikeNew() {
        AudioClip random = LikeNew[Random.Range(0, LikeNew.Length)];
        PlaySoundFX(random);
    }

    public void Play9000() {
        PlaySoundFX(nineThousands);
    }

    public bool PlayCarl() {
        return(PlaySoundFX(Carl));
    }
}
