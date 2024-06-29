using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "SO/Building")]
public class BuildingSO : ScriptableObject
{
    [field:Header("Overview")]
    [field:SerializeField]
    public string BuildingName { get; private set; }
    [field:SerializeField]
    [field:TextArea(3,10)]
    public string BuildingDescription { get; private set; }

    [field:Header("Costs & Effects")]
    [field:SerializeField]
    public Cost BuildingCost { get; private set; }
    [field:SerializeField]
    public Cost DestructionReward { get; private set; }


    [field:Header("Objects")]
    [field:SerializeField]
    public GameObject Object { get; private set; }
}
