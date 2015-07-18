using UnityEngine;
using System.Collections;

public class GunObject : GrabbableObject {

    public GameObject ShootPosition;
    public GameObject ShootLineRenderer;
    public GameObject[] gunPows;
    public GameObject model;
    public LayerMask ShootLayerMask;

    private Transform originalParent;
    private ConfigurableJoint confJoint;
    private Rigidbody rb;
    private Collider[] colliders;
    private bool _canShot = true;

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

    void Update() { 
        //Look at target
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, 1000, ShootLayerMask)) {
            transform.rotation = Quaternion.LookRotation(hit.point);
        }
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
        if(_canShot == true) Shoot();
        return true;
    }

    public override void ForceRelease() {
        Destroy(confJoint);
        Transform[] Ts = GetComponentsInChildren<Transform>();
        foreach (Transform t in Ts) {
            t.gameObject.layer = 9; //Physics Object
        }

        transform.parent = originalParent;
        transform.localPosition = Vector3.zero; //place it out of screen
        
    }

    void Shoot() {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, 1000, ShootLayerMask)) {
            //Debug.DrawLine(ray.origin, hit.point, Color.red, 2f);
            //check for normal grabbable object
            NormalObject normalObj = hit.transform.GetComponentInParent<NormalObject>();
            if (normalObj != null) normalObj.Push(hit.point);
            //check for boss damage
           
            Enemy_Manager enemyManager = hit.transform.GetComponentInParent<Enemy_Manager>();
            if (enemyManager != null) enemyManager.TakeDamage(1);
            StartCoroutine(SpawnShootParticles(hit));
        }
    }

    public void DeactivateGun() {
        _canShot = false;
        model.SetActive(false);
    }

    public void ActivateGun() {
        _canShot = true;
        model.SetActive(true);
    }

    IEnumerator SpawnShootParticles(RaycastHit hit) {
        //Spawn them
        LineRenderer newLineRenderer = Instantiate(ShootLineRenderer).GetComponent<LineRenderer>();
        newLineRenderer.SetPosition(0,ShootPosition.transform.position);
        newLineRenderer.SetPosition(1, hit.point);
        SpriteRenderer[] gunPowSprites = new SpriteRenderer[2];
        gunPowSprites[0] = ((GameObject)Instantiate(gunPows[Random.Range(0, gunPows.Length)], ShootPosition.transform.position, Quaternion.identity)).GetComponentInChildren<SpriteRenderer>();
        gunPowSprites[1] = ((GameObject)Instantiate(gunPows[Random.Range(0, gunPows.Length)], hit.point, Quaternion.identity)).GetComponentInChildren<SpriteRenderer>();

        //Fade them
        for (float i = 0; i < 1; i += Time.deltaTime / 1f) {
            Color targetColor = Color.Lerp(new Color(1,1,1,1), new Color(1,1,1,0), i);
            newLineRenderer.SetColors(targetColor,targetColor);
            foreach (SpriteRenderer sp in gunPowSprites) sp.color = targetColor;
            yield return null;
        }
        //Destroy them
        Destroy(newLineRenderer.gameObject);
        foreach (SpriteRenderer sp in gunPowSprites) Destroy(sp.transform.parent.gameObject);
        gunPowSprites = null;
    }

}
