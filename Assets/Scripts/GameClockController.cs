using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameClockController : MonoBehaviour
{
    [Header("Clock UI")]
    [SerializeField] private TMP_Text clockText;
    private float elapsedTime;

    [Header("Time in a day")]
    [SerializeField] private float timeInADay = 86400f; // 24 hours = 86400 seconds

    [Header("How Fast Time Should Pass")]
    [SerializeField] private float timeScale = 2.0f;

    private void Start()
    {
        elapsedTime = 12 * 3600f; // set a time
    }

    private void Update()
    {
        elapsedTime += Time.deltaTime * timeScale;
        elapsedTime %= timeInADay;
        UpdateClockUI();
    }

    void UpdateClockUI()
    {
        int hours = Mathf.FloorToInt(elapsedTime / 3600f);
        int minutes = Mathf.FloorToInt((elapsedTime - hours * 3600f) / 60f);
        int seconds = Mathf.FloorToInt((elapsedTime - hours * 3600f) - (minutes * 60f));

        string clockString = string.Format("{0:00}:{1:00}:{2:00}", hours, minutes, seconds);
        clockText.text = clockString;
    }
}
