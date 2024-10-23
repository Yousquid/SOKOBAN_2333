using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager_New : MonoBehaviour
{
    public GridMaker gridMaker;
    public bool clingy_can_move_left = false;
    public bool clingy_left_go = false;
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

            if (boxData.boxtype == "clingy")
            {
                box_object.Add("clingy", box);
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

        if (boxData_1.boxtype != "clingy")
        {
            MoveBox(CurrentPosition, boxData_1, NewPosition);
            if (Input.GetKeyDown(KeyCode.A))
            {
                clingy_left_go = true;
            }

        }

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




    public void PlayerInput()
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
        PlayerInput();

       

    }
}
