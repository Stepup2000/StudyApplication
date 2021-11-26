using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Goal : MonoBehaviour
{
    [SerializeField] public TMP_Text _nameText;
    [SerializeField] private TMP_Text _placeHolder;

    public int ID;


    public void SetGoalName(string Pname)
    {
        _nameText.text = Pname;
        _placeHolder.text = Pname;
    }

    public void SetGoalNumber(int i)
    {
        ID = i;
    }

    public void ChangeGoalName()
    {
        var controller = GameObject.Find("MainController").GetComponent<MainController>();
        //controller.ChangeGoalName(ID, _nameText.text);
    }
}
