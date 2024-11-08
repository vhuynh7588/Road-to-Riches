using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameOverScreen : MonoBehaviour
{
    // Start is called before the first frame update
    public Text pointsText;
    public Text highScoreText;

    public void Setup(int maxPlatform, int score){
        gameObject.SetActive(true);
        pointsText.text = "Coins: " + score.ToString();

        int highScore = PlayerPrefs.GetInt("HighScore", 0); // Get saved high score, default to 0
        if (score > highScore)
        {
            highScore = score;
            PlayerPrefs.SetInt("HighScore", highScore);     // Save new high score
            PlayerPrefs.Save();
        }

        highScoreText.text = "High Score: " + highScore.ToString();  // Display high score
    }
    public void RestartButton(){
        SC_2DCoin.ResetCoinCount();
        Time.timeScale = 1f; 
        SceneManager.LoadScene("GamePlay");
    }
}
