using UnityEngine;
using UnityEngine.UI;

public class GameTimer : MonoBehaviour
{
    public Text timerText;        // UI Text for the timer
    public float gameDuration = 30f; // Game duration in seconds

    private float remainingTime; // Tracks remaining time
    private bool isGameOver = false; // Tracks game over state
    public GameOverScreen GameOverScreen;
    int maxPlatform = 0;

    void Start()
    {
        remainingTime = gameDuration;
    }

    void Update()
    {
        if (!isGameOver)
        {
            // Countdown the timer
            remainingTime -= Time.deltaTime;
            
            // Update the timer display
            timerText.text = FormatTime(remainingTime);

            // Check if time has run out
            if (remainingTime <= 0)
            {
                isGameOver = true;
                EndGame();
            }
        }
    }

    // Format the time in seconds to "ss" format
    string FormatTime(float time)
    {
        int seconds = Mathf.CeilToInt(time); // Convert to seconds, round up
        return seconds.ToString("00");       // Return formatted time as "00"
    }

    // Called when the game ends
    void EndGame()
    {
        timerText.text = "00";                // Set timer to 00
        // Optional: add any additional actions here, like stopping player movement
        int totalCoins = SC_2DCoin.totalCoins;
        GameOverScreen.Setup(maxPlatform, totalCoins);
        Time.timeScale = 0f;
    }
}

