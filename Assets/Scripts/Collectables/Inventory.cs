using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Inventory : MonoBehaviour {
    public List<Collectable> collectables = new List<Collectable>();

    public void AddToInventory(Collectable collectable) {
        collectables.Add(collectable);
    }
}
