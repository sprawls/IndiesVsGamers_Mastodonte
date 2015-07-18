using UnityEngine;
using System.Collections;

public class Obstacle : MonoBehaviour {

    //VARIABLES
    public enum Type { car, parkedCar, cone, pedestrian, flying}
    public Type type;
    private bool knockInTheAir = false;

    [Header("Force")]
    public float ForceMinSpeed = 1000f;
    public float ForceMaxSpeed = 2000f;
    public float YvectorRangeMin = 1000f;
    public float YvectorRangeMax = 2000f;

    [Header("Torque")]
    public float maxTorqueSpeed = 1000f;

    [Header("Effect Ref")]
    public GameObject Explosion;

    //cooldown
    private float cooldownTimer = 0.5f;
    private bool onCooldown = false;

    #region when hit by the player

    public void OnHit(Transform player) {
        if (!onCooldown) {
            gameObject.GetComponent<Rigidbody>().mass = 1;
            AddForce(player);
            RotationRandomDerp();
            OnHitEffect();
            onCooldown = true;
            knockInTheAir = true;
            StartCoroutine(Cooldown());
        }
    }

    void OnHitEffect() {
        switch (type) {
            case Type.car: Destroy(GetComponent<Car_ForwardMove>()); return;
            case Type.pedestrian: Destroy(GetComponent<PedestrianMoveForward>()); return;
        }
    }

    IEnumerator Cooldown() {
        yield return new WaitForSeconds(cooldownTimer);
        onCooldown = false;
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

    #endregion

    void OnDeath() {
        switch (type) {
            case Type.car:
                Instantiate(Explosion, transform.position, Quaternion.identity);
                Destroy(gameObject);
                return;
            case Type.parkedCar:
                Instantiate(Explosion, transform.position, Quaternion.identity);
                Destroy(gameObject);
                return;
        }
    }

    void OnCollisionEnter(Collision other) {
        //When colliding with the player or enemy vehicle get thrown into the air, otherwise explode on impact
        if (other.gameObject.tag == "Player" || other.gameObject.tag == "Enemy") OnHit(other.transform);
        else if (knockInTheAir) OnDeath();
    }
}
