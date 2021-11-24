using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Goal : MonoBehaviour
{
    [SerializeField] private TMP_Text _nameText;
    [SerializeField] private TMP_Text _placeHolder;
    private int _goalNumber;
    public int id;
    public int status;
    public string gname;
    public int priority;

    public Goal(int i, int s, string n, int p)
    {
        id = i;
        status = s;
        gname = n;
        priority = p;
    }

    public void SetGoalName(string Pname)
    {
        _nameText.text = Pname;
        _placeHolder.text = Pname;
    }

    public void SetGoalNumber(int pNumber)
    {
        _goalNumber = pNumber;
    }

    public void ChangeGoalName()
    {
        var controller = GameObject.Find("MainController").GetComponent<MainController>();
        controller.ChangeGoalName(_goalNumber, _nameText.text);
    }
}
