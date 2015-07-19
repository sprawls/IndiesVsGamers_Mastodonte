using UnityEngine;
using System.Collections;

public class GamejoltAPI_Manager {

    private bool userConnected;
    public bool IsConnected() { return userConnected; }

    public GamejoltAPI_Manager() {
        userConnected = false;
    }

    #region Login / Logout

    #if UNITY_STANDALONE
    public void Login() {
        GameJolt.UI.Manager.Instance.ShowSignIn((bool success) => {
            userConnected = success;
            if (success) {
               Debug.Log("The user signed in!");
            }
            else {
                Debug.Log("The user failed to signed in or closed the window");
            }
        });
    }
    #endif
    #if UNITY_WEBPLAYER
    public void Login(){
        userConnected = GameJolt.API.Manager.Instance.CurrentUser != null;
    }
    #endif

    public void Logout() {
        if (userConnected) {
            GameJolt.API.Manager.Instance.CurrentUser.SignOut();
        }
    }

    #endregion

    #region Score Related

    public void SendScore(int scoreValue = 0, string scoreText = "", int tableID = 83441, string extraData = "") {
        if (!userConnected) {
            Debug.LogError("Trying to use the method for sending score while connected, use SendScoreGuest() instead.");
            return;
        }

        GameJolt.API.Scores.Add(scoreValue, scoreText, tableID, extraData, (bool success) => {
            Debug.Log(string.Format("Score Add {0}.", success ? "Successful" : "Failed"));
        });
    }

    public void SendScoreGuest(int scoreValue = 0, string scoreText = "", string guestName = "Shark Cop", int tableID = 0, string extraData = "") {
        if (userConnected) {
            Debug.LogError("Trying to use the method for sending score while being a guest, use SendScore() instead.");
            return;
        }

        GameJolt.API.Scores.Add(scoreValue, scoreText, guestName, tableID, extraData, (bool success) => {
            Debug.Log(string.Format("Score Add {0}.", success ? "Successful" : "Failed"));
        });
    }

    public void ShowScore() {
        GameJolt.UI.Manager.Instance.ShowLeaderboards();
    }

    #endregion
}
