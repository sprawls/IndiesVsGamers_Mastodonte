using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Inventory {
    public List<UpgradeType> gunUpgrade = new List<UpgradeType>();
    //List of possible upgrade
    public enum UpgradeType { }

    public float currentAmmo = 0;



    public void AddToGunUpgrade(GunUpgrade upgrade) {
        
        gunUpgrade.Add(upgrade.type);
    }
}
