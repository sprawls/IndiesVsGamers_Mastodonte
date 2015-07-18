using UnityEngine;
using System.Collections;

public class FlyerSpawn : MonoBehaviour {

    public GameObject spawnObject;
    private GameObject player;
    private Vector3 posLock;

    void Start() {
        player = GameObject.FindGameObjectWithTag("Player");
        posLock = player.transform.position;
    }

    void Update() {
        transform.position = new Vector3(posLock.x, posLock.y, player.transform.position.z);
    }

    void CheckSpawn() {
        if (Random.Range(0, 60) == 0) {
            
        }
    }
}
