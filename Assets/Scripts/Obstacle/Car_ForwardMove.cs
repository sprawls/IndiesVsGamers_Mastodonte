using UnityEngine;
using System.Collections;

public class Car_ForwardMove : MonoBehaviour {

    public float Speed = 10;

    void Awake() {
        gameObject.GetComponent<Rigidbody>().velocity = new Vector3(0, 0, Speed); 
    }
}
