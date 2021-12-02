using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Note : MonoBehaviour
{
    public int note_id;
    public string text;
    public string category;
    public int task_id;

    [SerializeField] public TMP_InputField textField;
    [SerializeField] public TMP_InputField categoryField;
}
