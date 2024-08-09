// using System.Collections;
// using UnityEngine;
// using TMPro;
// using UnityEngine.SceneManagement; // To load a new scene or restart

// public class GameManager : MonoBehaviour
// {
//     public Transform playerTransform;
//     public float fallThreshold = -9f; // Y position where the game ends
//     public float timeLimit = 5f; // Time limit in seconds (2 minutes)
//     public int requiredCubes = 6; // Number of cubes to collect

//     public TMP_Text timeText; // Reference to the TextMeshPro UI element for time
//     public TMP_Text gameOverText; // Reference to the TextMeshPro UI element for game over message

//     private float elapsedTime = 0f;
//     private ScoreManager scoreManager;
//     private bool gameOver = false;

//     void Start()
//     {
//         // Get the ScoreManager instance
//         scoreManager = FindObjectOfType<ScoreManager>();

//         // Initialize UI elements
//         UpdateTimeText();
//         gameOverText.gameObject.SetActive(false); // Hide game over text initially
//     }

//     void Update()
//     {
//         if (gameOver)
//             return;

//         // Update the elapsed time
//         elapsedTime += Time.deltaTime;
//         float remainingTime = Mathf.Max(0, timeLimit - elapsedTime);

//         // Update time display
//         UpdateTimeText();

//         // Check if player falls below the fall threshold
//         if (playerTransform.position.y < fallThreshold)
//         {
//             GameOver("Player fell below the threshold!");
//         }

//         // Check if the time limit is exceeded
//         if (remainingTime <= 0)
//         {
//             if (scoreManager.score < requiredCubes)
//             {
//                 GameOver("Failed to collect enough cubes in time!");
//             }
//         }
//     }

//     void UpdateTimeText()
//     {
//         float remainingTime = Mathf.Max(0, timeLimit - elapsedTime);
//         int minutes = Mathf.FloorToInt(remainingTime / 60);
//         int seconds = Mathf.FloorToInt(remainingTime % 60);
//         timeText.text = $"Time Remaining: {minutes:00}:{seconds:00}";
//     }

//     // Handle the game over state
//     void GameOver(string reason)
//     {
//         gameOver = true;
//         gameOverText.text = reason;
//         gameOverText.gameObject.SetActive(true); // Show game over message

//         // restart the level 
//         SceneManager.LoadScene(SceneManager.GetActiveScene().name); // Restart current scene

//     }
// }
using System.Collections;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement; // To load a new scene or restart

public class GameManager : MonoBehaviour
{
    public Transform playerTransform;
    public float fallThreshold = -9f; // Y position where the game ends
    public float timeLimit = 120f; // Time limit in seconds (2 minutes)
    public int requiredCubes = 6; // Number of cubes to collect

    public TMP_Text timeText; // Reference to the TextMeshPro UI element for time
    public TMP_Text gameOverText; // Reference to the TextMeshPro UI element for game over message

    private float elapsedTime = 0f;
    private ScoreManager scoreManager;
    private bool gameOver = false;

    void Start()
    {
        // Get the ScoreManager instance
        scoreManager = FindObjectOfType<ScoreManager>();

        // Initialize UI elements
        UpdateTimeText();
        gameOverText.gameObject.SetActive(false); // Hide game over text initially
    }

    void Update()
    {
        if (gameOver)
            return;

        // Update the elapsed time
        elapsedTime += Time.deltaTime;
        float remainingTime = Mathf.Max(0, timeLimit - elapsedTime);

        // Update time display
        UpdateTimeText();

        // Check if player falls below the fall threshold
        if (playerTransform.position.y < fallThreshold)
        {
            GameOver("You fell to your death");
            Debug.Log("Started Coroutine");
            StartCoroutine(WaitAndRestart());
        }

        // Check if the time limit is exceeded
        if (remainingTime <= 0)
        {
            if (scoreManager.score < requiredCubes)
            {
                GameOver("Game Over!");
            }
        }
    }

    void UpdateTimeText()
    {
        float remainingTime = Mathf.Max(0, timeLimit - elapsedTime);
        int minutes = Mathf.FloorToInt(remainingTime / 60);
        int seconds = Mathf.FloorToInt(remainingTime % 60);
        timeText.text = $"Time Remaining: {minutes:00}:{seconds:00}";
    }

    // Handle the game over state
    void GameOver(string reason)
    {
        gameOver = true;
        gameOverText.text = reason;
        gameOverText.gameObject.SetActive(true); // Show game over message

        // Start coroutine to wait and then restart the game
        StartCoroutine(WaitAndRestart());
    }

    // Coroutine to wait and then restart the game
    IEnumerator WaitAndRestart()
    {
        yield return new WaitForSeconds(10); // Wait for 10 seconds

        // Restart the level or load a different scene
        SceneManager.LoadScene(SceneManager.GetActiveScene().name); // Restart current scene
    }
}
