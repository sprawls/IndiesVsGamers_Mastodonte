using UnityEngine;
using System.Collections;

public class sounds_pedestrian : MonoBehaviour {


    public AudioClip[] shouts;

    public void Start() {
        GameManager.instance.voices_pedestrian = this;
    }

    public AudioClip GetRandom() {
        return shouts[Random.Range(0,shouts.Length)];
    }
}
