using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Enemy_Manager : MonoBehaviour{

    public int maxHealth;
    private int health;
    public float horizontalSpeed;
    private bool vehicleInFront;
    CarTilt carTilt;
    private bool inRightLane;
    [Header("Lane")]
    public float leftLaneX;
    public float rightLaneX;

    enum State { DodgingCar, RandomPathing }
    State activeState;

    void Awake() {
        health = maxHealth;
        vehicleInFront = false;
        carTilt = gameObject.GetComponent<CarTilt>();
        SwitchState(State.RandomPathing);
        inRightLane = true;
    }

    #region Utility Function

    public void TakeDamage(int damageTaken) {
        if (health - damageTaken <= 0) Death();
        else health -= damageTaken;
        
    }

    void Death() {
        Destroy(gameObject);
    }

    #endregion

    #region AI

    bool IsFrontEmptyOfCar() { return !vehicleInFront; }

    IEnumerator SwitchLane(bool goingToRightLane) {
        if (!goingToRightLane) {
            while (transform.position.x > leftLaneX) {
                gameObject.transform.position = new Vector3(gameObject.transform.position.x - horizontalSpeed * Time.deltaTime,
                                                     gameObject.transform.position.y,
                                                     gameObject.transform.position.z);
                carTilt.Tilt(-1);
                inRightLane = false;
                yield return new WaitForEndOfFrame();
            }
        }
        else {
            while (transform.position.x < rightLaneX) {
                gameObject.transform.position = new Vector3(gameObject.transform.position.x + horizontalSpeed * Time.deltaTime,
                                                     gameObject.transform.position.y,
                                                     gameObject.transform.position.z);
                carTilt.Tilt(1);
                inRightLane = true;
                yield return new WaitForEndOfFrame();
            }
        }
        carTilt.Tilt(0);
    }

    IEnumerator DodgeState() {
        Coroutine switchLane = StartCoroutine(SwitchLane(!inRightLane));
        yield return switchLane;
        SwitchState(State.RandomPathing);
    }

    IEnumerator RandomPathingState() {
        Coroutine laneSwitch;
        while (true) {
            if (!IsFrontEmptyOfCar()) {
                SwitchState(State.DodgingCar);
            }

            if (Random.Range(0, 5f/Time.deltaTime) == 0) {
                laneSwitch = StartCoroutine(SwitchLane(!inRightLane));
                yield return laneSwitch;
            }
            yield return new WaitForEndOfFrame();
        }
    }

    void SwitchState(State newState) {
        StopAllCoroutines();
        activeState = newState;
        switch (newState) {
            case State.RandomPathing: StartCoroutine(RandomPathingState()); break;
            case State.DodgingCar: StartCoroutine(DodgeState()); break;
        }
    }

    void OnTriggerEnter(Collider other) {
        if (other.transform.root.tag == "Obstacle" && other.transform.root.GetComponent<Obstacle>().type == Obstacle.Type.car) {
            vehicleInFront = true;
        }
    }

    void OnTriggerExit(Collider other) {
        if (other.transform.root.tag == "Obstacle" && other.transform.root.GetComponent<Obstacle>().type == Obstacle.Type.car) vehicleInFront = false;
    }

    #endregion
}
