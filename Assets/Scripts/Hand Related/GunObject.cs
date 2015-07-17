using UnityEngine;
using System.Collections;

public class GunObject : GrabbableObject {

    public GameObject ShootPosition;
    public GameObject ShootLineRenderer;
    public LayerMask ShootLayerMask;

    private Transform originalParent;
    private ConfigurableJoint confJoint;
    private Rigidbody rb;
    private Collider[] colliders;

    void Awake(){
        rb = GetComponent<Rigidbody>();
        colliders = GetComponentsInChildren<Collider>();
    }
    
    void Start() {
        originalParent = transform.parent; 

        Transform[] Ts = GetComponentsInChildren<Transform>();
        foreach (Transform t in Ts) {
            t.gameObject.layer = 9; //Physics Object
        }
        canBeUsed = true;
        canBeDropped = false;

    }

    public override void Grab(Transform grabAnchor, Rigidbody rb) {
        foreach(Collider col in colliders) col.enabled = false;
        transform.parent = grabAnchor;
        transform.localPosition = Vector3.zero;
        

        confJoint = gameObject.AddComponent<ConfigurableJoint>();
        confJoint.connectedBody = rb;
        confJoint.xMotion = ConfigurableJointMotion.Locked;
        confJoint.yMotion = ConfigurableJointMotion.Locked;
        confJoint.zMotion = ConfigurableJointMotion.Locked;
        confJoint.angularXMotion = ConfigurableJointMotion.Locked;
        confJoint.angularYMotion = ConfigurableJointMotion.Locked;
        confJoint.angularZMotion = ConfigurableJointMotion.Locked;

        Transform[] Ts = GetComponentsInChildren<Transform>();
        foreach (Transform t in Ts) {
            t.gameObject.layer = 10; //Physics Hand
        }
    }

    public override void Release() {
        if (canBeDropped == false) return;

        transform.parent = originalParent;
        Destroy(confJoint);
    }

    public override bool Use() {
        Shoot();
        return true;
    }

    public override void ForceRelease() {
        foreach (Collider col in colliders) col.enabled = true;
        rb.useGravity = true;
        transform.parent = originalParent;
        Destroy(confJoint);

        Transform[] Ts = GetComponentsInChildren<Transform>();
        foreach (Transform t in Ts) {
            t.gameObject.layer = 9; //Physics Object
        }
    }

    void Shoot() {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, 100, ShootLayerMask)) {
            Debug.DrawLine(ray.origin, hit.point, Color.red, 2f);
            StartCoroutine(SpawnShootParticles(hit));
        }
    }

    IEnumerator SpawnShootParticles(RaycastHit hit) {
        LineRenderer newLineRenderer = Instantiate(ShootLineRenderer).GetComponent<LineRenderer>();
        newLineRenderer.SetPosition(0,ShootPosition.transform.position);
        newLineRenderer.SetPosition(1, hit.point);
        yield return new WaitForSeconds(0.5f);
        Destroy(newLineRenderer.gameObject);
    }

}
