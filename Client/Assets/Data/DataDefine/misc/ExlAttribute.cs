using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[AttributeUsage(AttributeTargets.Field | AttributeTargets.Property)]
public class ExlAttribute : Attribute {
    public string name;
    public ExlAttribute() {
    }
    public ExlAttribute(string name) {
        this.name = name;
    }
}
