using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class BreakTimer : MonoBehaviour
{
    private MainController _myController;
    public float _timeLeft = 5 * 60;
    [SerializeField] private TMP_Text _timerText;

    private void Start()
    {
        _myController = _myController = GameObject.Find("MainController").GetComponent<MainController>();
    }
    private void DecreaseTimer()
    {
        if (_timeLeft > Time.deltaTime) _timeLeft -= Time.deltaTime;
        else TimerCompletion();
    }

    private void TimerCompletion()
    {
        if (_myController.GetTaskTime() > 0) SceneManager.LoadScene("WorkTimer", LoadSceneMode.Single);
        else
        {
            SceneManager.LoadScene("GoalCreation", LoadSceneMode.Single);
        }
    }

    // Update is called once per frame
    void Update()
    {
        DecreaseTimer();

        float min = Mathf.Floor(_timeLeft / 60);
        float seconds = _timeLeft % 60;
        _timerText.text = min + " : " + Mathf.Round(seconds);
    }
}
