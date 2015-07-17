using UnityEngine;
using System.Collections;

public abstract class GrabbableObject : MonoBehaviour {

    public bool canBeDropped = true;

    public abstract void Grab(Transform grabAnchor, Rigidbody rb);

    public abstract void Release();

    public abstract void Use();

    public abstract void ForceRelease();
}
