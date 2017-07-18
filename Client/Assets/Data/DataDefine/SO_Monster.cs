using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExlFile("Monster.xlsx")]
public class SO_Monster : ScriptableObject {
    [Exl] public int Id;
    [Exl] public string Name;
    [Exl] public string titleName;
    [Exl] public string RoleType;
    [Exl] public int Race;
    [Exl] public string Procession;
    [Exl] public string Camp;
    [Exl] public int RoleQuality;
    [Exl] public int activeAttack;
    [Exl] public int level;
    [Exl] public int atkSpeed;
    [Exl] public int moveSpeed;
    [Exl] public int moveSpeedCd;
    [Exl] public int maxHp;
    [Exl] public string MpType;
    [Exl] public int maxMp;
    [Exl] public int Ac;
    [Exl] public int Ac2;
    [Exl] public int mac;
    [Exl] public int mac2;
    
}
