using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {

    [HideInInspector] public static GameManager instance{get; private set;}
    public int score = 0;
    public int level = 0;
    public int amtLevels = 4;

    void Awake() {
        if (GameManager.instance == null) {
            GameManager.instance = this;
            score = 0;
        } else {
            Destroy(gameObject);
        }
    }

    void OnDestroy() {
        if (GameManager.instance == this) GameManager.instance = null;
    }

    public void StartLevel_Phase1(){
        Application.LoadLevel("Phase1");
    }
    public  void StartLevel_Phase2(){
        Application.LoadLevel("Phase2");
    }
    public  void StartLevel_Phase3(){
        Application.LoadLevel("Phase2");
    }
    public void CompleteLevel() {
        if(level < (amtLevels-1)) Application.LoadLevel("Phase1");
        else {
            //End Game
        }
    }
}
