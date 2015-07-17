using UnityEngine;
using System.Collections;

public class Obstacle : MonoBehaviour {

    //VARIABLES
    public enum Type { car, cone, pedestrian}
    public Type type;
    [Header("Force")]
    public float ForceMinSpeed = 5f;
    public float ForceMaxSpeed = 15f;
    public float YvectorRangeMin = 5f;
    public float YvectorRangeMax = 30f;

    [Header("Torque")]
    public float maxTorqueSpeed = 1000f;

    //cooldown
    private float cooldownTimer = 0.5f;
    private bool onCooldown = false;

    public void OnHit(Transform player) {
        if (!onCooldown) {
            AddForce(player);
            RotationRandomDerp();
            onCooldown = true;
            StartCoroutine(Cooldown());
        }
    }

    private void RotationRandomDerp() {
        gameObject.GetComponent<Rigidbody>().AddTorque(new Vector3(Random.Range(0, maxTorqueSpeed), 
                                                                   Random.Range(0, maxTorqueSpeed),
                                                                   Random.Range(0, maxTorqueSpeed)));
    }

    private void AddForce(Transform player) {
        Vector3 force = (gameObject.transform.position - player.position) * Random.Range(ForceMinSpeed, ForceMaxSpeed) + new Vector3(0, Random.Range(YvectorRangeMin, YvectorRangeMax));
        gameObject.GetComponent<Rigidbody>().AddForce(force);
    }

    IEnumerator Cooldown() {
        yield return new WaitForSeconds(cooldownTimer);
        onCooldown = false;
    }
}
