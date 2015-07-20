using UnityEngine;
using System.Collections;

public class Obstacle : MonoBehaviour {

    //VARIABLES
    public enum Type { car, parkedCar, cone, pedestrian, flying}
    public Type type;
    public bool knockInTheAir = false;
    [Header("Stats")]
    public int maxHealth;
    private int health;

    [Header("Force")]
    public float ForceMinSpeed = 1000f;
    public float ForceMaxSpeed = 2000f;
    public float YvectorRangeMin = 1000f;
    public float YvectorRangeMax = 2000f;

    [Header("Torque")]
    public float maxTorqueSpeed = 1000f;

    [Header("Explosion")]
    public GameObject Explosion;

    [Header("Points")]
    public int scoreGivenOnDeath;
    public int scoreGivenOnBump;
    private bool hitByPlayer;

    //cooldown
    private float cooldownTimer = 0.5f;
    private bool onCooldown = false;

    //Audio Chances
    private float sharkAudioChance = 0.2f;
    private float stalinAudioChance = 0.1f;
    private float pencilAudioChance = 0.025f;

    public AudioClip explosionSound = GameManager.instance.explosionSound;

    void Awake() {
        health = maxHealth;
        hitByPlayer = false;
    }

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
            case Type.pedestrian:
                GameManager.instance.voices_pedestrian.PlayScream(gameObject.GetComponent<AudioSource>());
                Destroy(GetComponent<PedestrianMoveForward>()); 
                return;
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
        //Explosion
        if (type != Type.pedestrian)
            AudioSource.PlayClipAtPoint(explosionSound, transform.position);

        if (hitByPlayer) {
            GameManager.instance.scoreSystem.AddScore(scoreGivenOnDeath, gameObject, new Vector3(0,0,0));
            GameManager.instance.scoreSystem.AddKill(type);
        }
        switch (type) {
            case Type.car:
                Instantiate(Explosion, transform.position, Quaternion.identity);
                Destroy(gameObject);
                return;
            case Type.parkedCar:
                Instantiate(Explosion, transform.position, Quaternion.identity);
                Destroy(gameObject);
                return;
            case Type.flying:
                Instantiate(Explosion, transform.position, Quaternion.identity);
                ExplodingFlyer();
                Destroy(gameObject, 0.1f);
                return;
        }
    }

    void ExplodingFlyer() {
        gameObject.transform.FindChild("Explosion").GetComponent<Explosion>().BOOM();
    }

    public void TakeDamage(int damage) {
        if (health > 0) {
            health -= damage;
            if (type == Type.flying) GameManager.instance.scoreSystem.AddScore(100, gameObject, new Vector3(0, -20, 0));
            if (health <= 0) {
                if (type != Type.flying) {
                    GameManager.instance.scoreSystem.AddScore(scoreGivenOnBump, gameObject, new Vector3(0, 0, 0));
                    OnDeath();
                }
                else {
                    GameManager.instance.voices_player.PlayAirDown();
                    gameObject.GetComponent<Rigidbody>().useGravity = true;
                    RotationRandomDerp();
                    gameObject.GetComponent<Rigidbody>().AddForce(Random.Range(ForceMinSpeed, ForceMaxSpeed) * gameObject.transform.forward);
                    knockInTheAir = true;
                    hitByPlayer = true;
                }
            }
        }
    }

    void OnCollisionEnter(Collision other) {
        if (knockInTheAir && hitByPlayer && other.gameObject.GetComponentInParent<Obstacle>()) { //Combo points
            GameManager.instance.scoreSystem.AddScore(scoreGivenOnBump * 2, gameObject, new Vector3(0,10,0), true);
        }

        //When colliding with the player or enemy vehicle get thrown into the air, otherwise explode on impact
        if (type == Type.flying && other.gameObject.GetComponentInParent<Enemy_Manager>() != null && knockInTheAir) OnDeath();
        else if (other.gameObject.tag == "Player" || other.gameObject.tag == "Enemy") {
            if (other.gameObject.tag == "Player") {
                GameManager.instance.scoreSystem.AddScore(scoreGivenOnBump, gameObject, new Vector3(0, 0, 30));
                hitByPlayer = true;
            }
            if (other.gameObject.tag == "Enemy" && hitByPlayer && type != Type.flying) {
                GameManager.instance.scoreSystem.AddScore(5000, gameObject, new Vector3(0, 10, 0), true);
                other.gameObject.GetComponent<Enemy_Manager>().TakeDamage(10);
            }
            OnHit(other.transform);
            float randomChange = Random.Range(0f,1f);

            if (other.gameObject.tag == "Player" && randomChange < sharkAudioChance) {
                switch (type) {
                    case Type.parkedCar:
                    case Type.car:
                        GameManager.instance.voices_player.PlayCarHit();
                        break;
                    case Type.pedestrian :
                        GameManager.instance.voices_player.PlayPedHit();
                        break;
                }
            } else if (other.gameObject.tag == "Player" && randomChange < pencilAudioChance) {
                switch (type) {
                    case Type.parkedCar:
                    case Type.car:
                        GameManager.instance.voices_pencil.PlayPedHit();
                        break;
                    case Type.pedestrian:
                        GameManager.instance.voices_pencil.PlayPedHit();
                        break;
                }
            } else if (other.gameObject.tag == "Enemy" && randomChange < stalinAudioChance) {
                switch (type) {
                    case Type.parkedCar:
                    case Type.car:
                        GameManager.instance.voices_stalin.PlayPedHit();
                        break;
                    case Type.pedestrian:
                        GameManager.instance.voices_stalin.PlayPedHit();
                        break;
                }
            }
        } else if (knockInTheAir) OnDeath();
        
    }
}
