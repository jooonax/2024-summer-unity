using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    [field:SerializeField]
    public InventorySO Inventory { get; private set; }
    [SerializeField]
    private InventoryUI inventoryUI;

    void Start() {
        UpdateUI();
        Inventory.ChangedInventoryEvent += UpdateUI;
    }

    private void UpdateUI() {
        inventoryUI.WoodAmount.text = Inventory.Wood.ToString();
        inventoryUI.StoneAmount.text = Inventory.Stone.ToString();
        inventoryUI.PeopleAmount.text = Inventory.People.ToString();
        inventoryUI.VPAmount.text = Inventory.VPs.ToString();
    }

    void OnDestroy() {
        Inventory.ChangedInventoryEvent -= UpdateUI;
    }
}
