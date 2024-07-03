using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GridController : MonoBehaviour
{
    [Header("World Options")]
    [SerializeField]
    private int width = 10;
    [SerializeField]
    private List<TileSO> tiles = new();

    [Header("Tile Settings")]
    [SerializeField]
    private int tileWidth = 15;
    [SerializeField]
    private int tileLength = 15;
    [SerializeField]
    private LineRenderer lineRenderer;
    [SerializeField]
    private Material selectedMaterial;
    [SerializeField]
    private Material markedMaterial;

    [Header("Tile Selection Movement")]
    [SerializeField]
    private CameraMovement cameraMovement;

    public Tile SelectedTile { get; set; }

    private List<TileSO> worldSOs = new();
    private List<Tile> world = new();

    private Vector3 mouseDownPosition;

    public static GridController Instance { get; private set; }
    void Awake() {
        if (Instance != null && Instance != this) { 
            Destroy(this); 
            return;
        } 
        else { 
            Instance = this; 
        } 


        if (worldSOs.Count == 0) {
            for (int i = 0; i < width * width; i++) {
                worldSOs.Add(tiles[0]);
            }
        }
        BuildWorld();
        SelectTile(world[0]);
    }

    void Update() {
        if (Input.GetKeyUp(KeyCode.Escape)) {
            SelectedTile = null;
        }
        if (Input.GetKeyUp(KeyCode.UpArrow) || Input.GetKeyUp(KeyCode.W)) {
            SelectTile(GetNeigbourTile(SelectedTile, Vector2.up));
        }
        if (Input.GetKeyUp(KeyCode.DownArrow) || Input.GetKeyUp(KeyCode.S)) {
            SelectTile(GetNeigbourTile(SelectedTile, Vector2.down));
        }
        if (Input.GetKeyUp(KeyCode.LeftArrow) || Input.GetKeyUp(KeyCode.A)) {
            SelectTile(GetNeigbourTile(SelectedTile, Vector2.left));
        }
        if (Input.GetKeyUp(KeyCode.RightArrow) || Input.GetKeyUp(KeyCode.D)) {
            SelectTile(GetNeigbourTile(SelectedTile, Vector2.right));
        }


        // Moviing with mouse disabled because clicking through UI
        // if (Input.GetMouseButtonDown(0)) {
        //     mouseDownPosition = cameraMovement.transform.position;
        //     cameraMovement.TargetPosition = Vector3.zero;
        // }
        // if (Input.GetMouseButtonUp(0) && (mouseDownPosition - cameraMovement.transform.position).magnitude < 1f) {
		// 	RaycastHit hit;
		// 	var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
			
		// 	if (Physics.Raycast(ray, out hit, 1000, LayerMask.GetMask("Tile"))) {
		// 		if (hit.transform.tag == "Tile") {
        //             Tile tile = hit.transform.gameObject.GetComponent<Tile>();
        //             SelectTile(tile);
        //         }
		// 	}
		// }
    }

    
    private void SelectTile(Tile tile) {
        if (!tile) return;
        if (tile != SelectedTile) {
            tile.Selected = true;
            cameraMovement.TargetPosition = cameraMovement.CalculateTargetPosition(tile);
        }
        if (SelectedTile) {
            SelectedTile.Selected = false;
        }
        
        if (tile != SelectedTile) {
            SelectedTile = tile;
        } else SelectedTile = null;
    }

    

    public Tile GetNeigbourTile(Tile tile, Vector2 direction) {
        if (!tile) return null;
        int _oldIndex = world.IndexOf(tile);
        int _newIndex = _oldIndex;
        if (direction == Vector2.down && _oldIndex > width) {
            _newIndex -= width;
        }
        if (direction == Vector2.up && _oldIndex < width*(width-1)) {
            _newIndex += width;
        }
        if (direction == Vector2.left && _oldIndex % width != 0) {
            _newIndex -= 1;
        }
        if (direction == Vector2.right && _oldIndex % width != width-1) {
            _newIndex += 1;
        }
        if (_oldIndex == _newIndex) return null;
        return world[_newIndex];
    }

    


    private Vector3 startPosition = Vector3.zero;
    private void BuildWorld() {
        int i = 1;
        foreach (TileSO tile in worldSOs) {
            GameObject tileObejct = Instantiate(tile.TileObject, startPosition, Quaternion.identity, transform);

            Tile component = tileObejct.AddComponent<Tile>();
            component.SelectedLineMaterial = selectedMaterial;
            component.MarkedLineMaterial = markedMaterial;
            component.DrawGridLine(lineRenderer);
            world.Add(component);

            if (i % width != 0) {
                startPosition += new Vector3(tileWidth, 0f, 0f);
            } else {
                startPosition.x = 0f;
                startPosition += new Vector3(0f, 0f, tileLength);
            }
            i++;
        }
    }
} 
