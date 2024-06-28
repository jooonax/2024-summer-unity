using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "SO/Tile")]
public class Tile : ScriptableObject
{
    [field: SerializeField]
    public GameObject TileObject { get; private set; }


}
