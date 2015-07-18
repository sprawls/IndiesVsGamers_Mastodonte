using UnityEngine;
using System.Collections;

public class UI : MonoBehaviour {

	public void DisplayNextLevelPopUp(){
		transform.FindChild("LevelComplete").gameObject.SetActive(true);
	}
}
