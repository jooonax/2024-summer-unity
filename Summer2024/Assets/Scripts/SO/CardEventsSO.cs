using UnityEngine;
using UnityEngine.EventSystems;

[CreateAssetMenu(menuName = "SO/CardEvents")]
[System.Serializable]
public class CardEventsSO : ScriptableObject
{
    [field:Header("Activation")]
    [field:SerializeField]
    public string ActivationName {get; private set;}
    [field:SerializeField]
    public Event ActivationCost {get; private set;}
    [field:SerializeField]
    public Event ActivationEffect {get; private set;}
   
    [field:Header("Permanent")]
    [field:SerializeField]
    public string PermanentName {get; private set;}
    [field:SerializeField]
    public Event PermanentCost {get; private set;}
    [field:SerializeField]
    public Event PermanentEffect {get; private set;}

    [field:Header("Special")]
    [field:SerializeField]
    public string SpecialName {get; private set;}
    [field:SerializeField]
    public EventTrigger SpecialEvent {get; private set;}

    
    [field:Header("Ability")]
    [field:SerializeField]
    public string AbilityName {get; private set;}
    [field:SerializeField]
    public Event AbilityCost {get; private set;}
    [field:SerializeField]
    public Event AbilityEffect {get; private set;}

    [field:Header("Destruction")]
    [field:SerializeField]
    public string DestructionName {get; private set;}
    [field:SerializeField]
    public Event DestructionEffect {get; private set;}
    

    public bool Activate(Card card) {
        SpecialEvent.Init(card);
        return ExecuteEvent(ActivationCost, ActivationEffect, card);
    }

    public bool Permanent(Card card) {
        return ExecuteEvent(PermanentCost, PermanentEffect, card);
    }

    public bool UseAbility(Card card) {
        return ExecuteEvent(AbilityCost, AbilityEffect, card);
    }

    private bool ExecuteEvent(Event cost, Event effect, Card card) {
        if (cost.Execute(card)) {
            if (!effect.Execute(card)) {
                cost.Revert(card);
            }
        } else {
            Debug.Log("Cost Failed");
            return false;
        }
        return true;
    }

    public void Destruct(Card card) {
        DestructionEffect.Execute(card);
    }
}
