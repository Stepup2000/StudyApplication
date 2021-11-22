using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class MainController : MonoBehaviour
{
    public Character currentCharacterPrefab;
    [SerializeField] private Goal _goalPrefab;
    private Character _characterInstance;

    private string[] nameArray = new string[] { "Math", "English", "Chemistry" };

    // Start is called before the first frame update
    private void Start()
    {
        SwitchCharacter(currentCharacterPrefab);
        LoadScene("GoalCreation");
    }

    public void SwitchCharacter(Character pCharacter)
    {
        if (_characterInstance != null) Destroy(_characterInstance.gameObject);
        currentCharacterPrefab = pCharacter;
        _characterInstance = Instantiate<Character>(currentCharacterPrefab, transform.position, Quaternion.identity);
    }

    private void CreateGoal(int goalnumber)
    {
        Vector3 position = new Vector3(0, 720 - (goalnumber * 550), 0);
        var goal = Instantiate<Goal>(_goalPrefab, position, Quaternion.identity);
        var goalParent = GameObject.Find("Goals");
        goal.transform.SetParent(goalParent.transform, false);
        goal.SetGoalName("" + nameArray[goalnumber]);
    }

    public void LoadScene(string sceneName)
    {
        //SceneManager.LoadScene(sceneName, LoadSceneMode.Single);

        switch (sceneName)
        {
            case "GoalCreation":
                for (int i = 0; i < nameArray.Length; i++) CreateGoal(i);
                break;
        }
    }
}
