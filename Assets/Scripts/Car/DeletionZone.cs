using UnityEngine;
using System.Collections;

public class DeletionZone : MonoBehaviour {

    void OnTriggerExit(Collider other) {
        Destroy(other.gameObject);
    }
}
