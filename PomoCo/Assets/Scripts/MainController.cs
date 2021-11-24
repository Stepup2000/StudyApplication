using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;


public class MainController : MonoBehaviour
{
    public Character currentCharacterPrefab;
    [SerializeField] private Goal _goalPrefab;
    //[SerializeField] private Task _taskPrefab;
    private Character _characterInstance;
    private int goalAmount = -1;
    private int taskAmount = -1;
    private string[] nameArray = new string[3];
    private GameObject _plusButton;

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

    public void CreateGoal()
    {
        if (goalAmount < nameArray.Length - 1)
        {
            taskAmount += 1;
            Vector3 position = new Vector3(0, 780 - (goalAmount * 550), 0);
            var goal = Instantiate<Goal>(_goalPrefab, position, Quaternion.identity);
            var goalParent = GameObject.Find("Goals");
            goal.transform.SetParent(goalParent.transform, false);
            goal.SetGoalName("" + nameArray[goalAmount]);
            goal.SetGoalNumber(goalAmount);
            _plusButton = GameObject.Find("AddGoalButton");
            _plusButton.transform.position += new Vector3(0, -280, 0);
        }
    }

    public void CreateTask()
    {
        if (goalAmount < 3)
        {
            goalAmount += 1;
            Vector3 position = new Vector3(0, 780 - (taskAmount * 550), 0);
            //var task = Instantiate<Task>(_taskPrefab, position, Quaternion.identity);
            var taskParent = GameObject.Find("Tasks");
            //task.transform.SetParent(taskParent.transform, false);
            //task.SetTaskName("" + nameArray[taskAmount]);
            //task.SetTaskNumber(taskAmount);
            _plusButton = GameObject.Find("AddGoalButton");
            _plusButton.transform.position += new Vector3(0, -280, 0);
        }
    }

    public void ChangeGoalName(int number, string name)
    {

        nameArray[number] = name;
        Debug.Log(nameArray[number]);
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
