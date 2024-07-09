using UnityEngine;

public class GridData : ScriptableObject
{
    public int gridSize = 10;
    public TileSO.TileType[] cells;

    public void InitializeGrid(int size)
    {
        gridSize = size;
        cells = new TileSO.TileType[size * size];
    }
}