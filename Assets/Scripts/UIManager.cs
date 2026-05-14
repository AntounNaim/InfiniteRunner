using UnityEngine;
using TMPro;
using UnityEngine.UI;   // if using legacy Text, change accordingly

public class UIManager : MonoBehaviour
{
    public TextMeshProUGUI distanceText;        // HUD distance
    public GameObject gameOverPanel;            // reference to the panel
    public TextMeshProUGUI finalDistanceText;   // inside panel, for final score

    private void Start()
    {
        gameOverPanel.SetActive(false);
    }

    public TextMeshProUGUI coinsText;

    private void Update()
    {
        if (GameManager.Instance != null && !GameManager.Instance.IsGameOver)
        {
            distanceText.text = "Distance: " + Mathf.FloorToInt(GameManager.Instance.Distance);
            coinsText.text = "Coins: " + GameManager.Instance.Coins;
        }
    }

    // Called by GameManager when game ends
    public void ShowGameOver()
    {
        gameOverPanel.SetActive(true);
        int final = Mathf.FloorToInt(GameManager.Instance.Distance);
        finalDistanceText.text = "Final Score: " + final;
    }
}