using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShowcaseAvatar : MonoBehaviour
{
    private MainController _myController;
    [SerializeField] string _name;

    // Start is called before the first frame update
    void Start()
    {
        _myController = GameObject.Find("MainController").GetComponent<MainController>();
        LoadStudyBuddy();
    }

    public void LoadStudyBuddy()
    {
        var learningType = _myController.GetUserLearningType();
        var avatar = _name + "_" + learningType;

        var image = gameObject.GetComponent<Image>();
        image.sprite = Resources.Load<Sprite>(avatar);
    }
}
