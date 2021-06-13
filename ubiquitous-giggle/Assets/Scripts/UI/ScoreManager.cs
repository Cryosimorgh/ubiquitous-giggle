using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class ScoreManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI pausemenuhighscore;
    [SerializeField] private TextMeshProUGUI ingameUIscore;
    [SerializeField] private FloatSO highscore;
    [SerializeField] private FloatSO score;
    void Start()
    {
        InvokeRepeating(nameof(DisplayHighScore), 0, 120f);
    }

    private void DisplayHighScore()
    {
        string newhighscoretext = $"Your longest attempt at surviving has lasted for {highscore.number} Days!";
        pausemenuhighscore.text = newhighscoretext;
        string newscoretext = $"Survived for {score.number} days!";
        ingameUIscore.text = newscoretext;
    }
}
