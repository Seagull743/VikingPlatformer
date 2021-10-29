using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "LevelNamesData", menuName = "ScriptableObjects/LevelNamesData", order = 1)]
public class LevelNamesData : ScriptableObject
{
    public string[] levelNames = new string[0];
}
