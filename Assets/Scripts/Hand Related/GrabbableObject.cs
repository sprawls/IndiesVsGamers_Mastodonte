using UnityEngine;
using System.Collections;

public abstract class GrabbableObject : MonoBehaviour {

    [HideInInspector] public bool canBeDropped = true;
    [HideInInspector] public bool canBeUsed = false;

    public abstract void Grab(Transform grabAnchor, Rigidbody rb);

    public abstract void Release();

    public abstract bool Use();

    public abstract void ForceRelease();
}
