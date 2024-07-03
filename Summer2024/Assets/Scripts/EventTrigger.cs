using UnityEngine;

public abstract class EventTrigger: ScriptableObject {
    [field: SerializeField]
    [field:TextArea(3, 10)]
    public string EventTriggerDescription { get; private set; }
    [field: SerializeField]
    public Event Event { get; private set; }
    public Card Card { get; private set; }

    protected abstract void SubscribeToCallback(Card card);
    public void Init(Card card) {
        Card = card;
        SubscribeToCallback(card);
    }
    protected void ExecuteEvent() {
        Event.Execute(Card);
    }
}