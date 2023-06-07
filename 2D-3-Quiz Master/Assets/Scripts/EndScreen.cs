using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class EndScreen : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI finalScoreText;
    [SerializeField] string[] ranks;
    ScoreKeeper scoreKeeper;

    void Awake()
    {
        scoreKeeper = FindObjectOfType<ScoreKeeper>();
        finalScoreText.text = scoreKeeper.GetScore().ToString();
    }

    public void ShowFinalScore() {
        if (ranks.Length > 0) {
            var score = scoreKeeper.GetScore();
            var rank = Mathf.FloorToInt(score / 100f * (ranks.Length - 1));
            finalScoreText.text = $"Congratulations!\n Your final score is {score.ToString()}%\n Rank: {ranks[rank]}";
            return;
        }
        finalScoreText.text = $"Congratulations!\n Your final score is {scoreKeeper.GetScore().ToString()}%";
    }
}
