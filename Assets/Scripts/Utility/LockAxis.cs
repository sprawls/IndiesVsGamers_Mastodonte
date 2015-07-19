using UnityEngine;
using System.Collections;

public class LockAxis : MonoBehaviour {

	public bool LockX = false;
    public bool LockY = false;
    public bool LockZ = false;

    Vector3 startPos;

    void Start() {
        startPos = transform.position;
    }

    void Update() {
        
        float newX = transform.position.x;
        float newY = transform.position.y;
        float newZ = transform.position.z;
        if (LockX) newX = startPos.x;
        if (LockY) newY = startPos.y;
        if (LockZ) newZ = startPos.z;

        Vector3 newPosition = new Vector3(newX, newY, newZ);
        transform.position = newPosition;

    }
}
