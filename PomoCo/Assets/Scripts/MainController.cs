using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;


public class MainController : MonoBehaviour
{
    public Character currentCharacterPrefab;
    [SerializeField] private Goal _goalPrefab;
    [SerializeField] private Task _taskPrefab;
    private Character _characterInstance;
    private string[] nameArray = new string[3];
    private ArrayList _goalList = new ArrayList();
    private ArrayList _taskList = new ArrayList();

    // Start is called before the first frame update
    private void Start()
    {
    }

    public void SwitchCharacter(Character pCharacter)
    {
        if (_characterInstance != null) Destroy(_characterInstance.gameObject);
        currentCharacterPrefab = pCharacter;
        _characterInstance = Instantiate<Character>(currentCharacterPrefab, transform.position, Quaternion.identity);
    }

    public void GetDatabaseInformation()
    {

    }

    public void CreateGoal()
    {
        if (_goalList.Count < 3)
        {
            Vector3 position = new Vector3(0, 780 - (_goalList.Count * 550), 0);
            var goal = Instantiate<Goal>(_goalPrefab, position, Quaternion.identity);
            _goalList.Add(goal);
            var scrollContainer = GameObject.Find("Goals");
            goal.transform.SetParent(scrollContainer.transform, false);
            goal.SetGoalName("Insert name for goal_" + _goalList.Count);
            goal.SetGoalNumber(_goalList.Count);
            var plusButton = GameObject.Find("AddGoalButton");
            plusButton.transform.position += new Vector3(0, -280, 0);
        }
    }

    public void LoadGoal(int ID)
    {
        Vector3 position = new Vector3(0, 780 - (ID * 550), 0);
        var goal = Instantiate<Goal>(_goalPrefab, position, Quaternion.identity);
        var scrollContainer = GameObject.Find("Goals");
        goal.transform.SetParent(scrollContainer.transform, false);
        Goal listEntry = (Goal)_goalList[ID];
        goal.SetGoalName("" + listEntry.name);
        goal.SetGoalNumber(ID);
        var plusButton = GameObject.Find("AddGoalButton");
        plusButton.transform.position += new Vector3(0, -280, 0);
    }

    public void CreateTask()
    {
        if (_taskList.Count < 3)
        {
            Vector3 position = new Vector3(0, 780 - ((_taskList.Count + 1) * 550), 0);
            var task = Instantiate<Task>(_taskPrefab, position, Quaternion.identity);
            _taskList.Add(task);
            var scrollContainer = GameObject.Find("Tasks");
            task.transform.SetParent(scrollContainer.transform, false);
            task.SetTaskName("" + nameArray[_taskList.Count + 1]);
            task.SetTaskSetTaskID(_taskList.Count + 1);
            var plusButton = GameObject.Find("AddTaskButton");
            plusButton.transform.position += new Vector3(0, -280, 0);
        }
    }

    public void ChangeGoalName(int ID, string name)
    {

        nameArray[ID] = name;
        Debug.Log(nameArray[ID]);
    }

    public void LoadScene(string sceneName)
    {
        //SceneManager.LoadScene(sceneName, LoadSceneMode.Single);

        switch (sceneName)
        {
            case "GoalCreation":
                for (int i = 0; i < nameArray.Length; i++) CreateGoal();
                break;
        }
    }
}
