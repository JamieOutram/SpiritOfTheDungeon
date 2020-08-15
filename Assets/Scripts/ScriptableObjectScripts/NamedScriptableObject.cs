using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

public abstract class NamedScriptableObject : ScriptableObject
{
    public readonly string aName = "New Ability";

    public abstract void Initialize(GameObject obj);
}
