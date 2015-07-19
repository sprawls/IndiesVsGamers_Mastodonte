using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MapGeneration : MonoBehaviour {

    //PARAMETERS
    enum ChunckType { normal, intersection }
    public float distanceNextChunck;
    public GameObject startingChunck;
    public int numberOfChuncksLoaded;
    [Header("Ratio (1/n)")]
    public int intersectionChunckRation;
 
    //VARIABLES
    public List<GameObject> chunckList = new List<GameObject>();
    private Vector3 currentChunckPos;

    void Start() {
        currentChunckPos = startingChunck.transform.position;
        chunckList.Add(startingChunck);
        
        //First Load
        for (int i = 0; i < numberOfChuncksLoaded; i++) {
            CreateChunck(ChooseChunckToLoad());
        }
    }

    void Update() {
        if (chunckList.Count < numberOfChuncksLoaded) {
            CreateChunck(ChooseChunckToLoad());
        }
    }

    ChunckType ChooseChunckToLoad() {
        if (Random.Range(0, intersectionChunckRation) == 0) return ChunckType.intersection;
        return ChunckType.normal;
    }

    void CreateChunck(ChunckType type) {
        GameObject chunck = null;
        currentChunckPos = new Vector3(currentChunckPos.x, currentChunckPos.y, currentChunckPos.z + distanceNextChunck);

        switch (type) {
            case ChunckType.normal: chunck = ChooseNormalToSpawn(); break;
            case ChunckType.intersection: chunck = Instantiate(Resources.Load("Chunck_Intersection"), currentChunckPos, Quaternion.identity) as GameObject; break;
        }

        chunckList.Add(chunck);
        chunck.GetComponent<Chunck>().Spawn();
    }

    GameObject ChooseNormalToSpawn() {
        switch (Random.Range(0, 4)) {
            case 0: return Instantiate(Resources.Load("Chunck_Normal1"), currentChunckPos, Quaternion.identity) as GameObject;
            case 1: return Instantiate(Resources.Load("Chunck_Normal2"), currentChunckPos, Quaternion.identity) as GameObject;
            case 2: return Instantiate(Resources.Load("Chunck_Normal3"), currentChunckPos, Quaternion.identity) as GameObject;
            default: return Instantiate(Resources.Load("Chunck_Normal4"), currentChunckPos, Quaternion.identity) as GameObject;
        }
    }
}
