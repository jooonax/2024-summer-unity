using System;
using System.Runtime.Serialization;
using UnityEngine;


public abstract class Event : ScriptableObject {
    public abstract bool Execute(Card card);
    public abstract void Revert(Card card);
}

