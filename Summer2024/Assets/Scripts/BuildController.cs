using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildController : MonoBehaviour
{
    
    [field: SerializeField]
    public PlayerInventory Inventory { get; private set;}

    [field:Header("Debug / Testing")]
    [field: SerializeField]
    public Canvas BuildControllerCanvas { get; private set;}
    [field: SerializeField]
    public BuildingSO TestBuilding { get; private set;}
    public static BuildController Instance { get; private set; }
    void Awake() {
        if (Instance != null && Instance != this) { 
            Destroy(this); 
            return;
        } 
        else { 
            Instance = this; 
        }
    }
    
    public Building BuildBuilding(BuildingSO building) { 
        return BuildBuilding(building, GridController.Instance.SelectedTile);
    }
    public Building BuildBuilding(BuildingSO building, Tile tile) {
        if (tile && !tile.BuiltOn) {
            if (Inventory.Inventory.Wood >= building.BuildingCost.Wood &&
                Inventory.Inventory.Stone >= building.BuildingCost.Stone &&
                Inventory.Inventory.People >= building.BuildingCost.People
            ) {
                Inventory.Inventory.Wood -= building.BuildingCost.Wood;
                Inventory.Inventory.Stone -= building.BuildingCost.Stone;
                Inventory.Inventory.People -= building.BuildingCost.People;
                GameObject _building = Instantiate(building.Object, tile.transform.position, Quaternion.identity, tile.transform);
                _building.GetComponent<Building>().BuildingSO = building;
                _building.GetComponent<Building>().Tile = tile;
                tile.BuiltOn = true;
                tile.BuildingObject = _building;
                return _building.GetComponent<Building>();
            }
        }
        return null;
    }
    public void DestroyBuilding(BuildingSO building) { 
        DestroyBuilding(building, GridController.Instance.SelectedTile);
    }
    public void DestroyBuilding(BuildingSO building, Tile tile) {
        if (tile && tile.BuiltOn) {
            Inventory.Inventory.Wood += building.DestructionReward.Wood;
            Inventory.Inventory.Stone += building.DestructionReward.Stone;
            Inventory.Inventory.People += building.DestructionReward.People;
            Destroy(tile.BuildingObject);
            tile.BuiltOn = false;
            tile.BuildingObject = null;
        }
    }
}
