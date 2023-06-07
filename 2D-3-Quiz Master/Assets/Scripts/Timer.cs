using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    [SerializeField] float timeToAnswer = 30f;
    [SerializeField] float timeToReviewAnswer = 5f;
    public bool isAnswering = true;
    public bool loadNextQuestion = false;
    public float fillFraction = 1f;
    private float timerFullValue;
    private float timeLeft;

    void Start()
    {
        timeLeft = timeToAnswer;
        timerFullValue = timeToAnswer;
    }

    void Update()
    {
        UpdateTimer();
    }

    public void CancelTimer()
    {
        timeLeft = 0;
    }

    void UpdateTimer() 
    {        
        timeLeft -= Time.deltaTime;
        fillFraction = timeLeft / timerFullValue;
        if (timeLeft <= 0) {
            isAnswering = !isAnswering;
            if (isAnswering) {
                timerFullValue = timeToAnswer;
                loadNextQuestion = true;                
            } else {
                timerFullValue = timeToReviewAnswer;
            }            
            timeLeft = timerFullValue;
        }        
    }
}
