using UnityEngine;
using System.Collections;

public class CarButton : MonoBehaviour {

    public VehiculeManager vehicle;
    public MeshRenderer meshRenderer;

    void Start() {
        meshRenderer = GetComponent<MeshRenderer>();
    }

	public virtual void buttonPressed(){

    }

    public virtual bool canBePressed() {
        return false;
    }
}
