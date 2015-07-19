using UnityEngine;
using System.Collections;

public class wipe : MonoBehaviour {

    public void Wipe() {
        StartCoroutine(WipeCoroutine());
    }

    IEnumerator WipeCoroutine() {
        Quaternion s = Quaternion.identity;
        Quaternion e = Quaternion.Euler(0,0,90);

        for (float i = 0f; i < 1f; i += Time.deltaTime / 0.6f) {
            transform.localRotation = Quaternion.Slerp(s, e, i);
            yield return null;
        }

        for (float i = 0f; i < 1f; i += Time.deltaTime / 0.6f) {
            transform.localRotation = Quaternion.Slerp(e, s, i);
            yield return null;
        }

        transform.localRotation = s;
    }
}
