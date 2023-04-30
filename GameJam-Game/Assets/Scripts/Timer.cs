using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour{

    float currentTime = 1;
    float stageTime = 300;

    public TextMeshProUGUI CountdownText;

    private void Start()
    {
        currentTime = stageTime;
    }

    private void Update()
    {
        currentTime = currentTime - 1 * Time.deltaTime;
        CountdownText.text = currentTime.ToString("N0");

        if (currentTime <= 0)
        {
            currentTime = 0;
        }
    }
}
