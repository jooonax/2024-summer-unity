using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GridController : MonoBehaviour
{
    [SerializeField]
    private int width = 10;

    [SerializeField]
    private int tileWidth = 15;
    [SerializeField]
    private int tileLength = 15;

    [SerializeField]
    private LineRenderer lineRenderer;

    [SerializeField]
    private List<Tile> tiles = new();
    [SerializeField]
    private List<Tile> world = new();



    void Awake() {
        if (world.Count == 0) {
            for (int i = 0; i < width * width; i++) {
                world.Add(tiles[0]);
            }
        }
        BuildWorld();
    }

    private Vector3 startPosition = Vector3.zero;
    private void BuildWorld() {
        int i = 1;
        foreach (Tile tile in world) {
            GameObject tileObejct = Instantiate(tile.TileObject, startPosition, Quaternion.identity, transform);
            DrawGridLine(tileObejct);
            
            if (i % width != 0) {
                startPosition += new Vector3(tileWidth, 0f, 0f);
            } else {
                startPosition.x = 0f;
                startPosition += new Vector3(0f, 0f, tileLength);
            }
            i++;
        }
    }

    private void DrawGridLine(GameObject tile) {

        Vector3[] positions = new Vector3[lineRenderer.positionCount];
        lineRenderer.GetPositions(positions);

        LineRenderer lr = tile.AddComponent<LineRenderer>();
        lr.positionCount = positions.Length;
        lr.SetPositions(positions.Select(p => p + startPosition).ToArray());
        lr.material = lineRenderer.material;
        lr.widthCurve = lineRenderer.widthCurve;
    }
} 
