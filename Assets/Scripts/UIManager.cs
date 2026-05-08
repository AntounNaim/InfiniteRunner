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

    private void Update()
    {
        // Update distance only if game is not over
        if (GameManager.Instance != null && !GameManager.Instance.IsGameOver)
        {
            int distance = Mathf.FloorToInt(GameManager.Instance.Distance);
            distanceText.text = "Distance: " + distance;
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