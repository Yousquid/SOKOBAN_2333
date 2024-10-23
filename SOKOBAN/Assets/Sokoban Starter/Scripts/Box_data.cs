using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Box_data : MonoBehaviour
{
    public GridObject gridObject;
    public Vector2Int box_position;
    public string boxtype;
    public GameObject gameObject;
    public Vector2Int lastPullingDirection;
    public bool has_moved;
    public bool isMoving;
    public Vector2Int movementDirection;

    public Box_data(Vector2Int box_position, string boxtype, GameObject gameObject)
    {
        this.box_position = box_position;
        this.boxtype = boxtype;
        this.gameObject = gameObject;
    }

    void Start()
    {
        box_position = gridObject.gridPosition;
    }

    // Update is called once per frame
    void Update()
    {
        box_position = gridObject.gridPosition;
    }
}
