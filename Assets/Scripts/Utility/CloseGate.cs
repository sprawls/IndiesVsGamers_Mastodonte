using UnityEngine;
using System.Collections;

public class CloseGate : MonoBehaviour {

    public float startPos;
    public float endPos;
    public float speed;
    public float delayBeforeEnd = 0;

    void Start() {
        StartCoroutine(CloseGateAnim());
    }

    IEnumerator CloseGateAnim() {
        yield return new WaitForSeconds(delayBeforeEnd);
        while (transform.position.x < endPos) {
            transform.position += Vector3.right * speed * Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }
    }
}
