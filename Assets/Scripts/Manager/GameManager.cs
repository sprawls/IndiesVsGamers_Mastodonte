﻿using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {

	//Singleton
	private static GameManager _instance;
	public static GameManager instance{
		get{
			if(_instance == null){
				_instance = GameObject.FindObjectOfType<GameManager>();
				DontDestroyOnLoad(_instance.gameObject);
			}
			
			return _instance;
		}
	}

    //Collectable
    public Inventory inventory;

	//Game loop
	public int numberOfLevels = 4;
	internal int currentLevel = 0;
	internal int currentPhase = 1;

	//Score
	private int score;
	GamejoltAPI_Manager api = new GamejoltAPI_Manager();

	//Round timer
	public float phase1Time = 15.0f;
	public float phase2Time = 45.0f;
	public float phase3Time = 15.0f;
	private float currentPhaseTime = 0f;
	private bool phaseOngoing = true;

	
	void Awake() {
		if(_instance == null){
            if (inventory == null) {
                inventory = new Inventory();
            }
			_instance = this;
			DontDestroyOnLoad(this);
			score = 0;
		}
		else{
			if(this != _instance)
				Destroy(this.gameObject);
		}
	}

	void Start(){
		currentPhase = int.Parse(Application.loadedLevelName.Substring(5));

		switch(currentPhase){
		case 0:
			phaseOngoing = false;
			break;
		case 1:
			currentPhaseTime = phase1Time;
			break;
		case 2:
			currentPhaseTime = phase2Time;
			break;
		case 3:
			currentPhaseTime = phase3Time;
			break;
		case 4:
			phaseOngoing = false;
			currentPhaseTime = 0;
			api.ShowScore();
			break;
		default:
			break;
		}


	}
	
	private void Update(){
		if(phaseOngoing){
			//Timer
			currentPhaseTime -= Time.deltaTime;
			GameObject.Find("Main_UI").transform.FindChild("Timer").GetComponent<Text>().text = currentPhaseTime.ToString("F2");
			if(currentPhaseTime <= 0f){
				phaseOngoing = false;
				NextPhase();
			}

			//Scoring
			GameObject.Find("Main_UI").transform.FindChild("Score").GetComponent<Text>().text = score.ToString("000000000");
		}
		else{

		}
	}
	
    private void OnDestroy() {
        if (GameManager._instance == this){
			GameManager._instance = null;
		}
    }


	#region game loop
	private void NextPhase(){
		currentPhase = int.Parse(Application.loadedLevelName.Substring(5));

		switch(currentPhase){
			case 1:
				currentPhaseTime = phase2Time;
				StartLevel_Phase2();
				phaseOngoing = true;
				break;
			case 2:
				currentPhaseTime = phase3Time;
				StartLevel_Phase3();
				phaseOngoing = true;
			break;
			case 3:
				currentPhaseTime = phase3Time;
				CompleteLevel();
				break;
			default:
				StartLevel_Phase1();
				phaseOngoing = true;
				break;
		}
	}

	private void StartLevel_MainM0(){
		Application.LoadLevel("MainM0");
	}
	private void StartLevel_Phase1(){
        Application.LoadLevel("Phase1");
    }
    private void StartLevel_Phase2(){
        Application.LoadLevel("Phase2");
    }
    private  void StartLevel_Phase3(){
        Application.LoadLevel("Phase3");
    }
	private  void StartLevel_EndGa4(){
		Application.LoadLevel("EndGa4");

	}


	void OnLevelWasLoaded(int level){
		if(level == 0){

		}
		else if(level == 1){
			currentPhaseTime = phase1Time;
			phaseOngoing = true;
		}
		else if(level == 2){
			currentPhaseTime = phase2Time;
			phaseOngoing = true;
		}
		if(level == 3){
			currentPhaseTime = phase3Time;
			phaseOngoing = true;
		}
		if(level == 4){
			//api.SendScore();//TODO
			api.ShowScore();
		}
	}
	
	private void CompleteLevel() {
		currentLevel++;
		//End Game
		if(currentLevel < (numberOfLevels)){
			GameObject.Find("Main_UI").GetComponent<UI>().DisplayNextLevelPopUp();
		}
		else
			StartLevel_EndGa4();
			
	}

	#endregion

	#region UI calls

	public void login(){
		api.Login();
	}

	public void NextLevel(){
		StartLevel_Phase1();
		currentPhaseTime = phase1Time;
		phaseOngoing = true;
	}

	public void BackToMenu(){
		StartLevel_MainM0();
	}

	public void Pause(){
		Time.timeScale = 0.0f;
	}

	public void UnPause(){
		Time.timeScale = 1.0f;
	}

	public void Quit(){
		Application.Quit();
	}
	
	#endregion


	#region Score Managing

	public void addScore(int scoreInc){
		score += scoreInc;
	}

	#endregion
}
