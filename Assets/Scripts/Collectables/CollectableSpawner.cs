using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CollectableSpawner : MonoBehaviour {
    List<Inventory.UpgradeType> possibleUpgradeToSpawn = new List<Inventory.UpgradeType>();
    public int NumberOfUpgradeRatio = 5;

    public void OnReset() {
        foreach (Inventory.UpgradeType upgrade in System.Enum.GetValues(typeof(Inventory.UpgradeType))) {
            possibleUpgradeToSpawn.Add(upgrade);
        }
    }

    private void SpawnLogic() {
        int random = Random.Range(0, NumberOfUpgradeRatio);
        for(int i=0; i < random; i++) {
            if (possibleUpgradeToSpawn.Count != 0) ChoosePossibleUpgradeToSpawn();
        }
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
