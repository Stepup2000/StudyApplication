using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class CreateName : MonoBehaviour
{
    TMP_InputField _inputField;
    private MainController _myController;
    [SerializeField] Button _myButton;

    // Start is called before the first frame update
    void Start()
    {
        _inputField = GameObject.Find("InputField (TMP)").GetComponent<TMP_InputField>();
        _myController = GameObject.Find("MainController").GetComponent<MainController>();
    }

    public void InputName()
    {
        if (!string.Equals("", _inputField.text)) _myController.SetUserName(_inputField.text);
    }

    public void ClickEnabler()
    {
        _myButton.interactable = true;
    }

    // Update is called once per frame
    void Update()
    {

    }
}
