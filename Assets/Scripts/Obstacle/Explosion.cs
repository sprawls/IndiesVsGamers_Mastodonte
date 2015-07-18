using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Explosion : MonoBehaviour {

    List<GameObject> target;
    public float explosionForce = 500;
    public float radius = 10;

    void Awake() {
        
        target = new List<GameObject>();
        GetComponent<SphereCollider>().radius = radius;
        GetComponent<SphereCollider>().enabled = false;
    }

	public void BOOM () {
        GetComponent<Collider>().enabled = true;
        Destroy(gameObject, 0.1f);
    }

    void OnTriggerStay(Collider other) {
        Obstacle obstacle = other.GetComponentInParent<Obstacle>();
        if (obstacle != null && !target.Contains(obstacle.gameObject)) {
            target.Add(obstacle.gameObject);
            other.transform.GetComponentInParent<Rigidbody>().AddExplosionForce(explosionForce, gameObject.transform.position, radius, 400f);
        }
        Enemy_Manager enemy = other.GetComponentInParent<Enemy_Manager>();
        if ( enemy != null && !target.Contains(enemy.gameObject)) {
            enemy.TakeDamage(7);
            target.Add(enemy.gameObject);
        }
    }
}
