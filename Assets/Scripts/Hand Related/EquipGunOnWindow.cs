using UnityEngine;
using System.Collections;

public class EquipGunOnWindow : MonoBehaviour {

    private PlayerHand playerHand;
    public GunObject gunObj;
    public GameObject playerCamera;

    private bool _gunEquipped = true;
    private float _equipThresholdY = -0.2f;

    void Awake() {
        playerHand = GetComponentInChildren<PlayerHand>();
    }

	// Use this for initialization
	void Start () {
        
        EquipGun();
        playerHand.GrabAnObject(gunObj);
	}
	
	// Update is called once per frame
	void Update () {
        if (!_gunEquipped && playerCamera.transform.localPosition.y > _equipThresholdY) EquipGun();
        else if (_gunEquipped && playerCamera.transform.localPosition.y <= _equipThresholdY) UnEquipGun();
	}

    void EquipGun() {
        gunObj.ActivateGun();
        _gunEquipped = true;
        
        
    }

    void UnEquipGun() {

        _gunEquipped = false;
        gunObj.DeactivateGun();

    }
}
