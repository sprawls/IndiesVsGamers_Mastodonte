using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {

    [HideInInspector] public static GameManager instance{get; private set;}
    public int score = 0;
    public int amtLevels = 4;
	internal int currentLevel = 0;
	internal int currentPhase = 1;

	//Round timer
	public float roundTime = 30.0f;
	private bool phaseOngoing = true;

    void Awake() {
        if (GameManager.instance == null) {
            GameManager.instance = this;
            score = 0;
        } else {
            Destroy(gameObject);
        }
    }

	void Start(){
		Debug.Log ("Loaded " + Application.loadedLevelName);

		currentPhase = int.Parse(Application.loadedLevelName.Substring(5));
	}

	void Update(){
		if(phaseOngoing){
			//Timer
			roundTime -= Time.deltaTime;
			GameObject.Find("UI").transform.FindChild("Timer").GetComponent<Text>().text = roundTime.ToString("F2");
			if(roundTime <= 0f){
				phaseOngoing = false;
				NextPhase();
			}
		}
	}


    void OnDestroy() {
        if (GameManager.instance == this) GameManager.instance = null;
    }

	//Game loop
	private void NextPhase(){
		switch(currentPhase){
			case 1:
				StartLevel_Phase2();
				break;
			case 2:
				StartLevel_Phase3();
				break;
			case 3:
				CompleteLevel();
				break;
			default:
				break;
		}
	}

    public void StartLevel_Phase1(){
        Application.LoadLevel("Phase1");
    }
    public  void StartLevel_Phase2(){
        Application.LoadLevel("Phase2");
    }
    public  void StartLevel_Phase3(){
        Application.LoadLevel("Phase3");
    }
    public void CompleteLevel() {
		currentLevel++;

		if(currentLevel < (amtLevels-1))
			StartLevel_Phase1();
        else {
            //End Game
			Debug.Log ("Leaderboard time!");
        }
    }
}
