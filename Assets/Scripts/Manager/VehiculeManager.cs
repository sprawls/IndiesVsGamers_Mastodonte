using UnityEngine;
using System.Collections;

public class VehiculeManager : MonoBehaviour {

    [Header("Vehicule Stats")]
    public float forwardSpeed;
    public float horizontalSpeed;

    void Update () {
        GoForward();
        UpdateHorizontalPos(Input.GetAxis("Horizontal"));
    }

    void GoForward() {
        gameObject.transform.position = new Vector3(gameObject.transform.position.x,
                                                    gameObject.transform.position.y,
                                                    gameObject.transform.position.z + forwardSpeed * Time.deltaTime);
    }

    void UpdateHorizontalPos(float inputMultiplier) {
        gameObject.transform.position = new Vector3(gameObject.transform.position.x + horizontalSpeed * Time.deltaTime * inputMultiplier,
                                                    gameObject.transform.position.y,
                                                    gameObject.transform.position.z);
    }
}
