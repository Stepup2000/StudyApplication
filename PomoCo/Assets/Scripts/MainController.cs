using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;


public class MainController : MonoBehaviour
{
    public Character currentCharacterPrefab;
    [SerializeField] private Goal _goalPrefab;
    private Character _characterInstance;
    private int goalAmount = -1;
    private string[] nameArray = new string[3];
    private GameObject _plusButton;

    //Davids Part --------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------//
    //--------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------//
    private DatabaseCon database = new DatabaseCon();
    private User user;
    private string userName = "default";
    private string userAvatar = "default";
    private string userLearningType = "default";

    public void SetUserName(string name)
    {
        userName = name;
    }

    public void SetUserLearningType(string lt)
    {
        userLearningType = lt;
    }

    public void SetUserAvatar(string avatar)
    {
        userAvatar = avatar;
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
    //--------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------//
    //--------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------//


    // Start is called before the first frame update
    private void Start()
    {
        _plusButton = GameObject.Find("AddGoalButton");
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
            goalAmount += 1;
            Vector3 position = new Vector3(0, 780 - (goalAmount * 550), 0);
            var goal = Instantiate<Goal>(_goalPrefab, position, Quaternion.identity);
            var goalParent = GameObject.Find("Goals");
            goal.transform.SetParent(goalParent.transform, false);
            goal.SetGoalName("" + nameArray[goalAmount]);
            goal.SetGoalNumber(goalAmount);
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
