using System.Runtime.Serialization;
using UnityEngine;

[CreateAssetMenu(menuName = "SO/Event/Resource")]
public class ResourceEvent : Event
{
    [SerializeField]
    private InventorySO playerInventory;
    [SerializeField]
    private bool add = true;
    [SerializeField]
    private Resources resources;


    public override bool Execute(Card card)
    {
        if (add) {
            Add();
        } else {
            Remove();
        }
        return true;
    }

    public override void Revert(Card card)
    {
        if (add) {
            Remove();
        } else {
            Add();
        }
    }

    private void Add() {
        playerInventory.Wood += resources.Wood;
        playerInventory.Stone += resources.Stone;
        playerInventory.People += resources.People;
        playerInventory.VPs += resources.VPs;
    }

    private void Remove() {
        playerInventory.Wood -= resources.Wood;
        playerInventory.Stone -= resources.Stone;
        playerInventory.People -= resources.People;
        playerInventory.VPs -= resources.VPs;
    }
}