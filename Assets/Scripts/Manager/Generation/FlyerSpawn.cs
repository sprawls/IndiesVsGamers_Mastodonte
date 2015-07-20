using UnityEngine;
using System.Collections;

public class FlyerSpawn : MonoBehaviour {

    private GameObject player;
    private Vector3 posLock;

    void Start() {
        player = GameObject.FindGameObjectWithTag("Player");
        posLock = player.transform.position;
    }

    void Update() {
        transform.position = new Vector3(posLock.x, posLock.y + 35, player.transform.position.z - 10);
        CheckSpawn();
    }

    void CheckSpawn() {
        if (Random.Range(0, 900) == 0) {
            Instantiate(Resources.Load("Flyer"), transform.position + new Vector3(Random.Range(-15f, 15f), 0, 0), Quaternion.identity);
        }
    }
}
