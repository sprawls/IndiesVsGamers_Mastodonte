﻿using UnityEngine;
using System.Collections;

public class PlayerHand : MonoBehaviour {

    [Header("Variables to Set In Inspector : ")]
    public GameObject model;
    public SphereCollider grabAnchor;
    public GameObject grabbedModel;
    public float Ypos_Idle;
    public float Ypos_Grab;
    public float sensibility = 10f;
    public Vector2 maxCameraMouvement;
    public Vector2 handMouvementToReachMax;
    public LayerMask HandCollisionPlane;
    public LayerMask PhysicsObject;

    [Header("State Variables (for debug): ")]
    public bool armIsLowered = false;
    public bool isGrabbing = false;
    private bool _hasGrabbedRecently = false;


    private Vector3 _startModelPos;
    public Vector3 _mousePosition;
    private Rigidbody _modelRB;
    private GrabbableObject _grabbedObject;
    private Camera _mainCam;



    void Awake() {
        Cursor.visible = false;
    }

	void Start () {
        _startModelPos = model.transform.localPosition;
        _modelRB = GetComponentInChildren<Rigidbody>();
        _mainCam = Camera.main;
        grabbedModel.SetActive(false);
    }
	

	void Update () {
        CheckLeftClick();
        GetMousePosition();
        UpdateModelPosition();
        UpdateCameraPosition();
        if (armIsLowered && !isGrabbing && !_hasGrabbedRecently) AttemptToGrab();
        else if (isGrabbing && Input.GetMouseButtonDown(1)) DropObject();
	}


    void CheckLeftClick() {
        armIsLowered = false;
        if (isGrabbing && _grabbedObject.canBeUsed) {
            if (Input.GetMouseButtonDown(0)) {
                _grabbedObject.Use();
            }
        } else {
            UpdateGrabbingBool();
        }
    }

    void UpdateGrabbingBool() {
        if(Input.GetMouseButton(0)) armIsLowered = true;
        else armIsLowered = false;
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
        if (armIsLowered) Ypos = Ypos_Grab;
        else Ypos = Ypos_Idle;

        Vector3 targetPos = new Vector3(    _mousePosition.x,
                                            Ypos,
                                            _mousePosition.z) + _startModelPos;

        _modelRB.velocity = (targetPos - _modelRB.transform.position) * sensibility;
    }

    void UpdateCameraPosition() {
        float targetX = Mathf.Lerp(0, maxCameraMouvement.x, Mathf.Lerp(0f, 1f, Mathf.Abs(_modelRB.transform.localPosition.x) / handMouvementToReachMax.x));
        targetX *= Mathf.Sign(_modelRB.transform.localPosition.x);
        Vector3 XVector = _mainCam.transform.right * targetX;
        float targetZ = Mathf.Lerp(0, maxCameraMouvement.y, Mathf.Lerp(0f, 1f, Mathf.Abs(_modelRB.transform.localPosition.z) / handMouvementToReachMax.y));
        targetZ *= Mathf.Sign(_modelRB.transform.localPosition.z);
        Vector3 ZVector = _mainCam.transform.up * targetZ;
        Vector3 targetPos = XVector + ZVector;

        _mainCam.transform.localPosition = Vector3.Lerp(_mainCam.transform.localPosition, targetPos, 0.25f);
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
        grabbedModel.SetActive(true);
        _grabbedObject = grabbedObject;
        _grabbedObject.Grab(grabAnchor.transform, _modelRB);
    }

    void DropObject() {
        isGrabbing = false;
        StartCoroutine(GrabCooldown());
        grabbedModel.SetActive(false);
        _grabbedObject.Release();
        _grabbedObject = null;
    }

    //Debug function for grab collider
    void OnDrawGizmos() {
        //Gizmos.DrawSphere(grabAnchor.transform.position, grabAnchor.radius);
    }


    IEnumerator GrabCooldown() {
        _hasGrabbedRecently = true;
        yield return new WaitForSeconds(.5f);
        _hasGrabbedRecently = false;
    }
}
