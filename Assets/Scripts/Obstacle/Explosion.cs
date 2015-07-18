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
    }

	public void BOOM () {
        GetComponent<Collider>().enabled = true;
        Destroy(gameObject, 0.1f);
    }

    void OnTriggerStay(Collider other) {
        if (other.tag == "Obstacle" && !target.Contains(other.gameObject)) {
            target.Add(other.gameObject);
            other.gameObject.GetComponent<Rigidbody>().AddExplosionForce(explosionForce, gameObject.transform.position, radius);
        }
    }
}
