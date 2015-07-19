using UnityEngine;
using System.Collections;

public class DeletionWall : MonoBehaviour {

    void OnTriggerEnter(Collider other) {
        Destroy(other.transform.root.gameObject);
    }
}
