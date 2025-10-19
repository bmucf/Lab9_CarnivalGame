using UnityEngine;

public class ScoreManager : MonoBehaviour, IObserver
{
    private int score = 0;

    public void OnNotify(Target target)
    {
        score += target.pointValue;
        Debug.Log("Score updated: " + score);

        // TODO: Update UI or trigger feedback
    }
}