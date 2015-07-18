using UnityEngine;
using System.Collections;

public class EquipGunOnWindow : MonoBehaviour {

    private PlayerHand playerHand;
    public GunObject gunObj;

    void Awake() {
        playerHand = GetComponentInChildren<PlayerHand>();
    }

	// Use this for initialization
	void Start () {
        playerHand.GrabAnObject(gunObj);
        //gunObj.Grab(playerHand.grabAnchor.transform, playerHand._modelRB);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
