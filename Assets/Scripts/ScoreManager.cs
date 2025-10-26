using UnityEngine;
using TMPro; // for TextMeshProUGUI

public class ScoreManager : MonoBehaviour, IObserver
{
    [SerializeField] private TextMeshProUGUI scoreText; // assign in Inspector
    private int score = 0;

    public void OnNotify(Target target)
    {
        score += target.pointValue;
        Debug.Log("Score updated: " + score);

        UpdateUI();
    }

    private void UpdateUI()
    {
        if (scoreText != null)
        {
            scoreText.text = "Score: " + score;
        }
    }
}