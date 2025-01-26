using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ShowScore : MonoBehaviour
{
   	public TextMeshProUGUI scoreText;          // Assign in the Inspector
    public TextMeshProUGUI highScoreText;      // Assign in the Inspector
    private int score;
    private int highScore;

    private void Start()
    {
    	score = Score.timer;
        // Load the saved high score from PlayerPrefs
        highScore = PlayerPrefs.GetInt("HighScore", 0);
        if(score > highScore){
        	highScore = score;
        	PlayerPrefs.SetInt("HighScore", highScore); 
        }
        UpdateUI();
    }

    // Update the UI Text elements with the current score and high score
    private void UpdateUI()
    {
        scoreText.text = "Score: " + score.ToString();
        highScoreText.text = "High Score: " + highScore.ToString();
    }
}

