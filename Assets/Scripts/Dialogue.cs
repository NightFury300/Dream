using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Dialogue
{
    public string Name;
    [TextArea(3,10)]
    public string[] Sentences;
    public string[] Option = new string[3];
    public bool EnableOptions = false;
    public int CorrectOption;
}
