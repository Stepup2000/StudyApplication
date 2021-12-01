using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class User
{
    public int id;
    public string learningType;
    public string avatar;

    public string name;

    public User(int i, string lt, string av, string n)
    {
        id = i;
        learningType = lt;
        avatar = av;
        name = n;
    }
}
