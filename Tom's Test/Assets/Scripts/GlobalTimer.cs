using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GlobalTimer : MonoBehaviour
{
    public Text timer;
    public Text pausedTimer;
    public Text finishedTime;    
    float gameTimer = 0f;

    public Text metersTravelled;
    public Text finMetersTravelled;
    float gameMetersTravelled = 0f;

    public JetController bS;

    // Update is called once per frame
    void Update()
    {
        gameTimer += Time.deltaTime;
        float fraction = gameTimer * 1000;
        fraction = (fraction % 1000);
        int seconds = (int)(gameTimer % 60);
        int minutes = (int)(gameTimer / 60) % 60;
        
        string timerString = string.Format("{0:00}:{1:00}:{2:000}", minutes, seconds, fraction);
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
    }
}
