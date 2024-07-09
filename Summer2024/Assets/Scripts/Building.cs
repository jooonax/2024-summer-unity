using UnityEngine;

public class Building : MonoBehaviour {
    public BuildingSO BuildingSO {get; set;}
    public Card Card {get; private set;}

    public void AddCard(Card card) {
        this.Card = card;
        card.OnDeactivation += DestroyBuilding;
        card.relatedBuildings.Add(this);
    }

    private void DestroyBuilding() {
        BuildController.Instance.DestroyBuilding(BuildingSO, Tile);
    }

    private void OnDestroy() {
        if (Card != null) {
            Card.relatedBuildings.Remove(this);
            Card.OnDeactivation -= DestroyBuilding;
        }
    }

    public Tile Tile {get; set;}
}