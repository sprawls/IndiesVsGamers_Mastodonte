using UnityEngine;
using System.Collections;

public class GunObject : GrabbableObject {

    public GameObject ShootPosition;
    public GameObject ShootLineRenderer;
    public GameObject[] gunPows;
    public GameObject hitParticles;
    public GameObject model;
    public GameObject SmashingHandGO;
    public LayerMask ShootLayerMask;

    private Transform originalParent;
    private ConfigurableJoint confJoint;
    private Rigidbody rb;
    private Collider[] colliders;
    private bool _canShot = true;

    //Cursor Related
    private Vector2 mouse;
    private int w = 32;
    private int h = 32;
    public Texture2D cursor;
    public Texture2D cursor_highlighted;
    private bool highligthed = false;

    //Gun Related
    private float cooldown = 0.25f;
    private bool onCooldown = false;
    private AudioSource audio;

    void Awake(){
        rb = GetComponent<Rigidbody>();
        colliders = GetComponentsInChildren<Collider>();
        audio = gameObject.GetComponent<AudioSource>();
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
            if (hit.collider.gameObject.layer == 9) {
                highligthed = true;
            } else highligthed = false;
            transform.rotation = Quaternion.LookRotation(hit.point);
        }
        //Get mouse position
        mouse = new Vector2(Input.mousePosition.x, Screen.height - Input.mousePosition.y);
    }

    void LateUpdate() {
        
    }

    void OnGUI(){
        if (!highligthed) GUI.DrawTexture(new Rect(mouse.x - (w / 2), mouse.y - (h / 2), w, h), cursor);
        else GUI.DrawTexture(new Rect(mouse.x - (w / 2), mouse.y - (h / 2), w, h), cursor_highlighted);
    }

    public override void Grab(Transform grabAnchor, Rigidbody rb) {
        foreach(Collider col in colliders) col.enabled = false;
        transform.parent = grabAnchor;
        transform.localPosition = Vector3.zero;
        
        /*
        confJoint = gameObject.AddComponent<ConfigurableJoint>();
        confJoint.connectedBody = rb;
        confJoint.xMotion = ConfigurableJointMotion.Locked;
        confJoint.yMotion = ConfigurableJointMotion.Locked;
        confJoint.zMotion = ConfigurableJointMotion.Locked;
        confJoint.angularXMotion = ConfigurableJointMotion.Locked;
        confJoint.angularYMotion = ConfigurableJointMotion.Locked;
        confJoint.angularZMotion = ConfigurableJointMotion.Locked;
        */
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
        if (_canShot == true) Shoot();
        else SpawnArm();
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
        if (!onCooldown) {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, 1000, ShootLayerMask)) {
                //Debug.DrawLine(ray.origin, hit.point, Color.red, 2f);
                //check for normal grabbable object
                NormalObject normalObj = hit.transform.GetComponentInParent<NormalObject>();
                if (normalObj != null) normalObj.Push(hit.point);
                //check for boss damage

                Enemy_Manager enemyManager = hit.transform.GetComponentInParent<Enemy_Manager>();
                Obstacle obstacle = hit.transform.GetComponentInParent<Obstacle>();
                if (enemyManager != null) enemyManager.TakeDamage(1);
                if (obstacle != null) obstacle.TakeDamage(1);
                audio.pitch = Random.Range(0.85f, 1.15f);
                audio.Play();
                StartCoroutine(SpawnShootParticles(hit));
            }
            StartCoroutine(CooldownTime());
        }
    }

    IEnumerator CooldownTime() {
        onCooldown = true;
        yield return new WaitForSeconds(cooldown);
        onCooldown = false;
    }

    void SpawnArm() {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, 1000, ShootLayerMask)) {
            CarButton carButton = hit.collider.gameObject.GetComponentInChildren<CarButton>();
            if (carButton != null && carButton.canBePressed()) { //Physics object
                carButton.buttonPressed();
                GameObject GO = (GameObject)Instantiate(SmashingHandGO, hit.point, Quaternion.identity);
                GO.transform.parent = originalParent.parent;
            }
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
        Instantiate(hitParticles, hit.point, Quaternion.identity);


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
