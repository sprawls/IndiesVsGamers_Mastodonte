using UnityEngine;
using System.Collections;

public class GoToNextScene : MonoBehaviour {

    public float idleTimeToSkip = 15f;
    public float curIdleTime = 0f;
    public float holdTimeToSkip = 2f;
    public float curHoldTime = 0f;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetMouseButton(0)) {
            curHoldTime += Time.deltaTime;
            if (curHoldTime > holdTimeToSkip) NextScene();
        } else {
            curHoldTime = 0;
        }

        curIdleTime += Time.deltaTime;
        if (curIdleTime > idleTimeToSkip) NextScene();
       
	}




    void NextScene() {
        Application.LoadLevel("Phase1");
    }
}
