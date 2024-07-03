using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{

    [field:Header("Movement")]
    [field: SerializeField]
    public float MoveSpeed { get; private set;}
    [field: SerializeField]
    public float ToSelectedSpeed { get; private set;}

    [field:Header("Zoom")]
    [field: SerializeField]
    public float MinZoom { get; private set;}
    [field: SerializeField]
    public float MaxZoom { get; private set;}
    [field: SerializeField]
    public float ZoomSpeed { get; private set;}

    [field:Header("Camera")]
    [field: SerializeField]
    public GameObject CameraObject { get; private set;}
    [field: SerializeField]
    public float CameraDownAngle { get; private set;}


    public Vector3 TargetPosition {get; set;}

    void Awake() {
        TargetPosition = Vector3.zero;
        CameraObject.transform.rotation = Quaternion.Euler(CameraDownAngle, 0, 0);
    }


    void Update() {
        transform.position = new Vector3(transform.position.x, GetZoom(), transform.position.z);
        if (GetZoom() != 0) {
            TargetPosition = CalculateTargetPosition(GridController.Instance.SelectedTile);
        }

        if (Input.GetAxis("Fire1") == 1) {
            float _mouseX = Input.GetAxis("Mouse X") * Time.deltaTime * MoveSpeed * -1;
            float _mouseY = Input.GetAxis("Mouse Y") * Time.deltaTime * MoveSpeed * -1;

            transform.Translate(_mouseX, 0f, _mouseY);
        }
        if (TargetPosition != Vector3.zero) {
            Vector3 change = Vector3.Lerp(transform.position, TargetPosition, Time.deltaTime * ToSelectedSpeed);
            change.y = transform.position.y;
            transform.position = change;
        }
    }
    public Vector3 CalculateTargetPosition(Tile tile) {
        float _positionZ = tile.transform.position.z - (transform.position.y / (float) Math.Tan(CameraDownAngle * (Math.PI/180)));
        return new Vector3(tile.transform.position.x, transform.position.y, _positionZ);
    }
    private float GetZoom() {
        return  Math.Min(Math.Max(transform.position.y + Input.GetAxis("Mouse ScrollWheel") * Time.deltaTime * ZoomSpeed * -1, MinZoom), MaxZoom);
    }
}
