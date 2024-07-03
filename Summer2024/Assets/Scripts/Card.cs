using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Card : MonoBehaviour
{
    private bool _active;
    public bool Active {
        get {
            return _active;
        } 
        set {
            if (value) {
                OnActivation?.Invoke();
            } else {
                OnDeactivation?.Invoke();
            }
            _active = value;
        }
    }

    public delegate void OnDeactivationDelegate();
    public OnDeactivationDelegate OnDeactivation {get; set;}
    public delegate void OnActivationDelegate();
    public OnActivationDelegate OnActivation {get; set;}
    
    public bool UsedAbility {get; set;}
    public CardSO CardSO {get; set;}
    public List<Building> relatedBuildings = new();

}