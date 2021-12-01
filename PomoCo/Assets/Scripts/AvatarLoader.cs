using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AvatarLoader : MonoBehaviour
{
    private MainController _myController;

    // Start is called before the first frame update
    void Start()
    {
        _myController = GameObject.Find("MainController").GetComponent<MainController>();
        LoadStudyBuddy();
    }

    private void LoadStudyBuddy()
    {
        var choosenAvatar = _myController.GetUserAvatar();
        var learningType = _myController.GetUserLearningType();
        var avatar = choosenAvatar + "_" + learningType;

        var image = gameObject.GetComponent<Image>();
        image.sprite = Resources.Load<Sprite>(avatar);
    }
}