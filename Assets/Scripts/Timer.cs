using System;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    float currentTime;
    public Text timeText;
    bool timer;
    void Update()
    {
        if (timer)
        {
            currentTime = currentTime + Time.deltaTime;
        }
        TimeSpan time = TimeSpan.FromSeconds(currentTime);
        timeText.text = time.ToString(@"mm\:ss\:fff");
    }

    [ContextMenu("start")]
    public float stopTime()
    {
        timer = false;
        return currentTime;
    }

    [ContextMenu("stop")]
    public void startTime()
    {
        timer = true;
        currentTime = 0;
    }
}
