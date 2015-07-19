using UnityEngine;
using System.Collections;

public class UI : MonoBehaviour {

	public void DisplayNextLevelPopUp(){
		transform.FindChild("LevelComplete").gameObject.SetActive(true);
	}

	public void NextLevel(){
		GameManager.instance.NextLevel();
	}

	public void MainMenu(){
		GameManager.instance.BackToMenu();
	}

	public void Pause(){
		GameManager.instance.Pause();
	}
}
