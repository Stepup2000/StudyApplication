using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class NoteUtil : MonoBehaviour
{

    private MainController _myController;

    [SerializeField] private TMP_InputField _inputFieldText;

    [SerializeField] private TMP_InputField _inputFieldCategory;


    private Note _myNoteScript;
    // Start is called before the first frame update
    void Start()
    {
        _myController = GameObject.Find("MainController").GetComponent<MainController>();
        _myNoteScript = transform.gameObject.GetComponent<Note>();
        //_myController.LoadNotes();
    }

    public void CreateNote()
    {
        Debug.Log("click");
        _myController.CreateNote();
    }

    public void SetNoteText()
    {
        Debug.Log(_inputFieldText.text);
        _myController.SetNoteText(_inputFieldText.text);
    }

    public void SetNoteCategory()
    {
        Debug.Log(_inputFieldCategory.text);
        _myController.SetNoteCategory(_inputFieldCategory.text);

    }
}
