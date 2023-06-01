using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class EndScreen : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI finalScoreText;
    [SerializeField] string[] ranks;
    ScoreKeeper scoreKeeper;

    // Start is called before the first frame update
    void Awake()
    {
        scoreKeeper = FindObjectOfType<ScoreKeeper>();
        finalScoreText.text = scoreKeeper.GetScore().ToString();
        
    }

    public void ShowFinalScore() {
        if (ranks.Length > 0) {
            var score = scoreKeeper.GetScore();
            var rank = Mathf.FloorToInt(score / 100f * ranks.Length);
            finalScoreText.text = $"Congratulations!\n Your final score is {score.ToString()}%\n Rank: {ranks[rank]}";
            return;
        }
        finalScoreText.text = $"Congratulations!\n Your final score is {scoreKeeper.GetScore().ToString()}%";
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
