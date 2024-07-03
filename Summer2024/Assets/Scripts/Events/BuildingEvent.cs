using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "SO/Event/Building")]
public class BuildingEvent : Event
{
    [SerializeField]
    private BuildingSO buildingSO;
    [SerializeField]
    private GameObject buildingUIPrefab;
    [field:SerializeField]
    public bool DestroyWithCard { get; set; }
    private List<GameObject> buildingUIObjects;
    private List<Card> cards;
    public override bool Execute(Card card)
    {
        cards.Add(card);
        buildingUIObjects.Add(Instantiate(buildingUIPrefab, BuildController.Instance.BuildControllerCanvas.transform));
        buildingUIObjects[buildingUIObjects.Count-1].GetComponent<BuildingEventUI>().Init(this);
        return true;
    }


    public override void Revert(Card card)
    {
        Debug.Log("Building Event has no Revert implemented! This line should never be executed :(");
        return;
    }

    public void BuildBuilding(bool addCard) {
        Tile tile = GridController.Instance.SelectedTile;
        if (tile == null) return;

        Building building = BuildController.Instance.BuildBuilding(buildingSO, tile);
        if (building != null) {
            if (addCard) {
                building.AddCard(cards[0]);
            }
            cards.RemoveAt(0);
            Destroy(buildingUIObjects[0]);
            buildingUIObjects.RemoveAt(0);
        }
    }
}