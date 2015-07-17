using UnityEngine;
using System.Collections;

public class DeletionWall : MonoBehaviour {

    MapGeneration generation;

    void Awake() {
        generation = GameObject.Find("Level_Manager").GetComponent<MapGeneration>();
    }

    void OnTriggerEnter(Collider other) {
        if (other.tag == "Chunck")
            generation.DeleteChunk(other.gameObject);
        else 
            Destroy(other.gameObject);
    }
}
