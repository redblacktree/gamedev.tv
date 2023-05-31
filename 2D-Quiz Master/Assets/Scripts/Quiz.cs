using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Quiz : MonoBehaviour
{
    [Header("Questions")]
    [SerializeField] TextMeshProUGUI questionText;
    [SerializeField] QuestionSO question;

    [Header("Answers")]
    [SerializeField] GameObject[] answerButtons;
    int correctAnswer;
    bool hasAnswered = false;

    [Header("Buttons")]
    [SerializeField] Color correctColor;
    [SerializeField] Color incorrectColor;

    [Header("Timer")]
    [SerializeField] Image timerImage;
    Timer timer;
    
    void Start()
    {
        timer = FindObjectOfType<Timer>();
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
        DisplayQuestion();
    }

    private void DisplayQuestion()
    {        
        questionText.text = question.GetQuestion();
        for (int i = 0; i < answerButtons.Length; i++)
        {
            TextMeshProUGUI buttonText = answerButtons[i].GetComponentInChildren<TextMeshProUGUI>();
            buttonText.text = question.GetAnswer(i);
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
        Image buttonImage = answerButtons[question.GetCorrectAnswer()].GetComponent<Image>();

        if (index == question.GetCorrectAnswer())
        {
            questionText.text = question.GetFactoid();
            buttonImage.color = Color.green;       
        }
        else
        {
            questionText.text = question.GetFactoid();
            buttonImage.color = Color.red;
        }
    }


    public void OnAnswerSelected(int index)
    {
        hasAnswered = true;
        DisplayAnswer(index);        
        SetButtonState(false);
        timer.CancelTimer();
    }
}
