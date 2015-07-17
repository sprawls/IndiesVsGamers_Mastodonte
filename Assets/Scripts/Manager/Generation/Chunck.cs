using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public abstract class Chunck : MonoBehaviour {

    public abstract void Spawn();

    void OnDestroy() {
        if (GameObject.Find("Level_Manager") != null) GameObject.Find("Level_Manager").GetComponent<MapGeneration>().chunckList.RemoveAt(0);
    }
}
