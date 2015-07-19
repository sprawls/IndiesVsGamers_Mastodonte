using UnityEngine;
using System.Collections;

public class VehiculeManager : MonoBehaviour {

    [Header("Vehicule Stats")]
    public float forwardSpeed;
    public float HorizontalSpeed;
    public float speedModifier;

    [Header("Checks")]
    public float maxDistanceX;

    [Header("Sirene")]
    public GameObject Sirene;
    public float sireneTime = 5f;
    public float sireneCooldown = 10f;
    public bool sirene_IsOn = false;
    public bool canSirene = true;

    [Header("Wipers")]
    public GameObject Window;
    public float wipersCooldown = 2f;
    public bool canWipe = true;

    private CarTilt carTilt;

    void Awake() {
        carTilt = gameObject.GetComponent<CarTilt>();
        speedModifier = 0;
        Sirene.SetActive(false);
    }

    void Update () {
        GoForward();
        CalculateSpeedModifier(Input.GetAxis("Horizontal"));
        UpdateHorizontalPos();
    }

    #region Mouvement Controller

    void CalculateSpeedModifier(float input) {
        if (input == 0) {
            if (speedModifier < 0)
                speedModifier += 0.1f;
            if (speedModifier > 0)
                speedModifier -= 0.1f;
        }
        else {
            if (speedModifier < 1 && speedModifier > -1) {
                speedModifier += (input / 2);
            }
        }
        if (speedModifier > 1) 
            speedModifier = 1;
        else if (speedModifier < -1) 
            speedModifier = -1;
        else if (speedModifier > -0.15f && speedModifier < 0.15f) 
            speedModifier = 0;
    }

    void GoForward() {
        gameObject.transform.position = new Vector3(gameObject.transform.position.x,
                                                    gameObject.transform.position.y,
                                                    gameObject.transform.position.z + forwardSpeed * Time.deltaTime);
    }

    void UpdateHorizontalPos() {
        if (speedModifier < 0) {
            gameObject.transform.position = new Vector3(Mathf.Clamp(gameObject.transform.position.x + HorizontalSpeed * speedModifier * Time.deltaTime, -maxDistanceX, maxDistanceX),
                                                        gameObject.transform.position.y,
                                                        gameObject.transform.position.z);
            carTilt.Tilt(speedModifier);
        }
        else if (speedModifier > 0) {
            gameObject.transform.position = new Vector3(Mathf.Clamp(gameObject.transform.position.x + HorizontalSpeed * speedModifier * Time.deltaTime, -maxDistanceX, maxDistanceX),
                                                        gameObject.transform.position.y,
                                                        gameObject.transform.position.z);
            carTilt.Tilt(speedModifier);
        }
    }

    #endregion

    #region Collision Test

    void OnCollisionEnter(Collision other) {
		//transform.FindChild("Main Camera").GetComponent<CameraShake>().DoShake();

    }

    #endregion

    #region Gyrophares
    public void StartCirene() {
        if (canSirene) StartCoroutine(CoroutineSirene());
    }

    IEnumerator CoroutineSirene() {
        GameManager.instance.voices_player.PlayPolice();
        canSirene = false;
        sirene_IsOn = true;
        Sirene.SetActive(true);
        yield return new WaitForSeconds(sireneTime);
        sirene_IsOn = false;
        Sirene.SetActive(false);
        yield return new WaitForSeconds(sireneCooldown);
        canSirene = true;

    }

    #endregion
    public void StartWipers() {
        if (canSirene) StartCoroutine(CoroutineWipers());
    }

    IEnumerator CoroutineWipers() {
        GameManager.instance.voices_player.PlayLikeNew();
        canWipe = false;
        Transform[] ts = Window.GetComponentsInChildren<Transform>();
        foreach (Transform t in ts) {
            if (t != Window.transform) Destroy(t.gameObject);
        }
        yield return new WaitForSeconds(wipersCooldown);
        canWipe = true;

    }

    #region Wipers

    #endregion
}
