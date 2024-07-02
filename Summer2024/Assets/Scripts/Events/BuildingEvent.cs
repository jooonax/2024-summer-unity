using UnityEngine;

[CreateAssetMenu(menuName = "SO/Event/Building")]
public class BuildingEvent : Event
{
    [SerializeField]
    private GameObject buildingPrefab;
    [SerializeField]
    private GameObject buildingUI;
    private Card card;
    private GameObject buildingObject;
    public override bool Execute(Card card)
    {
        this.card = card;
        card.OnDeactivation += DestroyBuilding;
        return true;
    }


    public override void Revert(Card card)
    {
        return;
    }

    private void BuildBuilding(Tile tile) {
        buildingObject = Instantiate(buildingPrefab, tile.transform);
        tile.BuildingObject = buildingObject;
        tile.BuiltOn = true;
    }
    private void DestroyBuilding() {

    }
}