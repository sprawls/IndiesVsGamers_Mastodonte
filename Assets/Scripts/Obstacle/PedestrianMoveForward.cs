using UnityEngine;
using System.Collections;

public class PedestrianMoveForward : MonoBehaviour {

    public float multiplier = 1;
    public float minSpeed;
    public float maxSpeed;
    private float speed;

    void Awake() {
        speed = Random.Range(minSpeed, maxSpeed);
    }

    void Update() {
        gameObject.transform.position = new Vector3(gameObject.transform.position.x + speed * multiplier * Time.deltaTime,
                                                    gameObject.transform.position.y,
                                                    gameObject.transform.position.z); 
    }
}
