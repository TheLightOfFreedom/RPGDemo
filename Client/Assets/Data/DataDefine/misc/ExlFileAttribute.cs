using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[AttributeUsage(AttributeTargets.Class)]
public class ExlFileAttribute : Attribute {
    public string file;
    public ExlFileAttribute(string file) {
        this.file = file;
    }
}
