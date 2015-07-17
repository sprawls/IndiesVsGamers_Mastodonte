using UnityEngine;
using System.Collections;

public class ScoreObjectsPoint : MonoBehaviour {

    public float multiplier;

    private float MultiplierPercentageLost = 0.15f;

    void OnTriggerEnter(Collider col) {
        NormalObject NOScript = col.GetComponentInParent<NormalObject>();
        if (NOScript != null && NOScript.CanBeScored()) {
            NOScript.ScorePoints(multiplier);
            multiplier *= (1f - MultiplierPercentageLost); //Reduce gain points for succesive items
        }
    }
}
