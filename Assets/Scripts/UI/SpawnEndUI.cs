using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class SpawnEndUI : MonoBehaviour {

    public Text pedestrian;
    public Text car;
    public Text flying;
    public Text enemy;
    public Text justice;
    public float scaleTime = 0.5f;
    public Text finalScore;
    public Text finalText;

    void Start() {
        SetEnd();
    }

    void SetEnd() {
        ScoreSystem score = GameManager.instance.scoreSystem;
        pedestrian.text += score.PedestrianKill;
        car.text += score.CarKill + score.ParkedCarKill;
        flying.text += score.FlyerKill;
        StartCoroutine(ScaleEndUP());
    }

    IEnumerator ScaleEndUP() {
        float time = 0;
        yield return new WaitForSeconds(0.7f);
        while (pedestrian.transform.localScale.x < 1) {
            pedestrian.transform.localScale += new Vector3(1, 1, 1) * Time.deltaTime * scaleTime;
            yield return new WaitForEndOfFrame();
        }
        yield return new WaitForSeconds(0.7f);
        time = 0;
        while (flying.transform.localScale.x < 1) {
            flying.transform.localScale += new Vector3(1, 1, 1) * Time.deltaTime * scaleTime;
            yield return new WaitForEndOfFrame();
        }
        yield return new WaitForSeconds(0.7f);
        time = 0;
        while (car.transform.localScale.x < 1) {
            car.transform.localScale += new Vector3(1, 1, 1) * Time.deltaTime * scaleTime;
            yield return new WaitForEndOfFrame();
        }
        yield return new WaitForSeconds(1f);
        time = 0;
        while (enemy.transform.localScale.x < 1) {
            enemy.transform.localScale += new Vector3(1, 1, 1) * Time.deltaTime * scaleTime;
            yield return new WaitForEndOfFrame();
        }
        yield return new WaitForSeconds(0.5f);
        time = 0;
        while (justice.transform.localScale.x < 1) {
            justice.transform.localScale += new Vector3(1, 1, 1) * Time.deltaTime * scaleTime;
            yield return new WaitForEndOfFrame();
        }
        yield return new WaitForSeconds(1f);
        time = 0;
        while (finalScore.transform.localScale.x < 1) {
            finalScore.transform.localScale += new Vector3(1, 1, 1) * Time.deltaTime / scaleTime;
            finalText.transform.localScale += new Vector3(1, 1, 1) * Time.deltaTime / scaleTime;
            yield return new WaitForEndOfFrame();
        }
        
    }
}
