using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DataGoal
{
    private int _goalNumber;

    public int goalid;
    public int status;
    public string gname;
    public int priority;

    public DataGoal(int i, int s, string n, int p)
    {
        goalid = i;
        status = s;
        gname = n;
        priority = p;
    }


    public void SetGoalNumber(int pNumber)
    {
        goalid = pNumber;
    }
}