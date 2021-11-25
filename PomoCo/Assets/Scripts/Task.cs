using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Task : MonoBehaviour
{

    public int goalid;

    public string taskname;

    public void SetTaskName(string pName)
    {
        taskname = pName;
    }

    public void SetTaskSetTaskID(int pNumber)
    {
        goalid = pNumber;
    }

}
