using UnityEngine;
using System.Collections;

public class VehiculeManager : MonoBehaviour {

    [Header("Vehicule Stats")]
    public float forwardSpeed;
    public float HorizontalSpeed;
    public float speedModifier;

    [Header("Checks")]
    public float maxDistanceX;

    private CarTilt carTilt;

    void Awake() {
        carTilt = gameObject.GetComponent<CarTilt>();
        speedModifier = 0;
    }

    void Update () {
        GoForward();
        CalculateSpeedModifier(Input.GetAxis("Horizontal"));
        UpdateHorizontalPos();
    }

    #region Mouvement Controller

    void CalculateSpeedModifier(float input) {
        if (input == 0) {
            if (speedModifier < 0)
                speedModifier += 0.1f;
            if (speedModifier > 0)
                speedModifier -= 0.1f;
        }
        else {
            if (speedModifier < 1 && speedModifier > -1) {
                speedModifier += (input / 2);
            }
        }
        if (speedModifier > 1) 
            speedModifier = 1;
        else if (speedModifier < -1) 
            speedModifier = -1;
        else if (speedModifier > -0.15f && speedModifier < 0.15f) 
            speedModifier = 0;
    }

    void GoForward() {
        gameObject.transform.position = new Vector3(gameObject.transform.position.x,
                                                    gameObject.transform.position.y,
                                                    gameObject.transform.position.z + forwardSpeed * Time.deltaTime);
    }

    void UpdateHorizontalPos() {
        if (speedModifier < 0) {
            gameObject.transform.position = new Vector3(Mathf.Clamp(gameObject.transform.position.x + HorizontalSpeed * speedModifier * Time.deltaTime, -maxDistanceX, maxDistanceX),
                                                        gameObject.transform.position.y,
                                                        gameObject.transform.position.z);
            carTilt.Tilt(speedModifier);
        }
        else if (speedModifier > 0) {
            gameObject.transform.position = new Vector3(Mathf.Clamp(gameObject.transform.position.x + HorizontalSpeed * speedModifier * Time.deltaTime, -maxDistanceX, maxDistanceX),
                                                        gameObject.transform.position.y,
                                                        gameObject.transform.position.z);
            carTilt.Tilt(speedModifier);
        }
    }

    #endregion

    #region Collision Test

    void OnCollisionEnter(Collision other) {
		//transform.FindChild("Main Camera").GetComponent<CameraShake>().DoShake();

    }

    #endregion

    #region Gyrophares

    #endregion
}
