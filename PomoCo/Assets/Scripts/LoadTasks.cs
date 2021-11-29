using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadTasks : MonoBehaviour
{

    private MainController _myController;
    // Start is called before the first frame update
    void Start()
    {
        _myController = GameObject.Find("MainController").GetComponent<MainController>();
        _myController.LoadAllTasks();
    }


}