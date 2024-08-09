using UnityEngine;
using TMPro;

public class ScoreManager : MonoBehaviour
{
    public int score = 0; // The player's score
    public TMP_Text scoreText; // Reference to the TextMeshPro UI Text component that displays the score

    void Start()
    {
        UpdateScoreText(); // Initialize the score display
    }

    // Method to increase the score
    public void IncreaseScore()
    {
        score ++; // Increase the score by the specified amount
        UpdateScoreText(); // Update the score display
    }

    // Method to update the score text UI
    void UpdateScoreText()
    {
        scoreText.text = "Score: " + score.ToString(); // Update the UI text to display the current score
    }
}

