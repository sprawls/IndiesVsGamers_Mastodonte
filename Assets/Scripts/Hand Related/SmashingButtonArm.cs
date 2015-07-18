using UnityEngine;
using System.Collections;

public class SmashingButtonArm : MonoBehaviour {


    public Vector3 mouvement = new Vector3(0,0,5);
    public float inTime = 0.2f;
    public float idleTime = 0.3f;
    public float outTime = 0.4f;

	void Start () {
        transform.localPosition -= mouvement;
        StartCoroutine(Translate());
	}

    void OnDestroy() {
        StopAllCoroutines();
    }

    IEnumerator Translate() {

        Vector3 startPos = transform.localPosition;
        Vector3 endPos = transform.localPosition + mouvement;

        for (float i = 0f; i < 1f; i += Time.deltaTime / inTime) {
            transform.localPosition = Vector3.Lerp(startPos, endPos, Mathf.SmoothStep(0,1,i));
            yield return null;
        }

        yield return new WaitForSeconds(idleTime);

        for (float i = 0f; i < 1f; i += Time.deltaTime / outTime) {
            transform.localPosition = Vector3.Lerp(endPos, startPos, Mathf.SmoothStep(0, 1, i));
            yield return null;
        }
        for (float i = 0f; i < 1f; i += Time.deltaTime / outTime) {
            transform.localPosition = Vector3.Lerp(startPos, startPos - mouvement, Mathf.SmoothStep(0, 1, i));
            yield return null;
        }
    }
}
