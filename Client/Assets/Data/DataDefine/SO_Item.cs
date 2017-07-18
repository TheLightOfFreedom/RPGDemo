using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExlFile("Item.xlsx")]
public class SO_Item : ScriptableObject {
    [Exl] public int id;
    [Exl] public string name;
    [Exl] public int stdMode;
    [Exl] public int weigh;
    [Exl] public int duraMax;
    [Exl] public int m_MaxCount;
    [Exl] public bool m_bConsume;
    [Exl] public int ac;
    [Exl] public int ac2;
    [Exl] public int mac;
    [Exl] public int mac2;
    [Exl] public int dc;
    [Exl] public int dc2;
    [Exl] public int mc;
    [Exl] public int mc2;
    [Exl] public int sc;
    [Exl] public int sc2;
    [Exl] public int need;
    [Exl] public int needLevel;
    [Exl] public int price;
    [Exl] public string icon;
    [Exl] public int defaultColor;
    [Exl] public string descr;
}
