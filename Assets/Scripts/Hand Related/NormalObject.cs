using UnityEngine;
using System.Collections;

public class NormalObject : GrabbableObject {

    public int amt_points = 10;
    public GameObject ScoreParticles;

    private bool _canScore = true;
    private Transform originalParent;
    private ConfigurableJoint confJoint;
    private Rigidbody rb;

    void Awake() {
        rb = GetComponent<Rigidbody>();
    }

    void Start() {
        originalParent = transform.parent;
        Transform[] Ts = GetComponentsInChildren<Transform>();
        foreach (Transform t in Ts) {
            t.gameObject.layer = 9; //Physics Object
        }
        canBeUsed = false;
       
    }

    public override void Grab(Transform grabAnchor, Rigidbody rb) {
        transform.parent = grabAnchor;
        //transform.localPosition = Vector3.zero;

        confJoint = gameObject.AddComponent<ConfigurableJoint>();
        confJoint.connectedBody = rb;
        confJoint.xMotion = ConfigurableJointMotion.Locked;
        confJoint.yMotion = ConfigurableJointMotion.Locked;
        confJoint.zMotion = ConfigurableJointMotion.Locked;

        /*
        JointDrive xDrive = new JointDrive();
        xDrive.mode = JointDriveMode.Position;
        confJoint.angularXDrive = xDrive;
        JointDrive YZDrive = new JointDrive();
        YZDrive.mode = JointDriveMode.Position;
        confJoint.angularYZDrive = YZDrive;
        */

        /*
        hingeJoint = gameObject.AddComponent<HingeJoint>();
        hingeJoint.connectedBody = rb;
        hingeJoint.connectedAnchor = Vector3.zero;
        //hingeJoint.anchor = grabAnchor.position;
        JointSpring tempSpring = new JointSpring();
        tempSpring.spring = 10;
        tempSpring.damper = 1;
        hingeJoint.spring = tempSpring;
        */
        /*
        springJoint = gameObject.AddComponent<SpringJoint>();
        springJoint.connectedBody = rb;
        springJoint.connectedAnchor = Vector3.zero;
        springJoint.anchor = grabAnchor.position;
        springJoint.minDistance = 0.5f;
        springJoint.maxDistance = 0.5f;
        springJoint.spring = 10;
        springJoint.damper = 1;
        */
    }

    public override void Release() {
        if (canBeDropped == false) return;
        transform.parent = originalParent;
        Destroy(confJoint);
    }

    public override bool Use() {
        return false;
    }

    public override void ForceRelease() {

    }

    public void ScorePoints(float multiplier) {
        if (_canScore) {
            _canScore = false;
            GameManager.instance.addScore( (int)(amt_points * multiplier) );
            if (ScoreParticles != null) Instantiate(ScoreParticles,transform.position, Quaternion.identity);
        }
    }
    public bool CanBeScored() {
        return _canScore;
    }

    public void Push(Vector3 point) {
        Debug.Log("Push not implemented yet!");
    }

}
