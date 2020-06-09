﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;



public class Timer : MonoBehaviour
{
    [SerializeField] AudioSource audioSource;           //alarm sound
    [SerializeField] Text timerText;                    //timer's countdown text field

    public void StartTimer (float sec)                  //getting seconds from the corresponding button
    {
        Screen.sleepTimeout = SleepTimeout.NeverSleep;
        StopAllCoroutines();
        timerText.text = "0' 00\"";
        StartCoroutine("TimerText", sec);
    }


    IEnumerator TimerText(float s)
    {
        float _inputSeconds = s;                        //saving seconds to temp variable
        string minSec;                                  //string variable for building timer's text
        bool sound = true;

        while (true)
        {                                               //converting from sec to min:sec
            minSec = string.Format("{0}' {1:00}\"",
                     Mathf.Floor(_inputSeconds / 60), _inputSeconds % 60);
            timerText.text = minSec;                    //displaying min:sec time on screen
            yield return new WaitForSecondsRealtime(1);
            _inputSeconds--;                            //counting down
            if (_inputSeconds <= 1 && sound)
            {
                audioSource.Play(0);                    //playing sound 1 sec before timer goes to 
                Handheld.Vibrate();                     //zero, so sound comes first :)
                sound = false;                          //preventing sound to play again
            }

            if (_inputSeconds < 0)              
            {
                _inputSeconds = s;                      //setting number of seconds to initial value
                sound = true;                           //and enabling sounds
            }


        }
    }

  
  

    public void StopTimer()
    {

        StopAllCoroutines();
    }

    public void ExitApp()
    {
        Screen.sleepTimeout = SleepTimeout.SystemSetting;
        Application.Quit();
    }


    

}
