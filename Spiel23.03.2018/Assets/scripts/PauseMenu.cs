using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour {

    public static bool GameIsPaused = false;
    public GameObject pauseMenuUI;  //Pausescreen Image

    // Update is called once per frame
    void Update() {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (GameIsPaused)
            {
                Resume();
            } else
            {
                Pause();
            }
        }
    }
        void Resume() {
        pauseMenuUI.SetActive(false); 
        Time.timeScale = 1f;   // Normal Game Time 
        GameIsPaused = false; 
        }

        void Pause() {
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;  // Whole Game is paused while the screen is open
        GameIsPaused = true; 
        }
	}

