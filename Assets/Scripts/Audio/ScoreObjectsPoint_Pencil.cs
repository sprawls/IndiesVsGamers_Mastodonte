using UnityEngine;
using System.Collections;

public class ScoreObjectsPoint_Pencil : ScoreObjectsPoint {

    public override void playSound() {
        GameManager.instance.voices_pencil.PlayScores();
    }
}
