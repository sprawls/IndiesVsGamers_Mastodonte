using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class AnimationPhase_1 : MonoBehaviour {

    private UI mainUI;
    public GameObject middleUI;
    public GameObject topBar;
    public GameObject botBar;
    public Image backImg;

    void OnEnable() {
        StartCoroutine(Animation());
    }

    public void OnClick_FadeBG() {
        mainUI = GetComponentInParent<UI>();
        StartCoroutine(FadeBackground());
    }


    IEnumerator Animation() {
        RectTransform topRect = topBar.GetComponent<RectTransform>();
        Vector3 iniTop = topBar.transform.localPosition;
        Vector3 targetTop = topBar.transform.localPosition + new Vector3(0, 5, 0);
        for (float i = 0; i < 1f; i += Time.deltaTime / 1f) {
            //topRect. = Vector3.Lerp(iniTop, targetTop, i);
            yield return null;
        }
        
        yield return null;
    }

    IEnumerator FadeBackground() {
        backImg.gameObject.SetActive(true); 
        for (float i = 0; i < 1f; i += Time.deltaTime / 2f) {
            backImg.color = Color.Lerp(new Color(0, 0, 0, 0), new Color(0, 0, 0, 1), i);
            yield return null;
        }
        mainUI.NextPhase();
    }
}
