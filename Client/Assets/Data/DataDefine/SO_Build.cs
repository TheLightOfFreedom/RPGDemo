using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//[CreateAssetMenu(fileName ="Build", menuName ="Excel/Build", order = 1)]
public class SO_Build : ScriptableObject {
    public int ID;
    public string Name;
    public int Lev;
    public int MaxLev;
    public int BuildType;
    public string Precondition_Craft;
    public string Consume_Craft;
    public float Time_Craft;
    public string FuncitonOfBuild;
    public string Precondition_LevUp;
    public string Consume_LevUp;
    public int LevUpId;
    public string CraftFlash;
    public string Model;
    public string Icon;
    public bool canMove;
    public bool canRemove;
}
