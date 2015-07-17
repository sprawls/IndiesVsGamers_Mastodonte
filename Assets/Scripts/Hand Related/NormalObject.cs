using UnityEngine;
using System.Collections;

public class NormalObject : GrabbableObject {

    public Rigidbody rbToDeactivate;

    private Transform originalParent;
    private HingeJoint hingeJoint;
    private SpringJoint springJoint;
    private ConfigurableJoint confJoint;

    void Start() {
        originalParent = transform.parent;
        Transform[] Ts = GetComponentsInChildren<Transform>();
        foreach (Transform t in Ts) {
            t.gameObject.layer = 9; //Physics Object
        }
       
    }

    public override void Grab(Transform grabAnchor, Rigidbody rb) {
        transform.parent = grabAnchor;

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

    public override void Use() {

    }

    public override void ForceRelease() {

    }

}
