using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreatePopUpScript : MonoBehaviour
{
    private MainController _myController;
    // Start is called before the first frame update
    void Start()
    {
        _myController = GameObject.Find("MainController").GetComponent<MainController>();
    }

    public void CreatePopUpNow()
    {
        _myController.CreatePopUp();
    }

}
