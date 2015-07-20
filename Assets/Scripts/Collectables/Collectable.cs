using UnityEngine;
using System.Collections;

public class Collectable : MonoBehaviour {
    
    public int pointsGiven;
    public int amtAmmo;
    public GameObject scoreParticles;

    public void OnPickUp() {
        GameManager.instance.scoreSystem.AddScore(pointsGiven, gameObject, new Vector3(0,3,0));
        GameManager.instance.inventory.currentAmmo += amtAmmo;
        GameObject go = (GameObject)Instantiate(scoreParticles, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }
}
