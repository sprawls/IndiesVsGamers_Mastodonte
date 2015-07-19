using UnityEngine;
using System.Collections;

public class RandomTranslateX : MonoBehaviour {

    public float minX = 10f;
    public float maxX = 15f;

    public float rateX = 1f;

	// Use this for initialization
	void Start () {
        float newX = Random.Range(minX,maxX);
        transform.position += new Vector3(newX, 0, 0);
	}
	
	// Update is called once per frame
	void Update () {
        transform.position += new Vector3(rateX,0,0) * Time.deltaTime;
	}
}
