using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public GameOverScreen GameOverScreen;
    int maxPlatform = 0;
    public static GameManager instance;
    public Text scoreText;  // Reference to a UI Text component for score display
    private int score = 0;

    // Start is called before the first frame update

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);
    }

    public void GameOver(){
        GameOverScreen.Setup(maxPlatform);
    }

    public void AddScore(int points)
    {
        score += points;
        scoreText.text = "Score: " + score;
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
