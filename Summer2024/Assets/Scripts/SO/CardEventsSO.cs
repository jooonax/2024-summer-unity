using UnityEngine;

[CreateAssetMenu(menuName = "SO/CardEvents")]
[System.Serializable]
public class CardEventsSO : ScriptableObject
{
    [field:Header("Activation")]
    [field:SerializeField]
    public Event ActivationCost {get; private set;}
    [field:SerializeField]
    public Event ActivationEffect {get; private set;}
   
    [field:Header("Permanent")]
    [field:SerializeField]
    public Event PermanentCost {get; private set;}
    [field:SerializeField]
    public Event PermanentEffect {get; private set;}

    [field:Header("Special")]
    [field:SerializeField]
    public Event SpecialEvent {get; private set;}

    
    [field:Header("Ability")]
    [field:SerializeField]
    public Event AbilityCost {get; private set;}
    [field:SerializeField]
    public Event AbilityEffect {get; private set;}

    [field:Header("Destruction")]
    [field:SerializeField]
    public Event DestructionEffect {get; private set;}
    

    public void Activate() {
        if (ActivationCost.Execute()) {
            ActivationEffect.Execute();
        } else Debug.Log("Cost Failed");
    }

    public void Permanent() {
        if (PermanentCost.Execute()) {
            PermanentEffect.Execute();
        } else Debug.Log("Cost Failed");
    }

    public void Ability() {
        if (AbilityCost.Execute()) {
            AbilityEffect.Execute();
        } else Debug.Log("Cost Failed");
    }

    public void Special() {
        SpecialEvent.Execute();
    }

    public void Destruct() {
        DestructionEffect.Execute();
    }
}
