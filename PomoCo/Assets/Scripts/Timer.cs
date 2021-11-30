using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Timer : MonoBehaviour
{
    private float  _timeLeft = 70;
    [SerializeField] private TMP_Text _timerText;


    private void Update()
    {
        _timeLeft -= Time.deltaTime;

        float min = Mathf.Floor(_timeLeft / 60);
        float seconds = _timeLeft % 60;
        _timerText.text = "Time left: " + min + " : " + Mathf.Round(seconds);
    }
}
