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

    public void SetGoalName(string Pname)
    {
        _nameText.text = Pname;
        _placeHolder.text = Pname;
    }
    public Goal(int i, int s, string n, int p)
    {
        id = i;
        status = s;
        gname = n;
        priority = p;
    }

}
