using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadNotes : MonoBehaviour
{
    private MainController _myController;

    void Start()
    {
        _myController = GameObject.Find("MainController").GetComponent<MainController>();

        _myController.LoadNotes();
    }


}
