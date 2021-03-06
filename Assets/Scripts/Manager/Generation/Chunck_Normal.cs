﻿using UnityEngine;
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

    [Header("Type of Cars")]
    public Material[] carMat;

    public override void Spawn() {
        SpawnInParking();
        SpawnInLane();
    }

    void SpawnInParking() {
        for (int i = 0; i < parkingSpawn.Length; i++) {
            if (Random.Range(0, VehicleInParkingRatio) == 0) {
                GameObject temp = Instantiate(Resources.Load("ParkedCar"), parkingSpawn[i].transform.position, Quaternion.identity) as GameObject;
                if (parkingSpawn[i].transform.position.x < 0) temp.transform.localScale = new Vector3(1, 1, -1);
                RandomizeCarMat(temp);
            }
        }
    }

    void SpawnInLane() { //Only one can spawn per chunck
        if (Random.Range(0, VehicleInLaneRatio) == 0) {
            if (Random.Range(0, 2) == 0) {
                GameObject temp = Instantiate(Resources.Load("Car"), leftLaneSpawn[Random.Range(0, leftLaneSpawn.Length)].transform.position, Quaternion.identity) as GameObject;
                temp.GetComponent<Car_ForwardMove>().Speed *= -1;
                temp.transform.localScale = new Vector3(1, 1, -1);
                RandomizeCarMat(temp);
            }
            else {
                RandomizeCarMat(Instantiate(Resources.Load("Car"), rightLaneSpawn[Random.Range(0, rightLaneSpawn.Length)].transform.position, Quaternion.identity) as GameObject);
            }
        }
    }

    void RandomizeCarMat(GameObject car) {
        if (car.transform.FindChild("OtherCar") != null) {
            car.transform.FindChild("OtherCar").GetComponent<MeshRenderer>().material = carMat[Random.Range(0, carMat.Length)];
        }
        else if (car.transform.FindChild("CarModel") != null) {
            car.transform.FindChild("CarModel").GetComponent<MeshRenderer>().material = carMat[Random.Range(0, carMat.Length)];
        }
    }
}
