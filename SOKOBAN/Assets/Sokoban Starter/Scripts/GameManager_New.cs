using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager_New : MonoBehaviour
{
    public GridMaker gridMaker;
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

            Vector2Int gridPos = boxData.gridObject.gridPosition;

            boxDataDictionary.Add(gridPos, boxData);
        }

        
        

    }

    public void UpdateBoxPosition(GameObject box, Vector2Int AddedPosition)
    {
        Box_data boxData_1 = box.GetComponent<Box_data>();

        Vector2Int CurrentPosition = boxData_1.gridObject.gridPosition;

        Vector2Int NewPosition = CurrentPosition + AddedPosition;

        if (NewPosition.x < 1 || NewPosition.x >= gridMaker.dimensions.x +1 || NewPosition.y < 1 || NewPosition.y >= gridMaker.dimensions.y +1)
        {
            return;
        }

        if (boxDataDictionary.ContainsKey(NewPosition))
        {
            Box_data boxFront = boxDataDictionary[NewPosition];

            if (boxFront.boxtype == "slick")
            {
                if (CanPushChain(boxFront, AddedPosition))
                {
                    MovePushChain(boxFront, AddedPosition);

                    MoveBox(CurrentPosition, boxData_1, NewPosition);
                }

                return;
            }


            return;
        }
        else MoveBox(CurrentPosition, boxData_1, NewPosition);

    }

    public bool CanPushChain(Box_data boxFront, Vector2Int AddedPosition)
    {
        Vector2Int NewPosition = boxFront.gridObject.gridPosition + AddedPosition;

        if (NewPosition.x < 1 || NewPosition.x >= gridMaker.dimensions.x + 1 || NewPosition.y < 1 || NewPosition.y >= gridMaker.dimensions.y + 1)
        {
            return false; 
        }

        if (boxDataDictionary.ContainsKey(NewPosition))
        {
            Box_data nextBox = boxDataDictionary[NewPosition];

            if (nextBox.boxtype == "slick")
            {
                return CanPushChain(nextBox, AddedPosition); 
            }
            else
            {
                return false; 
            }
        }

        return true;
    }

    public void MoveBox(Vector2Int CurrentPosition, Box_data boxData_1, Vector2Int NewPosition)
    {
        if (boxDataDictionary.ContainsKey(CurrentPosition))
        {
            boxDataDictionary.Remove(CurrentPosition);
        }

        boxData_1.gridObject.gridPosition = NewPosition;

        if (!boxDataDictionary.ContainsKey(NewPosition))
        {
            boxDataDictionary.Add(NewPosition, boxData_1);
        }
    }

    public void MovePushChain(Box_data box, Vector2Int AddedPosition)
    {
        Vector2Int CurrentPosition = box.gridObject.gridPosition;

        Vector2Int NewPosition = CurrentPosition + AddedPosition;

        if (boxDataDictionary.ContainsKey(NewPosition))
        {
            Box_data nextBox = boxDataDictionary[NewPosition];

            MovePushChain(nextBox, AddedPosition); 
        }

        MoveBox(CurrentPosition,box, NewPosition);
    }



    public void PushBoxCheck(Box_data boxFront, Box_data boxData_1, Vector2Int FrontPosition, Vector2Int AddedPosition, Vector2Int CurrentPosition, Vector2Int NewPosition)
    {
        if (boxFront.boxtype == "slick")
        {
           
            Vector2Int PushToPosition = FrontPosition + AddedPosition;

            if (PushToPosition.x < 1 || PushToPosition.x >= gridMaker.dimensions.x + 1 || PushToPosition.y < 1 || PushToPosition.y >= gridMaker.dimensions.y + 1)
            {
                return;
            }

            if (boxDataDictionary.ContainsKey(CurrentPosition) && boxDataDictionary.ContainsKey(NewPosition))
            {
                boxDataDictionary.Remove(CurrentPosition);
                boxDataDictionary.Remove(NewPosition);
            }

            boxData_1.gridObject.gridPosition += AddedPosition;
            boxFront.gridObject.gridPosition += AddedPosition;

            if (!boxDataDictionary.ContainsKey(NewPosition))
            {
                boxDataDictionary.Add(FrontPosition, boxData_1);
                boxDataDictionary.Add(PushToPosition, boxFront);
            }

            return;

        }
    }



    public void MovePlayer()
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



    // Update is called once per frame
    void Update()
    {
        MovePlayer();


    }
}
