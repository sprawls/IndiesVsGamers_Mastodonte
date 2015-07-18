using UnityEngine;
using System.Collections;



public class FollowsObject : MonoBehaviour {

    public Transform ObjToFollow;

    void LateUpdate() {
        transform.position = ObjToFollow.position;
    }
}
