using UnityEngine;
using TMPro;
using System.Collections;

public class UIManager : MonoBehaviour
{
    [Header("HUD")]
    public TextMeshProUGUI distanceText;
    public TextMeshProUGUI coinsText;
    public GameObject newHighScoreMessage; // UI text that shows "New High Score!" then fades

    [Header("Game Over Panel")]
    public GameObject gameOverPanel;
    public TextMeshProUGUI finalDistanceText;
    public TextMeshProUGUI finalCoinsText;
    public TextMeshProUGUI highestCoinsText; // NEW: displays highest coins ever
    public TextMeshProUGUI highScoreText;

    private int highestCoins = 0;

    private void Start()
    {
        gameOverPanel.SetActive(false);
        if (newHighScoreMessage != null)
            newHighScoreMessage.SetActive(false);
        
        // Load highest coins
        highestCoins = PlayerPrefs.GetInt("HighestCoins", 0);
    }

    private void Update()
    {
        if (GameManager.Instance != null && !GameManager.Instance.IsGameOver)
        {
            int distance = Mathf.FloorToInt(GameManager.Instance.Distance);
            distanceText.text = "Distance: " + distance;
            coinsText.text = "Coins: " + GameManager.Instance.Coins;

            // Check for new high score during play
            int currentHighScore = PlayerPrefs.GetInt("HighScore", 0);
            if (distance > currentHighScore && distance > 0)
            {
                // Show one-time message
                if (newHighScoreMessage != null && !newHighScoreMessage.activeSelf)
                {
                    StartCoroutine(ShowNewHighScoreMessage());
                }
            }
        }
    }

    private IEnumerator ShowNewHighScoreMessage()
    {
        newHighScoreMessage.SetActive(true);
        yield return new WaitForSecondsRealtime(2f);
        newHighScoreMessage.SetActive(false);
    }

    public void ShowGameOver()
    {
        int finalDistance = Mathf.FloorToInt(GameManager.Instance.Distance);
        int finalCoins = GameManager.Instance.Coins;
        int currentHighScore = PlayerPrefs.GetInt("HighScore", 0);

        // Update highest coins
        if (finalCoins > highestCoins)
        {
            highestCoins = finalCoins;
            PlayerPrefs.SetInt("HighestCoins", highestCoins);
            PlayerPrefs.Save();
        }

        gameOverPanel.SetActive(true);
        finalDistanceText.text = "Distance: " + finalDistance;
        finalCoinsText.text = "Coins: " + finalCoins;
        highestCoinsText.text = "Best Coins: " + highestCoins;

        // Distance high score
        if (finalDistance > currentHighScore)
        {
            PlayerPrefs.SetInt("HighScore", finalDistance);
            highScoreText.text = "Best: " + finalDistance;
        }
        else
        {
            highScoreText.text = "Best: " + currentHighScore;
        }
    }
}