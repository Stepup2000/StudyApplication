using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoalFunctions : MonoBehaviour
{
    private MainController _myController;


    // Start is called before the first frame update
    void Start()
    {
        _myController = GameObject.Find("MainController").GetComponent<MainController>();
        Debug.Log("Create goal controller");
        //_myController.LoadAllGoals();
    }


    public void SetSelectedGoal(int id)
    {
        _myController.setSelectedGoalID(id);
    }

    public void SetGoalName(string n)
    {
        _myController.SetGoalName(n);
    }

    public void SetGoalStatus(int n)
    {
        _myController.SetGoalStatus(n);
    }

    public void SetGoalPrio(int n)
    {
        _myController.SetGoalPrio(n);
    }

    public void AddNewGoal()
    {
        _myController.CreateGoal();
    }

}
