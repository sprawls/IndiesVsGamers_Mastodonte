using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class AlarmEffect : MonoBehaviour {

    List<GameObject> target = new List<GameObject>();

    void OnTriggerStay(Collider other) {
        Car_ForwardMove obstacle = other.GetComponentInParent<Car_ForwardMove>();
        if (obstacle != null && !target.Contains(obstacle.gameObject)) {
            target.Add(obstacle.gameObject);
            obstacle.SirensNearby();
        }
    }
}
