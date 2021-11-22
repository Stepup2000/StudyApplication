using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Task
{
    public int goalid;
    public int completed;
    public string taskname;
    public int time;

    public int id;

    public Task(int gid, int cmpld, string n, int t, int i)
    {
        goalid = gid;
        completed = cmpld;
        taskname = n;
        time = t;
        id = i;
    }
}
