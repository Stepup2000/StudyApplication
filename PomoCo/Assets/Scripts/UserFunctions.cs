using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UserFunctions : MonoBehaviour
{
    private MainController _myController;
    private void Start()
    {
        _myController = GameObject.Find("MainController").GetComponent<MainController>();
    }

    public void SetUserAvatar(string pAvatar)
    {
        _myController.SetUserAvatar(pAvatar);
    }

    public void SetUserLearningType(string pLearningType)
    {
        _myController.SetUserLearningType(pLearningType);
    }

    public void CreateUser()
    {
        _myController.AddUser();
    }
}
