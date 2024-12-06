using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UI_Menu : MonoBehaviour
{
    public GameObject TitleScreen;
    public GameObject SplashScreen;
    public GameObject TutorialScreen;

    // Navigate to the Splash screen
    public void GoSplash()
    {
        SplashScreen.SetActive(false);
        TitleScreen.SetActive(true);
    }

    // Navigate to the Tutorial screen
    public void GoTutorial()
    {
        TitleScreen.SetActive(false);
        TutorialScreen.SetActive(true);
    }

    // Navigate back to the Title screen
    public void GoBack()
    {
        TutorialScreen.SetActive(false);
        TitleScreen.SetActive(true);
    }

    // Load a specific level by name
    public void LoadGame(string levelName)
    {
        if (!string.IsNullOrEmpty(levelName))
        {
            SceneManager.LoadScene(levelName);
        }
        else
        {
            Debug.LogWarning("Level name is empty or null. Please provide a valid level name.");
        }
    }

    // Initialize UI states at the start
    void Start()
    {
        SplashScreen.SetActive(true);
        TitleScreen.SetActive(false);
        TutorialScreen.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        // Optional logic for updates if needed
    }
}