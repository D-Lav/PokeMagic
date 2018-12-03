using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PauseMenu : MonoBehaviour {

    public static bool GameIsPaused = false;
    public GameObject pauseMenuUI;
    PlayerControls player;

    private void Start()
    {
        player = GameObject.Find("PlayerComponents").GetComponent<PlayerControls>();
    }
    // Update is called once per frame
    void Update () {

        

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (GameIsPaused)
            {
                
                Resume();
                player.enabled = true;

            }
            else
            {
                player.enabled = false;
                Pause();
                defaulButton();
               
                
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
