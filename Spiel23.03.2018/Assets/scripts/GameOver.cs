using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour {

	public void SpielBeenden()
    {
        Application.Quit();
    }

    public void ErneutVersuchen()
    {
        SceneManager.LoadScene("level1", LoadSceneMode.Single);
    }
}
