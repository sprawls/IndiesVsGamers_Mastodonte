using UnityEngine;
using System.Collections;

public class VoicePedestrian : MonoBehaviour {

    public AudioClip[] audioList;

    public void PlayScream(AudioSource other) {
        if (Random.Range(0, 3) == 0) {
            other.clip = audioList[Random.Range(0, audioList.Length)];
            other.Play();
        }
    }
}
