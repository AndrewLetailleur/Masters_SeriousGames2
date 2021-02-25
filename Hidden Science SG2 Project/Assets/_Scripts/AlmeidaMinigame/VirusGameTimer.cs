// Becca's script
// Adapted from https://gamedevbeginner.com/how-to-make-countdown-timer-in-unity-minutes-seconds/#timer
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class VirusGameTimer : MonoBehaviour
{
    // Float variable to store amount of time remaining
    public float timeRemaining = 30;

    // To prevent continuous updates when timer runs out, set a bool condition
    public bool timerIsRunning = false;

    TMP_Text timeText;
    public static VirusGameTimer instance;

    // Make a public game object for the end panel to pop up when timer ends
    public GameObject endGamePanel;

    void Start()
    {
        // Starts timer automatically
        timerIsRunning = true;
        timeText = GetComponent<TMP_Text>();

        instance = this;
    }

    void Update()
    {
        // this if statement means will only do the following when the timer is running
        if (timerIsRunning)
        {
            if (timeRemaining > 0)
            {
                timeRemaining -= Time.deltaTime;
                DisplayTime(timeRemaining);
            }

            // so something happens when the time runs out
            else
            {
                Debug.Log("Time has run out!");
                timeRemaining = 0;
                // This willl stop the timer continually updating once this condition is set to false
                timerIsRunning = false;
                endGamePanel.SetActive(true);
            }
        }
    }

    void DisplayTime(float timeToDisplay)
    {
        timeToDisplay += 1;

        // To calculate the time in minutes and seconds to display
        float minutes = Mathf.FloorToInt(timeRemaining / 60);
        float seconds = Mathf.FloorToInt(timeRemaining % 60);
        float milliSeconds = (timeToDisplay % 1) * 1000;

        // Display the time in TMPro
        timeText.text = string.Format("{0:00}:{1:00}:{2:000}", minutes, seconds, milliSeconds);
    }
}
