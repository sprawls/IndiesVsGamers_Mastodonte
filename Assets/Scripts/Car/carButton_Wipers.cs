using UnityEngine;
using System.Collections;

public class carButton_Wipers : CarButton {

	 void Update() {
         if (vehicle.canWipe) {
            meshRenderer.material.color = new Color(1, 1, 1);
        } else {
            meshRenderer.material.color = new Color(0.3f, 0.3f, 0.3f);
        }
    }


    public override void buttonPressed() {
        vehicle.StartWipers();
    }

    public override bool canBePressed() {
        return vehicle.canWipe;
    }
    
}
