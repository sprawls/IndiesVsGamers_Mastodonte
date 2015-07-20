using UnityEngine;
using System.Collections;

public class CloseGate : MonoBehaviour {

    public GameObject gate;
    public float startPos;
    public float endPos;
    public float speed;
    public float delayBeforeEnd = 0;

    void Start() {
        StartCoroutine(CloseGateAnim());
    }

    IEnumerator CloseGateAnim() {
        while (gate.transform.position.x < endPos) {
            gate.transform.position += Vector3.right * speed * Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }
    }
}
