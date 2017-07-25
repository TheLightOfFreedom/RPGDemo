using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//[CreateAssetMenu(fileName ="Build", menuName ="Excel/Build", order = 1)]
[ExlFile("Build.xlsx")]
public class SO_Build : ScriptableObject {
    [Exl] public int ID;
    [Exl] public string Name;
    [Exl] public int Lev;
    [Exl] public int MaxLev;
    [Exl] public int BuildType;
    [Exl] public string Precondition_Craft;
    [Exl] public string Consume_Craft;
    [Exl] public float Time_Craft;
    [Exl] public string FuncitonOfBuild;
    [Exl] public string Precondition_LevUp;
    [Exl] public string Consume_LevUp;
    [Exl] public int LevUpId;
    [Exl] public string CraftFlash;
    [Exl] public string Model;
    [Exl] public string Icon;
    [Exl] public bool canMove;
    [Exl] public bool canRemove;

    public SO_Build item;
}
