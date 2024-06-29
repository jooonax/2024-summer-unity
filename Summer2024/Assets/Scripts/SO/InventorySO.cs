using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "SO/Inventory")]
public class InventorySO : ScriptableObject
{
    [SerializeField]
    private int _wood;
    public int Wood { get {return _wood;} 
        set {
            _wood = value;
            ChangedInventoryEvent.Invoke();
        }
    }

    [SerializeField]
    private int _stone;
    public int Stone { get {return _stone;} 
        set {
            _stone = value;
            ChangedInventoryEvent.Invoke();
        }
    }
    
    [SerializeField]
    private int _people;
    public int People { get {return _people;} 
        set {
            _people = value;
            ChangedInventoryEvent.Invoke();
        }
    }
    
    [SerializeField]
    private int _vps;
    public int VPs { get {return _vps;} 
        set {
            _vps = value;
            ChangedInventoryEvent.Invoke();
        }
    }

    public delegate void ChangedInventory();
    public event ChangedInventory ChangedInventoryEvent;
}
