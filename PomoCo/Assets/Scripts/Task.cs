using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Task : MonoBehaviour
{

    public int goalid;

    public int taskid;

    public string taskname;

    public int taskTime;

    public string taskReward;

    [SerializeField] public TMP_InputField _nameText;
    [SerializeField] public TMP_InputField _timeText;
    [SerializeField] public TMP_InputField _rewardText;


    public void SetTaskName(string pName)
    {
        taskname = pName;
        _nameText.text = taskname;

    }

    public void SetTaskReward(string r)
    {
        taskReward = r;
        _rewardText.text = taskReward;
    }

    public void SetTaskID(int pNumber)
    {
        taskid = pNumber;
    }

    public void SetGoalID(int pNumber)
    {
        goalid = pNumber;
    }

    public void SetTaskTime(int ti)
    {
        taskTime = ti;
        _timeText.text = "" + taskTime;
    }
}
