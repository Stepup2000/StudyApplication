using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataNote
{

    public int note_id;
    public string text;
    public string category;
    public int task_id;


    public DataNote(int nid, string t, string c, int tid)
    {
        note_id = nid;
        text = t;
        category = c;
        task_id = tid;
    }
}
