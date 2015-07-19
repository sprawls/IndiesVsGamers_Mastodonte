using UnityEngine;
using UnityEngine.Audio;
using System.Collections;

public class PedestrianMoveForward : MonoBehaviour {

    public float multiplier = 1;
    public float minSpeed;
    public float maxSpeed;
    private float speed;

    public AudioMixerGroup mixerGroup;
    private AudioSource audioSource;


    void Awake() {
        speed = Random.Range(minSpeed, maxSpeed);
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null) audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.outputAudioMixerGroup = mixerGroup;
        audioSource.spatialBlend = 1f;
        PlaySound();
    }

    void Update() {
        gameObject.transform.position = new Vector3(gameObject.transform.position.x + speed * multiplier * Time.deltaTime,
                                                    gameObject.transform.position.y,
                                                    gameObject.transform.position.z); 
    }

    public void PlaySound() {
        audioSource.clip = GameManager.instance.voices_pedestrian.GetRandom();
        audioSource.Play();
    }
}
