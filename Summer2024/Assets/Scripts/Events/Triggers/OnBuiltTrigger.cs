using UnityEngine;

[CreateAssetMenu(menuName = "SO/Triggers/OnBuilt")]
public class OnBuiltTrigger : EventTrigger
{
    protected override void SubscribeToCallback(Card card)
    {
        BuildController.Instance.OnBuilt += ExecuteEvent;
    }
}