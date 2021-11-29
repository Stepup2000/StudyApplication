using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TaskFunctions : MonoBehaviour
{
    private MainController _myController;


    private Task myTaskScript;


    // Start is called before the first frame update
    void Start()
    {
        _myController = GameObject.Find("MainController").GetComponent<MainController>();
        myTaskScript = transform.gameObject.GetComponent<Task>();
        Debug.Log("Create goal controller");
        //_myController.LoadAllGoals();
    }

    public void AddTask()
    {
        _myController.CreateTask();
    }

    public void SetTaskName(string n)
    {
        _myController.SetTaskName(n);
    }

    public void SetTaskTime(int t)
    {
        _myController.SetTaskTime(t);
    }

    public void SetTaskStatus(int s)
    {
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
