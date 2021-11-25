using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UserFunctions : MonoBehaviour
{
    private MainController _myController;
    private void Start()
    {
        _myController = GameObject.Find("MainController").GetComponent<MainController>();
        Debug.Log("Create controller");
    }

    public void SetUserAvatar(string pAvatar)
    {
        _myController.SetUserAvatar(pAvatar);
        Debug.Log(_myController.GetUserAvatar());
    }

    public void SetUserLearningType(string pLearningType)
    {
        _myController.SetUserLearningType(pLearningType);
        Debug.Log(_myController.GetUserLearningType());
    }

    public void CreateUser()
    {
        _myController.AddUser();
    }

    public void CreateGoal()
    {
        _myController.CreateGoal();
    }
}
