using UnityEngine;
using UnityEditor;

public class GridPainter : EditorWindow
{
    private GridData gridData;
    private TileSO.TileType selectedColor = TileSO.TileType.WATER;
    private int gridSize = 10;
    private bool isPainting = false;

    [MenuItem("Window/Grid Painter")]
    public static void ShowWindow()
    {
        GetWindow<GridPainter>("Grid Painter");
    }

    private void OnEnable()
    {
        gridData = AssetDatabase.LoadAssetAtPath<GridData>("Assets/SOs/GridData.asset");
        if (gridData == null)
        {
            gridData = CreateInstance<GridData>();
            gridData.InitializeGrid(gridSize);
            AssetDatabase.CreateAsset(gridData, "Assets/SOs/GridData.asset");
            AssetDatabase.SaveAssets();
        }
    }

    private void OnGUI()
    {
        EditorGUILayout.LabelField("Grid Settings", EditorStyles.boldLabel);
        gridSize = EditorGUILayout.IntField("Grid Size", gridSize);
        
        if (GUILayout.Button("Initialize Grid"))
        {
            gridData.InitializeGrid(gridSize);
        }

        EditorGUILayout.Space();
        EditorGUILayout.LabelField("Paint Settings", EditorStyles.boldLabel);
        selectedColor = (TileSO.TileType)EditorGUILayout.EnumPopup("Selected Color", selectedColor);

        EditorGUILayout.Space();
        DrawGrid();

        HandleInput(UnityEngine.Event.current);

        if (GUI.changed)
        {
            Repaint();
        }
    }

    private void DrawGrid()
    {
        if (gridData == null || gridData.cells == null)
            return;

        for (int y = 0; y < gridData.gridSize; y++)
        {
            EditorGUILayout.BeginHorizontal();
            for (int x = 0; x < gridData.gridSize; x++)
            {
                var cellIndex = y * gridData.gridSize + x;
                var cellColor = GetColor(gridData.cells[cellIndex]);

                var style = new GUIStyle(GUI.skin.button);
                style.normal.background = MakeTex(1, 1, cellColor);
                if (GUILayout.Button("", style, GUILayout.Width(20), GUILayout.Height(20)))
                {
                    gridData.cells[cellIndex] = selectedColor;
                }
            }
            EditorGUILayout.EndHorizontal();
        }
    }

    private void HandleInput(UnityEngine.Event e)
    {
        if (e.type == UnityEngine.EventType.MouseDown && e.button == 0)
        {
            isPainting = true;
            PaintAtMousePosition(e.mousePosition);
        }

        if (e.type == UnityEngine.EventType.MouseUp && e.button == 0)
        {
            isPainting = false;
        }

        if (isPainting && e.type == UnityEngine.EventType.MouseDrag)
        {
            PaintAtMousePosition(e.mousePosition);
        }
    }

    private void PaintAtMousePosition(Vector2 mousePosition)
    {
        for (int y = 0; y < gridData.gridSize; y++)
        {
            for (int x = 0; x < gridData.gridSize; x++)
            {
                Rect cellRect = new Rect(x * 20, y * 20 + 100, 20, 20); // Adjust the y offset based on your layout
                if (cellRect.Contains(mousePosition))
                {
                    var cellIndex = y * gridData.gridSize + x;
                    gridData.cells[cellIndex] = selectedColor;
                }
            }
        }
    }

    private Color GetColor(TileSO.TileType cellColor)
    {
        switch (cellColor)
        {
            case TileSO.TileType.GRASS:
                return Color.green;
            case TileSO.TileType.WATER:
                return Color.cyan;
            case TileSO.TileType.FOREST:
                return Color.black;
            case TileSO.TileType.MOUNTAIN:
                return Color.gray;
            case TileSO.TileType.DESSERT:
                return Color.yellow;
            default:
                return Color.white;
        }
    }

    private Texture2D MakeTex(int width, int height, Color col)
    {
        Color[] pix = new Color[width * height];
        for (int i = 0; i < pix.Length; i++)
        {
            pix[i] = col;
        }
        Texture2D result = new Texture2D(width, height);
        result.SetPixels(pix);
        result.Apply();
        return result;
    }
}