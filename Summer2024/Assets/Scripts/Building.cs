using UnityEngine;

public class Building : MonoBehaviour {
    public BuildingSO BuildingSO {get; set;}
    private Card card;

    public void AddCard(Card card) {
        this.card = card;
        card.OnDeactivation += DestroyBuilding;
        card.relatedBuildings.Add(this);
    }

    private void DestroyBuilding() {
        BuildController.Instance.DestroyBuilding(BuildingSO, Tile);
    }

    private void OnDestroy() {
        if (card != null) {
            card.relatedBuildings.Remove(this);
            card.OnDeactivation -= DestroyBuilding;
        }
    }

    public Tile Tile {get; set;}
}