using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Task : MonoBehaviour
{

    public int goalid;

    public int taskid;

    public string taskname;

    public int taskTime;

    public void SetTaskName(string pName)
    {
        taskname = pName;
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
    }

}
