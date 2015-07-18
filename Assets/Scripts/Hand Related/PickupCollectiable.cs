using UnityEngine;
using System.Collections;

public class PickupCollectiable : MonoBehaviour {

    void OnTriggerEnter(Collider other) {
        if (other.GetComponent<Collectable>() != null) {
            GameManager.instance.inventory.AddToInventory(other.GetComponent<Collectable>());
        }
    }
}
