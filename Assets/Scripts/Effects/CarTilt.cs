using UnityEngine;
using System.Collections;

public class CarTilt : MonoBehaviour {

    public float maxTiltRoll = 0.5f;
    public float maxTiltYaw = 1f;
    public float tiltSpeed = 0.1f;

    public void Tilt(float input) {
        float speedMultiplier = (input + 1) / 2; // (input + 1) / 2 to make the input value from -1 to 1 to a scale of 0 to 1
        //Calculate Roll
        float newTilt_roll = -Mathf.Lerp(-maxTiltRoll, maxTiltRoll, speedMultiplier);
        //Calculate Yaw
        float newTilt_yaw = Mathf.Lerp(-maxTiltYaw, maxTiltYaw, speedMultiplier);

        //Apply new Tilt
        transform.localRotation = Quaternion.Euler(new Vector3(0, newTilt_yaw, newTilt_roll));
    }
}
