using UnityEngine;
using System.Collections;

public class PedestrianMoveForward : MonoBehaviour {

    public float multiplier = 1;
    public float minSpeed;
    public float maxSpeed;
    private float speed;
    private bool sirensOn;
    public float rotateSpeed;
    private Vector3 mouvementVector;
    public bool isCar;
    private float jumpMin;
    private float jumpMax;
    private float jumpSpeed;

    void Awake() {
        speed = Random.Range(minSpeed, maxSpeed);
        mouvementVector = new Vector3(speed * Time.deltaTime, 0, 0);
        jumpSpeed = 4;
        jumpMin = transform.position.y;
        jumpMax = jumpMin + 4;
    }

    void Update() {
        gameObject.transform.position += mouvementVector * multiplier;
    }
}
