using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Box_data : MonoBehaviour
{
    public GridObject gridObject;
    public Vector2Int box_position;
    public string boxtype;

    public Box_data(Vector2Int box_position, string boxtype)
    {
        this.box_position = box_position;
        this.boxtype = boxtype;
    }

    void Start()
    {
        box_position = gridObject.gridPosition;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
