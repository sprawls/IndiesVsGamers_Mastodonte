using UnityEngine;
using System.Collections;

// Camera shake that decreases linearly over its lifespan.

public class CameraShake : MonoBehaviour
{
	// How long the object should shake for.
	private float shake = 0.0f; //How much longer we have to shake
	public float shakeParam = 1.05f;
	// Amplitude of the shake. A larger value shakes the camera harder.
	public float shakeAmount = 0.05f;
	private float decreaseFactor = 1.0f; //Divide the lenght of the shake by this

    // Transform of the camera to shake. Grabs the gameObject's transform
    // if null.
    private Transform camTransform = null;

	private Vector3 originalPos;
	
	void Awake()
	{
		if (camTransform == null)
		{
			camTransform = Camera.main.transform;
		}
		originalPos = camTransform.localPosition;
	}
	
	public void DoShake(float left=0.4f, float right=0.6f, float duration=0.3f)
	{
		shake = shakeParam;
	}
	
	void Update()
	{
        if (shake > 0f)
		{
			camTransform.localPosition = originalPos + Random.insideUnitSphere * (shakeAmount/(shakeParam/shake) * Time.timeScale);
			
			shake -= Time.deltaTime * decreaseFactor;
		}
		else
		{
			shake = 0f;
			camTransform.localPosition = originalPos;
		}
	}
}