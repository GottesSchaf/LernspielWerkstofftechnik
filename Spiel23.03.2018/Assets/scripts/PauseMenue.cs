﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenue : MonoBehaviour {

    public static bool GameIsPaused = false;
    public GameObject pauseMenuUI; 

    // Update is called once per frame
    void Update() {
        if (Input.GetKeyDown(KeyCode.P))
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
        Time.timeScale = 1f;
        GameIsPaused = false; 
        }

        void Pause() {
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        GameIsPaused = true; 
        }
	}

