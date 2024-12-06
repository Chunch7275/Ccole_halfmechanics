using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOvertoMenu : MonoBehaviour
{
    [SerializeField] private string levelName; // Name of the level to load
    [SerializeField] private float delayTime = 10f; // Delay time in seconds

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(LoadSceneAfterDelay());
    }

    private IEnumerator LoadSceneAfterDelay()
    {
        yield return new WaitForSeconds(delayTime);
        LoadGame(levelName);
    }

    public void LoadGame(string levelName)
    {
        if (!string.IsNullOrEmpty(levelName))
        {
            SceneManager.sceneLoaded += OnSceneLoaded; // Subscribe to the sceneLoaded event
            SceneManager.LoadScene(levelName);
        }
        else
        {
            Debug.LogWarning("Level name is empty or null. Please provide a valid level name.");
        }
    }
    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // Unlock and show the cursor
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        // Unsubscribe from the event to avoid multiple calls
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

}