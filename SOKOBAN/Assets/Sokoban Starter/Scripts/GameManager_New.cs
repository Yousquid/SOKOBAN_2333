using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager_New : MonoBehaviour
{
    private Dictionary<Vector2Int, Box_data> boxDataDictionary = new Dictionary<Vector2Int, Box_data>();
    private Dictionary<string,GameObject> box_object = new Dictionary<string,GameObject>();


    void Start()
    {
        GameObject[] boxes = GameObject.FindGameObjectsWithTag("Box");

        foreach (GameObject box in boxes)
        {
            Box_data boxData = box.GetComponent<Box_data>();

            if (boxData.boxtype == "player")
            {
                box_object.Add("player", box);
            }
            if (boxData.boxtype == "wall")
            {
                box_object.Add("wall", box);
            }
            if (boxData.boxtype == "clingy")
            {
                box_object.Add("clingy", box);
            }
            if (boxData.boxtype == "sticky")
            {
                box_object.Add("sticky", box);
            }
            if (boxData.boxtype == "slick")
            {
                box_object.Add("slick", box);
            }

            Vector2Int gridPos = boxData.gridObject.gridPosition;

            boxDataDictionary.Add(gridPos, boxData);
        }

        
        

    }

    public void UpdateBoxPosition(GameObject box, Vector2Int AddedPosition)
    {
        Box_data boxData_1 = box.GetComponent<Box_data>();

        Vector2Int CurrentPosition = boxData_1.gridObject.gridPosition;

        if (boxDataDictionary.ContainsKey(CurrentPosition))
        {
            boxDataDictionary.Remove(CurrentPosition);
        }

        boxData_1.gridObject.gridPosition += AddedPosition;

        Vector2Int NewPosition = boxData_1.box_position = boxData_1.gridObject.gridPosition + AddedPosition;

        if (!boxDataDictionary.ContainsKey(NewPosition))
        {
            boxDataDictionary.Add(NewPosition, boxData_1);
        }

    }


    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.D))
        {
            if (box_object.ContainsKey("player"))
            {
                GameObject player_object = box_object["player"];

                UpdateBoxPosition(player_object, new Vector2Int(1, 0));
            }
        }
        if (Input.GetKeyDown(KeyCode.A))
        {
            if (box_object.ContainsKey("player"))
            {
                GameObject player_object = box_object["player"];

                UpdateBoxPosition(player_object, new Vector2Int(-1, 0));
            }
        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            if (box_object.ContainsKey("player"))
            {
                GameObject player_object = box_object["player"];

                UpdateBoxPosition(player_object, new Vector2Int(0, 1));
            }
        }
        if (Input.GetKeyDown(KeyCode.W))
        {
            if (box_object.ContainsKey("player"))
            {
                GameObject player_object = box_object["player"];

                UpdateBoxPosition(player_object, new Vector2Int(0, -1));
            }
        }


    }
}
