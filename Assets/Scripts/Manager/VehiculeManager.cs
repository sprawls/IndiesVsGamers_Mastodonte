﻿using UnityEngine;
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

    [Header("Drivin Wheel")]
    public Transform drivinWheel;

    private CarTilt carTilt;

    void Awake() {
        carTilt = gameObject.GetComponent<CarTilt>();
        speedModifier = 0;
        Sirene.SetActive(false);
    }

    void Start() {
        StartCoroutine(PlayIntroVoices());
    }

    void Update () {
        GoForward();
        CalculateSpeedModifier(Input.GetAxis("Horizontal"));
        UpdateHorizontalPos();
        UpdateWheelPos(Input.GetAxis("Horizontal"));
    }

    #region Mouvement Controller
    void UpdateWheelPos(float input) {
        Vector3 targetRot = Vector3.zero;
        if (input == 0) targetRot = new Vector3(0, 0, 0);
        if (input > 0) targetRot = new Vector3(0, 0, -30);
        if (input < 0) targetRot = new Vector3(0, 0, 30);

       
        Quaternion TargetQ = Quaternion.Euler(targetRot);

        drivinWheel.transform.localRotation = Quaternion.Lerp(drivinWheel.transform.localRotation, TargetQ, 0.075f);
    }


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

    #region Wipers
    public void StartWipers() {
        if (canWipe) StartCoroutine(CoroutineWipers());
    }

    IEnumerator CoroutineWipers() {
        GameManager.instance.voices_player.PlayLikeNew();
        canWipe = false;
        Transform[] ts = Window.GetComponentsInChildren<Transform>();
        foreach (Transform t in ts) {
            if (t != Window.transform) Destroy(t.gameObject);
        }
        wipe[] wipers = GetComponentsInChildren<wipe>();
        foreach (wipe w in wipers) {
            w.Wipe();
        }
        yield return new WaitForSeconds(wipersCooldown);
        canWipe = true;

    }
    #endregion

    IEnumerator PlayIntroVoices() {
        GameManager.instance.voices_stalin.PlayIntro();
        yield return new WaitForSeconds(2.5f);
        GameManager.instance.voices_player.PlayBehindBars();
        yield return new WaitForSeconds(2f);
        GameManager.instance.voices_pencil.PlayIntro();

       
    }
}
