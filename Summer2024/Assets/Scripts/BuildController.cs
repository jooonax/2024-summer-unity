using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildController : MonoBehaviour
{
    
    [field: SerializeField]
    public PlayerInventory Inventory { get; private set;}

    [field:Header("Debug / Testing")]
    [field: SerializeField]
    public BuildingSO TestBuilding { get; private set;}

    void Update() {
        if (Input.GetKeyUp(KeyCode.E)) {
        BuildBuilding(TestBuilding);
        }
        if (Input.GetKeyUp(KeyCode.Q)) {
            DestroyBuilding(TestBuilding);
        }
    }

    private void BuildBuilding(BuildingSO building) {
        if (GridController.Instance.SelectedTile && !GridController.Instance.SelectedTile.BuiltOn) {
            if (Inventory.Inventory.Wood >= building.BuildingCost.Wood &&
                Inventory.Inventory.Stone >= building.BuildingCost.Stone &&
                Inventory.Inventory.People >= building.BuildingCost.People
            ) {
                Inventory.Inventory.Wood -= building.BuildingCost.Wood;
                Inventory.Inventory.Stone -= building.BuildingCost.Stone;
                Inventory.Inventory.People -= building.BuildingCost.People;
                GameObject _building = Instantiate(building.Object, GridController.Instance.SelectedTile.transform.position, Quaternion.identity, GridController.Instance.SelectedTile.transform);
                GridController.Instance.SelectedTile.BuiltOn = true;
                GridController.Instance.SelectedTile.BuildingObject = _building;
            }
        }
    }

    private void DestroyBuilding(BuildingSO building) {
        if (GridController.Instance.SelectedTile && GridController.Instance.SelectedTile.BuiltOn) {
            Inventory.Inventory.Wood += building.DestructionReward.Wood;
            Inventory.Inventory.Stone += building.DestructionReward.Stone;
            Inventory.Inventory.People += building.DestructionReward.People;
            Destroy(GridController.Instance.SelectedTile.BuildingObject);
            GridController.Instance.SelectedTile.BuiltOn = false;
            GridController.Instance.SelectedTile.BuildingObject = null;
        }
    }
}
