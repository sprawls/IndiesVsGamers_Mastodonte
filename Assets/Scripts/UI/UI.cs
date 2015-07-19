using UnityEngine;
using System.Collections;

public class UI : MonoBehaviour {

	public void DisplayNextLevelPopUp(){
		transform.FindChild("LevelComplete").gameObject.SetActive(true);
	}

	public void NextLevel(){
		GameManager.instance.NextLevel();
	}

    public void NextPhase()
    {
        GameManager.instance.NextPhaseMenu();
    }

	public void MainMenu(){
		GameManager.instance.BackToMenu();
	}

	public void Pause(){
		GameManager.instance.Pause();
		transform.FindChild("PauseMenu").gameObject.SetActive(true);
		transform.FindChild("Pause").gameObject.SetActive(false);
	}

	public void UnPause(){
		GameManager.instance.UnPause();
		transform.FindChild("PauseMenu").gameObject.SetActive(false);
		transform.FindChild("Pause").gameObject.SetActive(true);
	}
}
