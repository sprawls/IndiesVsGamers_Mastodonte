using UnityEngine;
using System.Collections;

public class PlayerHand : MonoBehaviour {

    [Header("Variables to Set In Inspector : ")]
    public GameObject model;
    public SphereCollider grabAnchor;
    public float Ypos_Idle;
    public float Ypos_Grab;
    public float sensibility = 10f;
    public LayerMask HandCollisionPlane;
    public LayerMask PhysicsObject;

    [Header("State Variables (for debug): ")]
    public bool armIsLowered = false;
    public bool isGrabbing = false;


    private Vector3 _startModelPos;
    private Vector3 _mousePosition;
    public Rigidbody _modelRB;
    public GrabbableObject _grabbedObject;


    void Awake() {
        Cursor.visible = false;
    }

	void Start () {
        _startModelPos = model.transform.localPosition;
        _modelRB = GetComponentInChildren<Rigidbody>();
    }
	

	void Update () {
        UpdateGrabbingBool();
        GetMousePosition();
        UpdateModelPosition();
        if (armIsLowered && !isGrabbing) AttemptToGrab();
        else if (isGrabbing && Input.GetMouseButtonDown(1)) DropObject();
	}

    void UpdateGrabbingBool() {
        if(Input.GetMouseButton(0)) armIsLowered = true;
        else armIsLowered = false;
    }

    void GetMousePosition() { 
        Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, 100, HandCollisionPlane)) {
            //Debug.DrawLine(ray.origin, hit.point);
            _mousePosition = hit.point;
        }

    }

    void UpdateModelPosition() {
        //Get target Height
        float Ypos;
        if (armIsLowered) Ypos = Ypos_Grab;
        else Ypos = Ypos_Idle;

        Vector3 targetPos = new Vector3(    _mousePosition.x,
                                            Ypos,
                                            _mousePosition.z) + _startModelPos;

        _modelRB.velocity = (targetPos - _modelRB.transform.position) * sensibility;
    }

    void AttemptToGrab() {
        Collider[] hitColliders = Physics.OverlapSphere(grabAnchor.transform.position, grabAnchor.radius, PhysicsObject);
        for (int i = 0; i < hitColliders.Length; i++) {
            //Debug.Log("object in grab coll : " + hitColliders[i]);
            GrabbableObject grabbedObject = hitColliders[i].GetComponentInParent<GrabbableObject>();
            if (grabbedObject != null) {
                GrabAnObject(grabbedObject);
                break;
            }
           
        }
       
    }

    void GrabAnObject(GrabbableObject grabbedObject) {
        isGrabbing = true;
        _grabbedObject = grabbedObject;
        _grabbedObject.Grab(grabAnchor.transform, _modelRB);
    }

    void DropObject() {
        isGrabbing = false;
        _grabbedObject.Release();
        _grabbedObject = null;
    }

    //Debug function for grab collider
    void OnDrawGizmos() {
        Gizmos.DrawSphere(grabAnchor.transform.position, grabAnchor.radius);
    }
}
