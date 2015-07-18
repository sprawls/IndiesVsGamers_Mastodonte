using UnityEngine;
using System.Collections;

public class breahingAnim : MonoBehaviour {

    public float ExpansionFactor = 0.01f;
    public float breathTime = 1.5f;

    private Vector3 startScale;
    private Vector3 endScale;

	// Use this for initialization
	void Start () {
        startScale = transform.localScale;
        endScale = startScale * (1f-ExpansionFactor);
        StartCoroutine(Breathe());
	}

    IEnumerator Breathe() {
        while (true) {
            for(float i = 0 ; i < 1f; i += Time.deltaTime /breathTime ){
                transform.localScale = Vector3.Lerp(startScale,endScale,Mathf.Lerp(0f,1f,i));
                yield return null;
            }

            for (float i = 0; i < 1f; i += Time.deltaTime / breathTime) {
                transform.localScale = Vector3.Lerp(endScale, startScale, Mathf.Lerp(0f, 1f, i));
                yield return null;
            }
        }
    }
}
