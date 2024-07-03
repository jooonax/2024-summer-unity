using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Tile : MonoBehaviour
{
    private bool _selected = false;
    public bool Selected { 
        get {return _selected;} 
        set {
            _selected = value;
            OnSelected();
        } 
    }
    private bool _marked = false;
    public bool Marked { 
        get {return _marked;} 
        set {
            _marked = value;
            OnMarked();
        } 
    }
    public bool BuiltOn {get; set;}
    public GameObject BuildingObject {get; set;}

    private LineRenderer lr;
    public Material SelectedLineMaterial { get; set; }
    public Material MarkedLineMaterial { get; set; }
    public Material LineMaterial { get; set; }

    private void OnSelected() {
        lr.material = Selected ? SelectedLineMaterial : (Marked ? MarkedLineMaterial : LineMaterial);

        // Vector3[] positions = new Vector3[lr.positionCount];
        // lr.GetPositions(positions);
        // lr.SetPositions(positions.Select(p => p + new Vector3(0f, Selected ? .2f : -.2f, 0f)).ToArray());
    }
    private void OnMarked() {
        lr.material = Selected ? SelectedLineMaterial : (Marked ? MarkedLineMaterial : LineMaterial);
    }

    public void DrawGridLine(LineRenderer lineRenderer) {
        Vector3[] positions = new Vector3[lineRenderer.positionCount];
        lineRenderer.GetPositions(positions);

        lr = gameObject.AddComponent<LineRenderer>();
        lr.positionCount = positions.Length;
        lr.SetPositions(positions.Select(p => p + transform.position).ToArray());
        lr.material = lineRenderer.material;
        LineMaterial = lineRenderer.material;
        lr.widthCurve = lineRenderer.widthCurve;
    }
}
