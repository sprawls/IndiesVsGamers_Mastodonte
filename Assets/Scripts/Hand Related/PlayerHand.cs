using UnityEngine;
using System.Collections;

public class PlayerHand : MonoBehaviour {

    [Header("Variables to Set In Inspector : ")]
    public GameObject model;
    public float Ypos_Idle;
    public float Ypos_Grab;
    public float sensibility = 10f;
    public LayerMask HandCollisionPlane;

    [Header("State Variables (for debug): ")]
    public bool isGrabbing = false;


    private Vector3 _startModelPos;
    private Vector3 _mousePosition;
    private Rigidbody _modelRB;
    

	void Start () {
        _startModelPos = model.transform.localPosition;
        _modelRB = GetComponentInChildren<Rigidbody>();
    }
	

	void Update () {
        UpdateGrabbing();
        GetMousePosition();
        UpdateModelPosition();
	}

    void UpdateGrabbing() {
        if(Input.GetMouseButton(0)) isGrabbing = true;
        else isGrabbing = false;
    }

    void GetMousePosition() { 
        Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, 100, HandCollisionPlane)) {
            Debug.DrawLine(ray.origin, hit.point);
            _mousePosition = hit.point;
        }

    }

    void UpdateModelPosition() {
        //Get target Height
        float Ypos;
        if (isGrabbing) Ypos = Ypos_Grab;
        else Ypos = Ypos_Idle;

        Vector3 targetPos = new Vector3(    _mousePosition.x,
                                            Ypos,
                                            _mousePosition.z) + _startModelPos;

        _modelRB.velocity = (targetPos - _modelRB.transform.position) * sensibility;
    }
}
