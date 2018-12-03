using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PauseMenu : MonoBehaviour {

    public static bool GameIsPaused = false;
    public GameObject pauseMenuUI;
    PlayerControls player;
    
	// Update is called once per frame
	void Update () {

        player = gameObject.GetComponent<PlayerControls>();

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (GameIsPaused)
            {

                Resume();
                player.onResume();
            }
            else
            {
                Pause();
                defaulButton();
                player.onPause();
                
            }
        }

	}
    
    void Resume()
    {
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        GameIsPaused = false;
    }

    void Pause()
    {
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        GameIsPaused = true;
    }

    void defaulButton()
    {
        GameObject selectedButton = GameObject.Find("PokedexButton");
        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(selectedButton);

    }
}
