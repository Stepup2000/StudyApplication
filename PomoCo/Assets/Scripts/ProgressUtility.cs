using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ProgressUtility : MonoBehaviour
{

    private MainController _myController;

    [SerializeField] public Image completedChart;
    [SerializeField] public Image uncompletedChart;



    public TextMeshProUGUI txt;




    // Start is called before the first frame update
    void Start()
    {
        _myController = GameObject.Find("MainController").GetComponent<MainController>();
        GetCompletedTasks();
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void GetCompletedTasks()
    {
        float completedTasks = (float)_myController.LoadCompletedTasks();
        float allTasks = (float)_myController.GetTaskCount();

        float percentage = 0.0f;
        if (allTasks != 0)
        {
            Debug.Log(completedTasks / allTasks);
            percentage = completedTasks / allTasks;
        }

        completedChart.fillAmount = percentage;

        txt.text = "You have " + completedTasks + " completed Tasks \n and " + (allTasks - completedTasks) + " Tasks still to do!";
    }
}
