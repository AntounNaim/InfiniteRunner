using UnityEngine;
using UnityEngine.InputSystem;

public class PauseManager : MonoBehaviour
{
    [SerializeField] private GameObject pausePanel;
    private bool isPaused = false;
    private PlayerController player;

    private void Start()
    {
        pausePanel.SetActive(false);
        player = FindFirstObjectByType<PlayerController>();
    }

    private void Update()
    {
        if (Keyboard.current != null && Keyboard.current.escapeKey.wasPressedThisFrame)
        {
            if (GameManager.Instance.IsGameOver) return;
            TogglePause();
        }
    }

    private void TogglePause()
    {
        if (isPaused)
            Resume();
        else
            Pause();
    }

    private void Pause()
    {
        isPaused = true;
        Time.timeScale = 0f;
        pausePanel.SetActive(true);
        if (player != null) player.enabled = false;
    }

    public void Resume()   // called by GameManager.ResumeGame()
    {
        if (!isPaused) return;
        isPaused = false;
        Time.timeScale = 1f;
        pausePanel.SetActive(false);
        if (player != null) player.enabled = true;
    }
}