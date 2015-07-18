using UnityEngine;
using System.Collections;

public class Chunck_Intersection : Chunck {

    public GameObject leftPedestrianSpawn;
    public GameObject rightPedestrianSpawn;
    public GameObject leftCarSpawn;
    public GameObject rightCarSpawn;

    public override void Spawn() {
        if (Random.Range(0, 10) == 0) { //chance to have 2 group of pedestrian
            SpawnPedestrianGroup(leftPedestrianSpawn.transform.position, false);
            SpawnPedestrianGroup(rightPedestrianSpawn.transform.position, true);
        }
        else if (Random.Range(0, 2) == 0) {
            SpawnPedestrianGroup(leftPedestrianSpawn.transform.position, false);
            SpawnCar(rightCarSpawn.transform.position, true);
        }
        else {
            SpawnPedestrianGroup(rightPedestrianSpawn.transform.position, true);
            SpawnCar(leftCarSpawn.transform.position, false);
        }
    }

    private void SpawnPedestrianGroup(Vector3 Pos, bool reversed) {
        GameObject group = Instantiate(Resources.Load("PedestrianGroup"), Pos, Quaternion.identity) as GameObject;
        for (int i = 0; i < group.transform.childCount; i++) {
            if (Random.Range(0, 6) == 0) Destroy(group.transform.GetChild(i).gameObject);
            else if (reversed) group.transform.GetChild(i).GetComponent<PedestrianMoveForward>().multiplier *= -1;
        }
    }

    private void SpawnCar(Vector3 Pos, bool reversed) {
        GameObject group = Instantiate(Resources.Load("HorizontalCar"), Pos, Quaternion.identity) as GameObject;
        if (reversed) group.GetComponent<PedestrianMoveForward>().multiplier *= -1;
    }
}
