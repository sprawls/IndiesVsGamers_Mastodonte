using UnityEngine;
using System.Collections;

public class DezoomCamera : MonoBehaviour {
	public float distanceBillboard;
	public float speedDezoom;
	public GameObject pressStart;

	float distanceTravel;
	float startTime;

	Vector3 endPosition;
	Vector3 startPosition;
	bool zoomer = true;
	float distCovered;
	float fracJourney;

	// Use this for initialization
	void Start () {
		endPosition = new Vector3 (transform.position.x, transform.position.y, distanceBillboard);
		startPosition = transform.position;
		distanceTravel = Vector3.Distance (startPosition, endPosition);
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.anyKey && zoomer) {
			startTime = Time.time;
			StartCoroutine("dezoom");
			zoomer = false;
			pressStart.SetActive(false);
		}
	}

	IEnumerator dezoom() {
		while (transform.position.z > endPosition.z + 0.05f) {
			distCovered = (Time.time -startTime) * speedDezoom;
			fracJourney = distCovered / distanceTravel;
			transform.position = Vector3.Lerp(startPosition, endPosition, fracJourney);
			yield return null;
		}

		yield return null;
	}
}
