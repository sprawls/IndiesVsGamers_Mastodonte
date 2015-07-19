using UnityEngine;
using System.Collections;

public class PickupCollectiable : MonoBehaviour {

    void OnTriggerEnter(Collider other) {
        if (other.GetComponent<GunUpgrade>() != null) {
            GameManager.instance.inventory.AddToGunUpgrade(other.GetComponent<GunUpgrade>());
            other.GetComponent<GunUpgrade>().OnPickUp();
        }
        else if (other.GetComponent<Collectable>() != null) {
            other.GetComponent<Collectable>().OnPickUp();
        }
    }
}
