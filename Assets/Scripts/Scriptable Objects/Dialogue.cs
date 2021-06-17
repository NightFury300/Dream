using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Dialogue", menuName = "Dialogue", order = 52)]
public class Dialogue : ScriptableObject
{
    public string heading;
    [TextArea(3,10)]
    public string[] sentences;
    public string[] options = new string[3];
    public bool enableOptions = false;
    public int correctOption;
}
