using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonEnabler : MonoBehaviour
{
    [SerializeField] Button _myButton;
    private MainController _myController;

    // Start is called before the first frame update
    void Start()
    {
        _myController = GameObject.Find("MainController").GetComponent<MainController>();
    }

    private void Update()
    {
        if (!string.Equals(_myController.GetUserAvatar(), "default")) _myButton.interactable = true;
    }
}
