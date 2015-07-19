using UnityEngine;
using System.Collections;

public class VoicePlayer_PlayerCar : VoicePlayer {

    public AudioClip[] AirDowns;
    public AudioClip[] CarHit;
    public AudioClip[] PedHit;
    public AudioClip[] Yeahs;
    public AudioClip[] JobDone;
    public AudioClip[] LikeNew;
    public AudioClip[] GameOver;
    public AudioClip[] Pause;
    public AudioClip[] Scores;
    public AudioClip[] Taunts;
    public AudioClip nineThousands;

    public void PlayAirDown() {
        AudioClip random = AirDowns[Random.Range(0,AirDowns.Length)];
        PlaySoundFX(random);
    }

    public void PlayCarHit() {
        AudioClip random = CarHit[Random.Range(0, CarHit.Length)];
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

    public void PlayJobDone() {
        AudioClip random = JobDone[Random.Range(0, JobDone.Length)];
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

    public void Play9000() {
        PlaySoundFX(nineThousands);
    }
}
