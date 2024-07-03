using System;
using System.Runtime.Serialization;
using UnityEngine;


public abstract class Event : ScriptableObject {
    [field:SerializeField]
    [field:TextArea(3, 10)]
    public string Description { get; private set; }
    public abstract bool Execute(Card card);
    public abstract void Revert(Card card);
}

