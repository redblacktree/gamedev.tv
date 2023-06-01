using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreKeeper : MonoBehaviour
{
    int correctAnswers = 0;
    int questionsSeen = 0;

    public int GetCorrectAnswers()
    {
        return correctAnswers;
    }

    public int GetQuestionsSeen()
    {
        return questionsSeen;
    }

    public void IncrementCorrectAnswers()
    {
        correctAnswers++;
    }

    public void IncrementQuestionsSeen()
    {
        questionsSeen++;
    }

    public void ResetScore()
    {
        correctAnswers = 0;
        questionsSeen = 0;
    }

    public float GetScore()
    {
        var score = (float)correctAnswers / (float)questionsSeen;
        return Mathf.Round(score * 100f);
    }
}
