using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIDisplay : MonoBehaviour
{
    [Header("Health")]
    [SerializeField] private Slider healthSlider;
    [SerializeField] private Health playerHealth;  // manually set in Unity UI since enemies will also have Health

    [Header("Score")]
    [SerializeField] private TextMeshProUGUI scoreText;

    private ScoreKeeper _scoreKeeper;

    private void Awake()
    {
        _scoreKeeper = FindObjectOfType<ScoreKeeper>();
    }

    private void Start()
    {
        healthSlider.maxValue = playerHealth.GetHealth();
    }

    private void Update()
    {
        healthSlider.value = playerHealth.GetHealth();
        scoreText.text = _scoreKeeper.GetScore().ToString("000000000");  // pad with leading zeroes
    }
}
