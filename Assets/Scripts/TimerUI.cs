using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TimerUI : MonoBehaviour
{
    [SerializeField] TMP_Text timerText;
    [SerializeField] string timerPreface = "Time: ";

    void Update()
    {
        TimeSpan time = TimeSpan.FromSeconds(Time.timeSinceLevelLoad);
        timerText.text = timerPreface + time.ToString(@"mm\:ss\.ff");    
    }
}
