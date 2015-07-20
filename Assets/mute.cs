using UnityEngine;
using System.Collections;

public class mute : MonoBehaviour {


    // Use this for initialization
    void Start() {

    }

    // Update is called once per frame
    void Update() {

    }

    void OnMouseEnter() {
        transform.localScale = new Vector3(transform.localScale.x + 0.01f, transform.localScale.y + 0.01f, transform.localScale.z);
    }

    void OnMouseExit() {
        transform.localScale = new Vector3(transform.localScale.x - 0.01f, transform.localScale.y - 0.01f, transform.localScale.z);
    }

    void OnMouseDown() {
        AudioSource audio = GetComponent<AudioSource>();
        audio.Play();
    }


}
