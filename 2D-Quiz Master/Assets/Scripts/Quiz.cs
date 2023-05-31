using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Quiz : MonoBehaviour
{
    [Header("Questions")]
    [SerializeField] TextMeshProUGUI questionText;
    [SerializeField] List<QuestionSO> questions = new List<QuestionSO>();
    QuestionSO currentQuestion;

    [Header("Answers")]
    [SerializeField] GameObject[] answerButtons;
    int correctAnswer;
    bool hasAnswered = false;

    [Header("Buttons")]
    [SerializeField] Color correctColor = Color.green;
    [SerializeField] Color incorrectColor = Color.red;

    [Header("Timer")]
    [SerializeField] Image timerImage;
    Timer timer;

    [Header("Scoring")]
    [SerializeField] TextMeshProUGUI scoreText;
    ScoreKeeper scoreKeeper;
    
    void Start()
    {
        timer = FindObjectOfType<Timer>();
        scoreKeeper = FindObjectOfType<ScoreKeeper>();
        GetNextQuestion();
    }
    
    void Update()
    {
        timerImage.fillAmount = timer.fillFraction;
        if (timer.loadNextQuestion) {
            timer.loadNextQuestion = false;
            GetNextQuestion();
        }
        else if (!timer.isAnswering && !hasAnswered) {
            hasAnswered = true;
            DisplayAnswer(-1);
            SetButtonState(false);
        }
    }

    private void GetNextQuestion()
    {
        hasAnswered = false;
        SetButtonState(true);
        SetDefaultButtonSprites();
        if (questions.Count == 0)
        {
            Debug.Log("No more questions!");
            return;
        }
        GetRandomQuestion();
        DisplayQuestion();
        scoreKeeper.IncrementQuestionsSeen();
    }

    private void GetRandomQuestion()
    {
        int randomIndex = Random.Range(0, questions.Count);
        currentQuestion = questions[randomIndex];
        if (questions.Contains(currentQuestion))
        {
            questions.RemoveAt(randomIndex);
        }
    }

    private void DisplayQuestion()
    {        
        questionText.text = currentQuestion.GetQuestion();
        for (int i = 0; i < answerButtons.Length; i++)
        {
            TextMeshProUGUI buttonText = answerButtons[i].GetComponentInChildren<TextMeshProUGUI>();
            buttonText.text = currentQuestion.GetAnswer(i);
        }
    }

    private void SetButtonState(bool state)
    {
        for (int i = 0; i < answerButtons.Length; i++)
        {
            answerButtons[i].GetComponent<Button>().interactable = state;
        }
    }

    private void SetDefaultButtonSprites() {
        for (int i = 0; i < answerButtons.Length; i++)
        {
            Image buttonImage = answerButtons[i].GetComponent<Image>();
            buttonImage.color = Color.white;
        }
    }

    private void DisplayAnswer(int index) 
    {
        Image buttonImage = answerButtons[currentQuestion.GetCorrectAnswer()].GetComponent<Image>();

        if (index == currentQuestion.GetCorrectAnswer())
        {
            questionText.text = currentQuestion.GetFactoid();
            buttonImage.color = Color.green;
            scoreKeeper.IncrementCorrectAnswers();
        }
        else
        {
            questionText.text = currentQuestion.GetFactoid();
            buttonImage.color = Color.red;
        }
    }


    public void OnAnswerSelected(int index)
    {
        hasAnswered = true;
        DisplayAnswer(index);
        SetButtonState(false);
        timer.CancelTimer();
        scoreText.text = $"Score: {scoreKeeper.GetScore().ToString()}%";
    }
}
