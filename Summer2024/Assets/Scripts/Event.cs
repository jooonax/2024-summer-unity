using System;
using System.Runtime.Serialization;
using UnityEngine;


public abstract class Event : ScriptableObject {
    public abstract bool Execute();
}

