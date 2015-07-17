using UnityEngine;
using System.Collections;

public class InvisibleWall : MonoBehaviour {

    public bool blockLeftSide;

    void OnTriggerEnter(Collider other) {
        if (other.GetComponent<VehiculeManager>() != null) {
            if (blockLeftSide)
                other.GetComponent<VehiculeManager>().canGoLeft = false;
            else
                other.GetComponent<VehiculeManager>().canGoRight = false;
        }
    }

    void OnTriggerExit(Collider other) {
        if (other.GetComponent<VehiculeManager>() != null) {
            if (blockLeftSide)
                other.GetComponent<VehiculeManager>().canGoLeft = true;
            else
                other.GetComponent<VehiculeManager>().canGoRight = true;
        }
    }
}
