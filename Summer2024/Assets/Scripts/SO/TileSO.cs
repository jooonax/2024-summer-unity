using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "SO/Tile")]
public class TileSO : ScriptableObject
{
    [field: SerializeField]
    public GameObject TileObject { get; private set; }
    [field: SerializeField]
    public TileType Type { get; private set; }

    public enum TileType {
        GRASS, WATER, FOREST, MOUNTAIN, DESSERT
    }
}
