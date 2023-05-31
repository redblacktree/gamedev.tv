using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimerController : MonoBehaviour
{
    TMPro.TextMeshProUGUI timerText;
    float startTime;
    GameObject timer;
    bool isRunning = true;
    float t = 0f;
    
    void Start()
    {
        startTime = Time.time;
        timer = GameObject.Find("Timer");
        timerText = timer.GetComponent<TMPro.TextMeshProUGUI>();
    }

    void Update()
    {
        if (isRunning) {
            t = Time.time - startTime;
            timerText.text = t.ToString("F2");
        }
    }

    public void Stop() {
        isRunning = false;
    }
}
