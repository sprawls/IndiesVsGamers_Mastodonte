using UnityEngine;
using System.Collections;

public class defilerText : MonoBehaviour {

    public float speed = 15f;
	
	// Update is called once per frame
	void Update () {
        transform.Translate(new Vector3(0, speed, 0) * Time.deltaTime);
	}
}
