using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class EndScreen : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI finalScoreText;
    ScoreKeeper scoreKeeper;

    // Start is called before the first frame update
    void Awake()
    {
        scoreKeeper = FindObjectOfType<ScoreKeeper>();
        finalScoreText.text = scoreKeeper.GetScore().ToString();
        
    }

    public void ShowFinalScore() {
        finalScoreText.text = $"Congratulations!\n Your final score is {scoreKeeper.GetScore().ToString()}%";
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
