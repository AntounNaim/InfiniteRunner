using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void StartGame()
    {
        Time.timeScale = 1f; // Ensure time is normal
        SceneManager.LoadScene(1); // Load game scene (index 1 in Build Settings)
    }

    public void QuitGame()
    {
        #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
        #else
            Application.Quit();
        #endif
    }
}