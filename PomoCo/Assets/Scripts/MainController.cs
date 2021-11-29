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
    private ArrayList _goalList = new ArrayList();
    private ArrayList _taskList = new ArrayList();

    //Davids Part --------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------//
    //--------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------//


    private DatabaseCon database = new DatabaseCon();

    //User
    private User user;
    private string userName = "default";
    private string userAvatar = "default";
    private string userLearningType = "default";

    //Goals
    private int selectedGoalID;
    private string goalName = "goal name";
    private int goalStatus = 1;
    private int goalPrio = 1;

    private void Start()
    {
        database.StartDB();

        if (!CheckForUser()) SceneManager.LoadScene("WelcomeScreen", LoadSceneMode.Single);
        else
        {
            SceneManager.LoadScene("MainMenu", LoadSceneMode.Single);
            GetDatabaseInformation();
        }
    }

    public void SetUserName(string name)
    {
        userName = name;
        UpdateUserName(userName);
    }

    public string GetUserName()
    {
        return userName;
    }

    public string GetUserLearningType()
    {
        return userLearningType;
    }

    public void SetUserLearningType(string lt)
    {
        userLearningType = lt;
    }

    public void SetUserAvatar(string avatar)
    {
        userAvatar = avatar;
        UpdateUserAvatar(userAvatar);
    }

    public string GetUserAvatar()
    {
        return userAvatar;
    }

    //If the name is changed in the name choosing screen this method should be called so the User can be updated if it already exists. 
    //The name is saved and the internal user updated. 
    public void UpdateUserName(string name)
    {
        if (CheckForUser())
        {
            database.UpdateUserName(name);
        }
        SetUserName(name);
        CheckForUser();
    }

    //If the learning type is changed in the name choosing screen this method should be called so the User can be updated if it already exists. 
    //The user type is saved and the internal user updated. 
    public void UpdateUserType(string ut)
    {
        if (CheckForUser())
        {
            database.UpdateUserName(ut);
        }
        SetUserLearningType(ut);
        CheckForUser();
    }

    //If the avatar is changed in the avatar choosing screen this method should be called so the User can be updated if it already exists. 
    //The avatar is saved and the internal user updated. 
    public void UpdateUserAvatar(string avatar)
    {
        if (CheckForUser())
        {
            database.UpdateUserAvatar(avatar);
        }
        SetUserAvatar(avatar);
        CheckForUser();
    }

    //Checks if a User exists in the database. Sets the User for further Use
    public bool CheckForUser()
    {
        ArrayList list = database.ReadUser();
        if (list.Count == 0)
        {
            return false;
        }
        else
        {
            user = (User)list[0];
        }
        return true;
    }

    //Adds a user to the database. Note that this will only work when there is no User already added to the database
    public void AddUser()
    {
        if (!CheckForUser())
        {
            database.CreateUser(userAvatar, userLearningType, userName);
            CheckForUser();
        }
    }

    //Goals---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------//
    //--------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------//

    //Loads all the goals from the Array List that contains the goals from the database and calls the function for each of them to be instantiated. 
    public void setSelectedGoalID(int gid)
    {
        //Debug.Log("asrkljgfnsrk " + gid);
        selectedGoalID = gid;
    }
    public void SetGoalName(string n)
    {
        goalName = n;
    }

    public void SetSelectedGoalName(int ID, string n)
    {
        goalName = n;
        //Debug.Log("new name " + n + " " + ID);
        UpdateGoal(n, ID);
    }

    public void SetGoalPrio(int p)
    {
        goalPrio = p;
    }

    public void SetGoalStatus(int s)
    {
        goalStatus = s;
    }

    public void LoadAllGoals()
    {
        for (int i = 0; i < _goalList.Count; i++)
        {
            DataGoal currentGoal = (DataGoal)_goalList[i];
            LoadSingleGoal(currentGoal);
        }
    }

    public void UpdateGoal(string gname, int gid)
    {
        database.UpdateGoalName(gname, gid);
        _goalList = database.ReadAllGoals();
    }

    public void CreateGoal()
    {
        if (_goalList.Count < 3)
        {
            Vector3 position = new Vector3(0, 780 - (_goalList.Count * 550), 0);
            var goal = Instantiate<Goal>(_goalPrefab, position, Quaternion.identity);


            database.CreateGoal(goalName, goalStatus, goalPrio);
            _goalList = database.ReadAllGoals();


            var scrollContainer = GameObject.Find("Goals");
            goal.transform.SetParent(scrollContainer.transform, false);

            goal.SetGoalName(((DataGoal)_goalList[_goalList.Count - 1]).gname);


            goal.SetGoalNumber(((DataGoal)_goalList[_goalList.Count - 1]).GetGoalID());


            var plusButton = GameObject.Find("AddGoalButton");
            plusButton.transform.position += new Vector3(0, -235, 0);
        }
    }

    public void LoadSingleGoal(DataGoal dg)
    {
        Debug.Log(dg.goalid);
        Vector3 position = new Vector3(0, 780 - ((dg.goalid - 1) * 550), 0);
        var goal = Instantiate<Goal>(_goalPrefab, position, Quaternion.identity);
        var scrollContainer = GameObject.Find("Goals");
        goal.transform.SetParent(scrollContainer.transform, false);
        goal.SetGoalName("" + dg.gname);
        goal.SetGoalNumber(dg.goalid);
        var plusButton = GameObject.Find("AddGoalButton");
        plusButton.transform.position += new Vector3(0, -235, 0);
    }

    //--------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------//


    public void GetDatabaseInformation()
    {
        _goalList = database.ReadAllGoals();
        _taskList = database.ReadAllTasks();
        CheckForUser();
    }

    public void SwitchCharacter(Character pCharacter)
    {
        if (_characterInstance != null) Destroy(_characterInstance.gameObject);
        currentCharacterPrefab = pCharacter;
        _characterInstance = Instantiate<Character>(currentCharacterPrefab, transform.position, Quaternion.identity);
    }


    /*
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

*/

}
