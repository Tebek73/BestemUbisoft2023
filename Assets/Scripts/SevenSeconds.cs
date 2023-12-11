using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SevenSeconds : MonoBehaviour
{
    public TMP_Text text;
     public float elapsedTime = 0f;
    private bool isTimerRunning = false;
    public bool isStopped;
    public string timerText;
    void Update()
    {
        if(elapsedTime == 0) timerText = "Wait for the timer to start!";
        else timerText = (int)(8 - elapsedTime) + " seconds to kill...";
        text.text = timerText;
        if (isTimerRunning)
        {
            elapsedTime += Time.deltaTime;
            
        }
    }

    // Start the timer
    public void StartTimer()
    {
        isTimerRunning = true;
    }

    // Stop the timer
    public void StopTimer()
    {
        isTimerRunning = false;
    }

    // Reset the timer
    public void ResetTimer()
    {
        elapsedTime = 0f;

    }
}
