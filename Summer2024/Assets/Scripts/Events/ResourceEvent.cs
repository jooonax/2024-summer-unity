using System.Runtime.Serialization;
using UnityEngine;

[CreateAssetMenu(menuName = "SO/Event/Resource")]
public class ResourceEvent : Event
{
    [SerializeField]
    private InventorySO playerInventory;
    [SerializeField]
    private Resources reward;


    public override bool Execute()
    {
        playerInventory.Wood += reward.Wood;
        playerInventory.Stone += reward.Stone;
        playerInventory.People += reward.People;
        playerInventory.VPs += reward.VPs;
        return true;
    }
}