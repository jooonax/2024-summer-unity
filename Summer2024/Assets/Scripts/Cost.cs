using System;
using UnityEngine;

[Serializable]
public struct Cost {
    [field:SerializeField]
    public int Wood { get; private set; }
    [field:SerializeField]
    public int Stone { get; private set; }
    [field:SerializeField]
    public int People { get; private set; }
}