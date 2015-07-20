using UnityEngine;
using System.Collections;

public class Collectable : MonoBehaviour {
    
    public int pointsGiven;
    public int amtAmmo;

    public void OnPickUp() {
        GameManager.instance.scoreSystem.AddScore(pointsGiven,gameObject, new Vector3(0,1,0), false);
        GameManager.instance.inventory.currentAmmo += amtAmmo;
        Destroy(gameObject);
    }
}
