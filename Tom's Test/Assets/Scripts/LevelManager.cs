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
                    RelaodScene();
                    Time.timeScale = 1f;
                }
                else
                {
                    Time.timeScale = 0f;
                    RelaodScene();
                    Time.timeScale = 1f;
                }
            }
        }
    }

    public void LoadMainMenu(int sceneNumber)
    {
        SceneManager.LoadScene(0);
    }
    public void LoadInstructions(int sceneNumber)
    {
        SceneManager.LoadScene(8);
    }
    public void LoadPractice(int sceneNumber)
    {
        SceneManager.LoadScene(9);
    }
    public void LoadMainMenuDark(int sceneNumber)
    {
        SceneManager.LoadScene(4);
    }
    public void RelaodScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    public void NextLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
    public void Race()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 2);
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
