using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoalFunctions : MonoBehaviour
{
    private MainController _myController;


    private Goal myGoalScript;


    // Start is called before the first frame update
    void Start()
    {
        _myController = GameObject.Find("MainController").GetComponent<MainController>();
        myGoalScript = transform.gameObject.GetComponent<Goal>();
        Debug.Log("Create goal controller");
        //_myController.LoadAllGoals();
    }


    public void SetSelectedGoal()
    {
        _myController.setSelectedGoalID(myGoalScript.GetGoalID());
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
