using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CreateName : MonoBehaviour
{
    TMP_InputField _inputField;

    // Start is called before the first frame update
    void Start()
    {
        _inputField = GameObject.Find("InputField (TMP)").GetComponent<TMP_InputField>();
    }

    public void InputName()
    {
        string name = _inputField.text;
        Debug.Log(name);
        _inputField.text = "";
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
