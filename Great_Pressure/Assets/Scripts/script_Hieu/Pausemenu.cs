using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Pausemenu : MonoBehaviour
{
    public GameObject pausemenu;
    //public KeyCode pauseKey;
    public static bool isPaused;
    void Start()
    {
        pausemenu.SetActive(false);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isPaused)
            {
                ResumeGame();
            }
            else { 
            PauseGame();
            }

        }
        
    }
    public void PauseGame() {
        pausemenu.SetActive(true);
        Time.timeScale = 0f;
        isPaused = true;
    }
    public void ResumeGame()
    {
        pausemenu.SetActive(false);
        Time.timeScale = 1f;
        isPaused = false;
    }
    public void GoToMenu() {
    Time.timeScale = 1f;
        SceneManager.LoadScene("MainMenu");
    }

}
