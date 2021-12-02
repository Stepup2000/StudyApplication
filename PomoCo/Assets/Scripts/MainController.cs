using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UI;


public class MainController : MonoBehaviour
{
    public Character currentCharacterPrefab;
    [SerializeField] private Goal _goalPrefab;
    [SerializeField] private Task _taskPrefab;
    [SerializeField] private Note _notePrefab;
    [SerializeField] private Button popUp;
    private Character _characterInstance;
    private ArrayList _goalList = new ArrayList();
    private ArrayList _taskList = new ArrayList();
    private ArrayList _noteList = new ArrayList();

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

    //Notes

    private int loadedNoteCount = 0;
    private string noteText;
    private string noteCategory;
    private int note_ID;

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
        Screen.SetResolution(576, 1024, false);
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
        UpdateUserType(userLearningType);
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
            userAvatar = user.avatar;
            userLearningType = user.learningType;
            userName = user.name;
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
        selectedGoalID = gid;
    }
    public void SetGoalName(string n)
    {
        goalName = n;
    }

    public void SetSelectedGoalName(int ID, string n)
    {
        goalName = n;
        UpdateGoal(n, ID);
    }

    public void SetGoalPrio(int p)
    {
        goalPrio = p;
    }

    public void SetGoalStatus(int s)
    {
        goalStatus = s;
        database.UpdateGoalStatus(goalStatus, selectedGoalID);
    }

    public void LoadAllGoals()
    {
        taskStatus = 0;

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

        if (taskTime >= 101)
        {
            CreatePopUpWithInput("Dont you want to split this task up?");
        }
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
        taskTime = 1;
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
        taskStatus = 0;
    }

    public void UpdateTaskReward(string r, int tid)
    {
        database.UpdateTaskReward(r, tid);
        _taskList = database.ReadAllTasks();
    }

    public int GetTaskTime()
    {
        return taskTime;
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
        //loadedNoteCount = 0;
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

        if (dt.completed == 1)
        {
            //Debug.Log("Completed goal");
            var headerButton = task.GetComponent<Button>();
            headerButton.interactable = false;
            ColorBlock colorBlock = headerButton.colors;
            colorBlock.normalColor = Color.green;
        }
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

    //Notes----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------//

    public void LoadNotes()
    {
        loadedNoteCount = 0;
        _noteList = database.ReadNotesForTaskX(selectedTaskID);
        for (int i = 0; i < _noteList.Count; i++)
        {
            DataNote currentNote = (DataNote)_noteList[i];
            LoadSingleNote(currentNote);
        }

    }

    public void LoadSingleNote(DataNote dn)
    {
        loadedNoteCount++;
        Vector3 position = new Vector3(0, 680 - ((loadedNoteCount - 1) * 550), 0);
        var note = Instantiate<Note>(_notePrefab, position, Quaternion.identity);
        var scrollContainer = GameObject.Find("Notes");
        note.transform.SetParent(scrollContainer.transform, false);

        note.SetCategory(dn.category);
        note.SetText(dn.text);
        note.note_id = dn.note_id;
        note.task_id = dn.task_id;

        var plusButton = GameObject.Find("AddNoteButton");
        plusButton.transform.position += new Vector3(0, -220, 0);
    }

    public void CreateNote()
    {
        _noteList = database.ReadNotesForTaskX(selectedTaskID);
        if (_noteList.Count < 3)
        {
            loadedNoteCount++;
            Vector3 position = new Vector3(0, 680 - ((loadedNoteCount - 1) * 550), 0);
            var note = Instantiate<Note>(_notePrefab, position, Quaternion.identity);

            Debug.Log(loadedNoteCount);

            database.CreateNote(noteText, noteCategory, selectedTaskID);
            _noteList = database.ReadNotesForTaskX(selectedTaskID);


            var scrollContainer = GameObject.Find("Notes");
            note.transform.SetParent(scrollContainer.transform, false);

            note.category = ((DataNote)_noteList[_noteList.Count - 1]).category;
            note.text = ((DataNote)_noteList[_noteList.Count - 1]).text;
            note.note_id = ((DataNote)_noteList[_noteList.Count - 1]).note_id;
            note.task_id = ((DataNote)_noteList[_noteList.Count - 1]).task_id;

            var plusButton = GameObject.Find("AddNoteButton");
            plusButton.transform.position += new Vector3(0, -220, 0);
        }
    }

    public void SetNoteText(string t, int noteid)
    {
        noteText = t;
        note_ID = noteid;
        database.UpdateNoteText(noteText, note_ID);
    }

    public void SetNoteCategory(string c, int noteid)
    {
        noteCategory = c;
        note_ID = noteid;
        database.UpdateNoteCategory(noteCategory, note_ID);
    }

    //--------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------//

    public void CreatePopUp()
    {

        string t = "Hello " + userName + "!";

        Debug.Log("click");
        Vector3 position = new Vector3(0, -200, 0);
        var pop_up = Instantiate<Button>(popUp, position, Quaternion.identity);

        var container = GameObject.Find("Canvas");
        pop_up.transform.SetParent(container.transform, false);

        pop_up.GetComponentInChildren<TMP_Text>().text = t;
    }

    public void CreatePopUpWithInput(string t)
    {
        Vector3 position = new Vector3(0, -200, 0);
        var pop_up = Instantiate<Button>(popUp, position, Quaternion.identity);

        var container = GameObject.Find("Canvas");
        pop_up.transform.SetParent(container.transform, false);

        pop_up.GetComponentInChildren<TMP_Text>().text = t;
    }


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
