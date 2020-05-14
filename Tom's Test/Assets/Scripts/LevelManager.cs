using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelManager : MonoBehaviour
{
    public GameObject pauseMenu;
    public GameObject finishMenu;
    public GameObject isRace;

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape)) 
        {
            if(pauseMenu.activeSelf)
            {
                PauseMenuRemove();
            }
            else
            {
                PauseMenuLoad();
            }
        }
        if(finishMenu.activeSelf)
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                SceneManager.LoadScene(0);
            }
            if (Input.GetKeyDown(KeyCode.Return))
            {
                if (!isRace.activeSelf)
                {
                    Time.timeScale = 0f;
                    LoadEndless(1);
                    Time.timeScale = 1f;
                }
                else
                {
                    Time.timeScale = 0f;
                    LoadRace(1);
                    Time.timeScale = 1f;
                }
            }
        }
    }

    public void LoadEndless(int sceneNumber)
    {
        SceneManager.LoadScene(1);
    }
    public void LoadRace(int sceneNumber)
    {
        SceneManager.LoadScene(2);
    }
    public void LoadMainMenu(int sceneNumber)
    {
        SceneManager.LoadScene(0);
    }
    public void RelaodScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    public void NextRace()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
    public void PauseMenuLoad()
    {
        pauseMenu.SetActive(true);
        Time.timeScale = 0f;
    }

    public void PauseMenuRemove()
    {
        pauseMenu.SetActive(false);
        Time.timeScale = 1f;
    }
}
