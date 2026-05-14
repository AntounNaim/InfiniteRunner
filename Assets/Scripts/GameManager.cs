using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    [SerializeField] private GameConfig config;

    public float ScrollSpeed { get; private set; }
    public float Distance { get; private set; }
    public bool IsGameOver { get; private set; }

    public int Coins { get; private set; }

    public void AddCoin()
    {
        Coins++;
    }

    private void Awake()
    {
        if (Instance != null && Instance != this) { Destroy(gameObject); return; }
        Instance = this;
        ScrollSpeed = config.startSpeed;
        IsGameOver = false;
        Time.timeScale = 1f;   // ensure time is normal on start
    }

    private void Update()
    {
        // Restart with R key (new Input System)
        if (Keyboard.current != null && Keyboard.current.rKey.wasPressedThisFrame)
            RestartGame();

        if (IsGameOver) return;

        ScrollSpeed = Mathf.Min(ScrollSpeed + config.speedIncreaseRate * Time.deltaTime, config.maxSpeed);
        Distance += ScrollSpeed * Time.deltaTime;
    }

    public void EndGame()
    {
        if (IsGameOver) return;
        IsGameOver = true;
        Debug.Log("GAME OVER!");
        Time.timeScale = 0f;

        UIManager ui = FindFirstObjectByType<UIManager>();
        if (ui != null) ui.ShowGameOver();
    }

    public void RestartGame()
    {
        Time.timeScale = 1f;    // critical: unfreeze time
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void QuitToMenu()
    {
        Time.timeScale = 1f;
        // Option 1: load main menu scene (create one)
        // SceneManager.LoadScene("MainMenu");
        // Option 2: restart (for now)
        RestartGame();
    }

    public void ResumeGame()
    {
        // Only resume if game is not over and the game is actually paused
        if (!IsGameOver && Time.timeScale == 0f)
        {
            PauseManager pm = FindFirstObjectByType<PauseManager>();
            if (pm != null) pm.Resume();
        }
    }
}