using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "New Question", menuName = "Quiz Question")]
public class QuestionSO : ScriptableObject 
{
    [TextArea(2, 6)]
    [SerializeField] string question = "Enter Question Here";
    [SerializeField] string[] answers = new string[4];
    [SerializeField] int correctAnswer = -1;
    [TextArea(2, 6)]
    [SerializeField] string factoid = "Enter Factoid Here";
    
    public string GetQuestion()
    {
        return question;
    }

    public string[] GetAnswers()
    {
        return answers;
    }

    public string GetAnswer(int index)
    {
        return answers[index];
    }

    public int GetCorrectAnswer()
    {
        return correctAnswer;
    }

    public string GetFactoid()
    {
        return factoid;
    }
}
