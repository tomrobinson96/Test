using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class GlobalTimer : MonoBehaviour
{
    public Text timer;
    public Text pausedTimer;
    public Text finishedTime;    
    float gameTimer = 0f;

    public string timerString;

    public Text metersTravelled;
    public Text finMetersTravelled;
    float gameMetersTravelled = 0f;

    public JetController bS;
    public GameObject isRace;
    bool race1;bool race2; bool dEndless1; bool dRace1;

    public Text highScore;
    public Text highScoreFinal;
    public Text highScore1;    
    public Text highScore1Final;    


    void Start()
    {
        // Create a temporary reference to the current scene.
        Scene currentScene = SceneManager.GetActiveScene();

        // Retrieve the name of this scene.
        string sceneName = currentScene.name;

        // Retrieve the index of the scene in the project's build settings.
        int buildIndex = currentScene.buildIndex;

        // Check the scene name as a conditional.
        switch (buildIndex)
        {
            case 1:
                highScore.text = PlayerPrefs.GetInt("HighscoreE", 0).ToString();
                highScoreFinal.text = PlayerPrefs.GetInt("HighscoreE", 0).ToString();
                break;
            case 2:
                highScore1.text = PlayerPrefs.GetFloat("HighscoreR1", 9999).ToString();
                highScore1Final.text = PlayerPrefs.GetFloat("HighscoreR1", 9999).ToString();
                race1 = true;
                break;
            case 3:
                highScore1.text = PlayerPrefs.GetFloat("HighscoreR2", 99999).ToString();
                highScore1Final.text = PlayerPrefs.GetFloat("HighscoreR2", 99999).ToString();
                race2 = true;
                break;
            case 5:
                highScore1.text = PlayerPrefs.GetFloat("HighscoreDE", 99999).ToString();
                highScore1Final.text = PlayerPrefs.GetFloat("HighscoreDE", 99999).ToString();
                dEndless1 = true;
                break;
            case 6:
                highScore1.text = PlayerPrefs.GetFloat("HighscoreDR1", 99999).ToString();
                highScore1Final.text = PlayerPrefs.GetFloat("HighscoreDR1", 99999).ToString();
                dRace1 = true;
                break;
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        gameTimer += Time.deltaTime;
        float fraction = gameTimer * 1000;
        fraction = (fraction % 1000);
        int seconds = (int)(gameTimer % 60);
        int minutes = (int)(gameTimer / 60) % 60;
        
        timerString = string.Format("{0:00}:{1:00}:{2:000}", minutes, seconds, fraction);
        timer.text = timerString;
        pausedTimer.text = timerString;
        finishedTime.text = timerString;

        if (Input.GetMouseButton(0))
        {
            if (bS.boosting)
            {
                gameMetersTravelled += (Time.deltaTime * 4) * 2;
            }
            else
            {
                gameMetersTravelled += Time.deltaTime * 4;
            }
        }
        int met = (int)(gameMetersTravelled);
        string metersString = string.Format("{0} Meters", met);

        metersTravelled.text = metersString;
        finMetersTravelled.text = metersString;


        if (!isRace.activeSelf)
        {
            if (met > PlayerPrefs.GetInt("HighscoreE", 0))
            {
                PlayerPrefs.SetInt("HighscoreE", met);
                highScore.text = met.ToString();
                highScoreFinal.text = met.ToString();
            }
        }
        if (dEndless1 == true)
        {
            if (met > PlayerPrefs.GetInt("HighscoreDE", 0))
            {
                PlayerPrefs.SetInt("HighscoreDE", met);
                highScore.text = met.ToString();
                highScoreFinal.text = met.ToString();
            }
        }

        if (race1 == true)
        {
            if (gameTimer < PlayerPrefs.GetFloat("HighscoreR1", 9999))
            {
                highScore1.text = PlayerPrefs.GetFloat("HighscoreR1", 9999).ToString();
            }            
        }
        if (race2 == true)
        {
            if (gameTimer < PlayerPrefs.GetFloat("HighscoreR2", 99999))
            {
                highScore1.text = PlayerPrefs.GetFloat("HighscoreR2", 99999).ToString();
            }
        }
        if (dRace1 == true)
        {
            if (gameTimer < PlayerPrefs.GetFloat("HighscoreDR1", 99999))
            {
                highScore1.text = PlayerPrefs.GetFloat("HighscoreDR1", 99999).ToString();
            }
        }
    }

    public void SetRecord()
    {
        if (race1 == true)
        {
            if (gameTimer < PlayerPrefs.GetFloat("HighscoreR1", 9999))
            {
                PlayerPrefs.SetFloat("HighscoreR1", gameTimer);
                highScore1Final.text = gameTimer.ToString();
                print("set 1");
            }
        }
        if (race2 == true)
        {
            if (gameTimer < PlayerPrefs.GetFloat("HighscoreR2", 99999))
            {
                PlayerPrefs.SetFloat("HighscoreR2", gameTimer);
                highScore1Final.text = gameTimer.ToString();
                print("set 2");
            }
        }
        if (dRace1 == true)
        {
            if (gameTimer < PlayerPrefs.GetFloat("HighscoreDR1", 99999))
            {
                PlayerPrefs.SetFloat("HighscoreDR1", gameTimer);
                highScore1Final.text = gameTimer.ToString();
                print("set 3");
            }
        }
    }
}
