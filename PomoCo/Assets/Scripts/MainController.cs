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
    private int selectedGoalID = 0;
    private string goalName = "goal name";
    private int goalStatus = 0;
    private int goalPrio = 1;

    private int loadedGoalsCount = 0;

    //Tasks
    private int taskTime = 100;
    private string taskName = "task name";

    private string taskReward = "";

    private int taskStatus = 0;
    private int taskPrio = 0;
    private int selectedTaskID;

    private int loadedTasksCount = 0;

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
        //SetUserName(name);
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
        //SetUserLearningType(ut);
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
        //SetUserAvatar(avatar);
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
        loadedGoalsCount = 0;
        for (int i = 0; i < _goalList.Count; i++)
        {
            DataGoal currentGoal = (DataGoal)_goalList[i];
            LoadSingleGoal(currentGoal);
        }
        loadedTasksCount = 0;
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
            loadedGoalsCount++;
            Vector3 position = new Vector3(0, 780 - ((loadedGoalsCount - 1) * 550), 0);
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
        loadedGoalsCount++;
        Vector3 position = new Vector3(0, 780 - ((loadedGoalsCount - 1) * 550), 0);
        var goal = Instantiate<Goal>(_goalPrefab, position, Quaternion.identity);
        var scrollContainer = GameObject.Find("Goals");
        goal.transform.SetParent(scrollContainer.transform, false);
        goal.SetGoalName("" + dg.gname);
        goal.SetGoalNumber(dg.goalid);
        var plusButton = GameObject.Find("AddGoalButton");
        plusButton.transform.position += new Vector3(0, -235, 0);
    }

    //Tasks----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------//

    public void SetTaskReward(string r)
    {
        taskReward = r;
        UpdateTaskReward(r, selectedTaskID);
    }

    public void SetTaskName(string n)
    {
        taskName = n;
        UpdateTaskName(taskName, selectedTaskID);
    }

    public void SetTaskTime(int t)
    {
        taskTime = t;
        UpdateTaskTime(taskTime, selectedTaskID);
    }

    public void SetTaskStatus(int s)
    {
        taskStatus = s;
        database.UpdateTaskStatus(taskStatus, selectedTaskID);
    }

    public void SetTaskPrio(int p)
    {
        taskPrio = p;
    }

    public void SetSelectedTask(int id)
    {
        selectedTaskID = id;
    }

    public void LoadAllTasks()
    {
        //Debug.Log("selected goal: " + selectedGoalID);
        _taskList = database.ReadTasksForGoalX(selectedGoalID);
        for (int i = 0; i < _taskList.Count; i++)
        {
            DataTask currentTask = (DataTask)_taskList[i];
            LoadSingleTask(currentTask);
        }
    }

    public void UpdateTaskName(string tname, int tid)
    {
        database.UpdateTaskName(tname, tid);
    }

    public void UpdateTaskTime(int time, int tid)
    {
        database.UpdateTaskTime(time, tid);
        _taskList = database.ReadAllTasks();
    }

    public void UpdateTaskReward(string r, int tid)
    {
        database.UpdateTaskReward(r, tid);
        _taskList = database.ReadAllTasks();
    }

    public void CreateTask()
    {
        _taskList = database.ReadTasksForGoalX(selectedGoalID);
        if (_taskList.Count < 3)
        {
            loadedTasksCount++;
            Vector3 position = new Vector3(0, 780 - ((loadedTasksCount - 1) * 550), 0);
            var task = Instantiate<Task>(_taskPrefab, position, Quaternion.identity);

            Debug.Log(selectedGoalID);

            database.CreateTask(selectedGoalID, taskName, taskTime, taskStatus, taskPrio, taskReward);
            _taskList = database.ReadAllTasks();


            var scrollContainer = GameObject.Find("Tasks");
            task.transform.SetParent(scrollContainer.transform, false);


            task.SetTaskName(((DataTask)_taskList[_taskList.Count - 1]).taskname);
            task.SetTaskTime(((DataTask)_taskList[_taskList.Count - 1]).time);
            task.SetTaskID(((DataTask)_taskList[_taskList.Count - 1]).id);

            var plusButton = GameObject.Find("AddTaskButton");
            plusButton.transform.position += new Vector3(0, -235, 0);

            database.UpdateGoalStatus(0, selectedGoalID);
        }
    }

    public void LoadSingleTask(DataTask dt)
    {
        //Debug.Log(loadedTasksCount);
        loadedTasksCount++;
        Vector3 position = new Vector3(0, 780 - ((loadedTasksCount - 1) * 550), 0);
        var task = Instantiate<Task>(_taskPrefab, position, Quaternion.identity);
        var scrollContainer = GameObject.Find("Tasks");
        task.transform.SetParent(scrollContainer.transform, false);

        task.SetTaskName("" + dt.taskname);
        task.SetTaskID(dt.id);
        task.SetTaskTime(dt.time);
        task.SetTaskReward(dt.reward);

        var plusButton = GameObject.Find("AddTaskButton");
        plusButton.transform.position += new Vector3(0, -235, 0);
    }

    //Loads all the completed Tasks
    public int LoadCompletedTasks()
    {
        ArrayList completedTaskList = database.ReadCompletedTasks();
        return completedTaskList.Count;
    }

    //Returns the amount of tasks planned
    public int GetTaskCount()
    {
        GetDatabaseInformation();
        return _taskList.Count;
    }

    //Tasks----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------//

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



}
