using UnityEngine;
using System.Collections;

public class NormalObject : GrabbableObject {

    public Rigidbody rbToDeactivate;

    private Transform originalParent;

    void Start() {
        originalParent = transform.parent;
        gameObject.layer = 9; //Physics Object
    }

    public override void Grab() {

    }

    public override void Release() {
        if (canBeDropped == false) return;
        transform.parent = originalParent;
    }

    public override void Use() {

    }

    public override void ForceRelease() {

    }

}
