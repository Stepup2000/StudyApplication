using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class WorkTimer : MonoBehaviour
{
    private MainController _myController;
    public float _originalTimeLeft;
    public float _passedTime;
    public float _timeLeft;
    [SerializeField] private TMP_Text _timerText;

    private void Start()
    {
        _myController = _myController = GameObject.Find("MainController").GetComponent<MainController>();
        GetSessionduration();
    }

    private void GetSessionduration()
    {
        if (_myController.GetTaskTime() * 60 > 30 * 60)
        {
            _originalTimeLeft = _myController.GetTaskTime() * 60;
            _timeLeft = 25 * 60;
        }
        else
        {
            _originalTimeLeft = _myController.GetTaskTime() * 60;
            _timeLeft = _originalTimeLeft;
        }
    }

    private void DecreaseTimer()
    {
        if (_timeLeft > Time.deltaTime)
        {
            _timeLeft -= Time.deltaTime;
            _passedTime += Time.deltaTime;

            float min = Mathf.Floor(_timeLeft / 60);
            float seconds = _timeLeft % 60;
            _timerText.text = min + " : " + Mathf.Round(seconds);
        }
        else if (_timeLeft != -1) TimerCompletion();
    }

    public void BreakOffSession()
    {
        int timePassed = (int)Mathf.Round((_originalTimeLeft - _passedTime) / 60);

        _myController.SetTaskTime(timePassed);

        //SceneManager.LoadScene("WorkTimer", LoadSceneMode.Single);
    }

    private void TimerCompletion()
    {
        int timePassed = (int)Mathf.Round((_originalTimeLeft - _passedTime) / 60);
        Debug.Log(timePassed);
        _myController.SetTaskTime(timePassed);

        if (_myController.GetTaskTime() > 0) SceneManager.LoadScene("BreakTimer", LoadSceneMode.Single);
        else
        {
            _myController.SetTaskStatus(1);
            SceneManager.LoadScene("GoalCreation", LoadSceneMode.Single);
        }
    }

    private void Update()
    {
        DecreaseTimer();
    }
}
