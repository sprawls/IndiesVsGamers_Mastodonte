using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class AnimationPhase_1 : MonoBehaviour {

    private UI mainUI;
    public GameObject middleUI;
    public GameObject topBar;
    public GameObject botBar;
    public Image backImg;
    public GameObject CrazyCar;

    private GameObject playerhand;

    void OnEnable() {
        playerhand = GameObject.FindGameObjectWithTag("PlayerHand");
        middleUI.SetActive(false);
        Destroy(playerhand);
        StartCoroutine(Animation());
    }

    public void OnClick_FadeBG() {
        mainUI = GetComponentInParent<UI>();
        StartCoroutine(FadeBackground());
    }

    void OnDisable() {
        StopAllCoroutines();
    }


    IEnumerator Animation() {

        RectTransform topRect = topBar.GetComponent<RectTransform>();
        Vector3 iniTop = topRect.anchoredPosition;
        Vector3 targetTop = iniTop + new Vector3(0, 50, 0);
        RectTransform botRect = botBar.GetComponent<RectTransform>();
        Vector3 iniBot = topRect.anchoredPosition;
        Vector3 targetBot = iniBot + new Vector3(0, -50, 0);
        
        topRect.anchoredPosition = targetTop;
        botRect.anchoredPosition = targetBot;
        for (float i = 0; i < 1f; i += Time.deltaTime / 1f) {
            Time.timeScale = 1;
            topRect.anchoredPosition = Vector3.Lerp(targetTop, iniTop, Mathf.SmoothStep(0,1,i));
            botRect.anchoredPosition = Vector3.Lerp(targetBot, iniBot, Mathf.SmoothStep(0, 1, i));
            yield return null;
        }


        yield return new WaitForSeconds(1.5f);
        Instantiate(CrazyCar, Vector3.zero, Quaternion.identity);
        Camera.main.GetComponent<Animation>().Play("CameraPhaseOneEnd");
        yield return new WaitForSeconds(3f);

        middleUI.SetActive(true);
    }

    IEnumerator FadeBackground() {
        backImg.gameObject.SetActive(true); 
        for (float i = 0; i < 1f; i += Time.deltaTime / 2f) {
            backImg.color = Color.Lerp(new Color(0, 0, 0, 0), new Color(0, 0, 0, 1), Mathf.SmoothStep(0, 1, i));
            yield return null;
        }
        mainUI.NextPhase();
    }
}
