using UnityEngine;
using System.Collections;

public class NormalObject_Carl : NormalObject {

    public override void PlaySound() {
        StartCoroutine(playCarl());
        
    }

    IEnumerator playCarl() {
        while (GameManager.instance.voices_player.PlayCarl() == false) {
            yield return null;
        }
    }

}
