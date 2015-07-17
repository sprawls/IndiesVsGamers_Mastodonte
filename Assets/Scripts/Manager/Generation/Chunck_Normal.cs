using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Chunck_Normal : Chunck {

    [Header("Ratio -> {1/n})")]
    public int VehicleInParkingRatio;
    public int VehicleInLaneRatio;

    [Header("Spawns")]
    public GameObject[] parkingSpawn;
    public GameObject[] leftLaneSpawn;
    public GameObject[] rightLaneSpawn;

    public override void Spawn() {
        SpawnInParking();
        SpawnInLane();
    }

    void SpawnInParking() {
        for (int i = 0; i < parkingSpawn.Length; i++) {
            if (Random.Range(0, VehicleInParkingRatio) == 0) {
                Instantiate(Resources.Load("ParkedCar"), parkingSpawn[i].transform.position, Quaternion.identity);
            }
        }
    }

    void SpawnInLane() { //Only one can spawn per chunck
        if (Random.Range(0, VehicleInLaneRatio) == 0) {
            if (Random.Range(0, 2) == 0) {
                Instantiate(Resources.Load("Car"), leftLaneSpawn[Random.Range(0, leftLaneSpawn.Length)].transform.position, Quaternion.identity);
            }
            else {
                Instantiate(Resources.Load("Car"), rightLaneSpawn[Random.Range(0, rightLaneSpawn.Length)].transform.position, Quaternion.identity);
            }
        }
    }
}
