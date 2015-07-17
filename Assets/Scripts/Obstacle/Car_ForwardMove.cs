using UnityEngine;
using System.Collections;

public class Car_ForwardMove : MonoBehaviour {

    public float Speed = 10;

    void Update() {
        gameObject.transform.position = new Vector3(gameObject.transform.position.x,
                                                    gameObject.transform.position.y,
                                                    gameObject.transform.position.z + Speed * Time.deltaTime); 
    }
}
