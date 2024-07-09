using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
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
    [SerializeField]
    private RoundController roundController;

    public List<Building> Buildings { get; private set; }
    public static BuildController Instance { get; private set; }
    public delegate void OnBuiltDelegate();
    public event OnBuiltDelegate OnBuilt;
    public delegate void OnDestroyedDelegate();
    public event OnDestroyedDelegate OnDestroyed;

    void Awake() {
        if (Instance != null && Instance != this) { 
            Destroy(this); 
            return;
        } 
        else { 
            Instance = this; 
            Buildings = new();
            roundController.OnNextRound += Permanent;
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
                OnBuilt?.Invoke();
                Buildings.Add(_building.GetComponent<Building>());
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
            Buildings.Remove(tile.BuildingObject.GetComponent<Building>());
            Destroy(tile.BuildingObject);
            tile.BuiltOn = false;
            tile.BuildingObject = null;
            OnDestroyed?.Invoke();
        }
    }

    private void Permanent() {
        Buildings.ForEach(building => {
            building.BuildingSO.PermanentEvent.Execute(building.Card);
        });
    }

    private void OnDestroy() {
        roundController.OnNextRound -= Permanent;
    }
}
