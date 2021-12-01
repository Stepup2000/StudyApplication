using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class CreateGoalName : MonoBehaviour
{

    [SerializeField] private TMP_InputField _inputField;
    private MainController _myController;
    [SerializeField] private Goal selectedGoal;


    // Start is called before the first frame update
    void Start()
    {
        _myController = GameObject.Find("MainController").GetComponent<MainController>();
    }

    public void SetGoalName()
    {
        _myController.SetSelectedGoalName(selectedGoal.GetGoalID(), _inputField.text);
    }
}
