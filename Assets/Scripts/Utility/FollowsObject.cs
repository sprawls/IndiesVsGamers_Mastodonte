using UnityEngine;
using System.Collections;



public class FollowsObject : MonoBehaviour {

    public Transform ObjToFollow;

    void Update() {
        transform.position = ObjToFollow.position;
    }
}
