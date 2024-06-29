using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.WSA;

[CreateAssetMenu(menuName = "SO/Card")]
public class CardSO : ScriptableObject
{
    [field:Header("Activation")]
    [field:SerializeField]
    public Cost ActivationCost {get; private set;}
    [field:SerializeField]
    public UnityEvent ActivationReward {get; private set;}
    
}
