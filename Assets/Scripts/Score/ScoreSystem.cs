using UnityEngine;
using System.Collections;

public class ScoreSystem : MonoBehaviour{

    #region KillData

    public int PedestrianKill = 0;
    public int ParkedCarKill = 0;
    public int CarKill = 0;
    public int FlyerKill = 0;

    #endregion

    private GameObject scoreText = Resources.Load("ScoreText") as GameObject;

    public void AddKill(Obstacle.Type type) {
        switch (type) {
            case Obstacle.Type.car: CarKill++; return;
            case Obstacle.Type.parkedCar: ParkedCarKill++; return;
            case Obstacle.Type.flying: FlyerKill++; return;
            case Obstacle.Type.pedestrian: PedestrianKill++; return;
        }
    }

    public void AddScore(int points, GameObject objectGivingScore, Vector3 offset, bool Combo = false) {
        AddScore(points, objectGivingScore, offset, new Vector3(0.05f, 0.05f, 0.05f), Combo);
    }

    public void AddScore(int points, GameObject objectGivingScore, Vector3 offset, Vector3 scale, bool Combo = false) {
        if (points != 0) {
            GameManager.instance.addScore(points);
            TextMesh scorePopup = (Instantiate(scoreText, objectGivingScore.transform.position + new Vector3(0, 5, 0) + offset, Quaternion.identity) as GameObject).GetComponent<TextMesh>();
            scorePopup.transform.localScale = scale;
            StartCoroutine(ScoreAnim(scorePopup, points, Combo));
        }
    }

    IEnumerator ScoreAnim(TextMesh score, int points, bool combo) {
        if (combo) {
            score.text = "Combo\n" + points.ToString();
            score.lineSpacing = 0.8f;
        }
        else score.text = points.ToString();

        float timeAnim = 1;
        float currentTime = 0;
        Color baseColor = score.GetComponent<MeshRenderer>().material.color;
        Color modifiedColor = score.GetComponent<MeshRenderer>().material.color;
        float alpha = 1;

        while (currentTime < timeAnim) {
            alpha -= timeAnim * Time.deltaTime;
            score.GetComponent<MeshRenderer>().material.color = new Color(baseColor.r, baseColor.g, baseColor.b, alpha);
            yield return new WaitForEndOfFrame();
        }

        Destroy(score.gameObject);
        
    }
}
