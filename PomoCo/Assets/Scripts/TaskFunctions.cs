using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TaskFunctions : MonoBehaviour
{
    private MainController _myController;
    private Task myTaskScript;
    [SerializeField] private TMP_InputField _inputFieldName;

    [SerializeField] private TMP_InputField _inputFieldTime;

    [SerializeField] private TMP_InputField _inputFieldReward;


    // Start is called before the first frame update
    void Start()
    {
        _myController = GameObject.Find("MainController").GetComponent<MainController>();
        myTaskScript = transform.gameObject.GetComponent<Task>();
        //Debug.Log("Create goal controller");
        //_myController.LoadAllGoals();
    }

    public void AddTask()
    {
        //_myController.SetSelectedTask(myTaskScript.taskid);
        _myController.CreateTask();
    }

    public void SetTaskName()
    {
        _myController.SetSelectedTask(myTaskScript.taskid);
        _myController.SetTaskName(_inputFieldName.text);
    }

    public void SetTaskReward()
    {
        _myController.SetSelectedTask(myTaskScript.taskid);
        _myController.SetTaskReward(_inputFieldReward.text);
    }
    public void SetTaskTime()
    {
        _myController.SetSelectedTask(myTaskScript.taskid);
        int i = int.Parse(_inputFieldTime.text);
        _myController.SetTaskTime(i);
    }

    public void SetTaskStatus(int s)
    {
        _myController.SetSelectedTask(myTaskScript.taskid);
        _myController.SetTaskStatus(s);
    }

    public void SetTaskPrio(int p)
    {
        _myController.SetTaskPrio(p);
    }

    public void SetSelectedTask()
    {
        _myController.SetSelectedTask(myTaskScript.taskid);
    }
}
