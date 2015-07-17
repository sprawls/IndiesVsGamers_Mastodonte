using UnityEngine;
using System.Collections;

public abstract class GrabbableObject : MonoBehaviour {

    public bool canBeDropped = true;
    public bool canBeUsed = false;

    public abstract void Grab(Transform grabAnchor, Rigidbody rb);

    public abstract void Release();

    public abstract bool Use();

    public abstract void ForceRelease();
}
