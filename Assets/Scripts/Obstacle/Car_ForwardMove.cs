using UnityEngine;
using System.Collections;

public class Car_ForwardMove : MonoBehaviour {

    public float Speed = 10;
    Vector3 direction;

    void Awake() {
        direction = Vector3.forward;
    }

    void Update() {
        gameObject.transform.position += direction * Speed * Time.deltaTime;
    }

    public void SirensNearby() {
        StartCoroutine(Flee());
    }

    IEnumerator Flee() {
        float count = 0;
        Vector3 rotationChange = new Vector3(0, 55f, 0);
        GetComponent<Obstacle>().knockInTheAir = true;

        while (count < 2f) {
            direction = transform.forward;
            transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles + rotationChange * Time.deltaTime);
            count += Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }
    }
}
