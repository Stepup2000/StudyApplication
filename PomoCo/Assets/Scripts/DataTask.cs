using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataTask
{
    public int goalid;
    public int completed;

    public string taskname;

    public int time;
    public int id;
    public int priority;

    public string reward;

    public DataTask(int gid, int cmpld, string n, int t, int i, int p, string r)
    {
        goalid = gid;
        completed = cmpld;
        taskname = n;
        time = t;
        id = i;
        priority = p;
        reward = r;
    }

    public void SetTaskName(string pName)
    {
        taskname = pName;
    }

    public void SetTaskSetTaskID(int pNumber)
    {
        goalid = pNumber;
    }
}