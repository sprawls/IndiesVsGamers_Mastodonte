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

    private float _jumpHeight = 1f;
    private float _jumpInTime = 0.25f;
    private float _jumpOutTime = 0.4f;

    private MeshRenderer modelMesh;

    void Awake() {
        speed = Random.Range(minSpeed, maxSpeed);
        mouvementVector = new Vector3(speed * Time.deltaTime, 0, 0);

        modelMesh = GetComponentInChildren<MeshRenderer>();
        StartCoroutine(Hop());
    }

    void Update() {
        gameObject.transform.position += mouvementVector * multiplier;

    }

    void OnDestroy() {
        StopAllCoroutines();
    }

    IEnumerator Hop() {
        Vector3 s = modelMesh.transform.localPosition;
        Vector3 e = modelMesh.transform.localPosition + new Vector3(0, _jumpHeight, 0);

        while (true) {

            for (float i = 0f; i < 1f; i += Time.deltaTime / _jumpInTime) {
                modelMesh.transform.localPosition = Vector3.Slerp(s, e, Mathf.SmoothStep(0, 1, i));
                yield return null;
            }

            for (float i = 0f; i < 1f; i += Time.deltaTime / _jumpOutTime) {
                modelMesh.transform.localPosition = Vector3.Slerp(e, s, Mathf.SmoothStep(0, 1, i));
                yield return null;
            }
        }
    }
}
