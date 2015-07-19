using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CollectableSpawner : MonoBehaviour {
    public GameObject[] spawnLocation;
    public List<GameObject> possibleSpawn;
    List<Inventory.UpgradeType> possibleUpgradeToSpawn = new List<Inventory.UpgradeType>();
    List<Collectable> collectable = new List<Collectable>();
    public int NumberOfUpgradeRatio = 2;
    public int MaxNumberCollectable = 10;

    public void Awake() {
        spawnLocation = GameObject.FindGameObjectsWithTag("Spawn");
        OnReset();
    }

    public void OnReset() {
        possibleUpgradeToSpawn.Clear();
        possibleSpawn = new List<GameObject>(spawnLocation);
        foreach (Inventory.UpgradeType upgrade in System.Enum.GetValues(typeof(Inventory.UpgradeType))) {
            possibleUpgradeToSpawn.Add(upgrade);
        }
        SpawnLogic();
    }

    public Vector3 RandomPos() {
        int random = Random.Range(0, possibleSpawn.Count);
        GameObject spawn = possibleSpawn[random];
        possibleSpawn.RemoveAt(random);
        return spawn.transform.position;
    }

    private void SpawnLogic() {
        int count = MaxNumberCollectable - SpawnUpgrade();
        for (int i = 0; i < count; i++) {
            Instantiate(collectable[Random.Range(0, collectable.Count)], RandomPos(), Quaternion.identity);
        }
    }

    private int SpawnUpgrade() {
        int random = Random.Range(0, NumberOfUpgradeRatio);
        for (int i = 0; i < random; i++) {
            if (possibleUpgradeToSpawn.Count != 0) {
                switch (ChoosePossibleUpgradeToSpawn()) {
                    //Upgrades Instantiate
                }
            }
            else {
                return i + 1;
            }
        }
        return random;
    }

    private Inventory.UpgradeType ChoosePossibleUpgradeToSpawn() {
        foreach (Inventory.UpgradeType upgrade in GameManager.instance.inventory.gunUpgrade) {
            possibleUpgradeToSpawn.Remove(upgrade);
        }

        Inventory.UpgradeType temp = possibleUpgradeToSpawn[Random.Range(0, possibleUpgradeToSpawn.Count)];
        possibleUpgradeToSpawn.Add(temp);
        return temp;
    }
}
