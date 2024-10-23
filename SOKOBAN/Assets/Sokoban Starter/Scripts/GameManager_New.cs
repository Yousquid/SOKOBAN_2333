using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager_New : MonoBehaviour
{
    public GridMaker gridMaker;
    public bool clingy_check = false;
    public bool clingy_left_go = false;
    public Dictionary<Vector2Int, Box_data> boxDataDictionary = new Dictionary<Vector2Int, Box_data>();
    public Dictionary<string,GameObject> box_object = new Dictionary<string,GameObject>();


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
            if (boxData.boxtype == "sticky")
            {
                box_object.Add("sticky", box);
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

                    boxFront.isMoving = true;
                    boxData_1.isMoving = true;

                    if (Input.GetKeyDown(KeyCode.D))
                    {
                        boxFront.movementDirection = new Vector2Int(1, 0);
                    }
                    if (Input.GetKeyDown(KeyCode.A))
                    {
                        boxFront.movementDirection = new Vector2Int(-1, 0);
                    }
                    if (Input.GetKeyDown(KeyCode.S))
                    {
                        boxFront.movementDirection = new Vector2Int(0, -1);
                    }
                    if (Input.GetKeyDown(KeyCode.W))
                    {
                        boxFront.movementDirection = new Vector2Int(0, 1);
                    }

                }

                return;
            }
            //if (boxFront.boxtype == "clingy")
            //{
            //    // Check if we are moving away from the pull-only cube
            //    if (CanPullBox(boxData_1, boxFront, AddedPosition))
            //    {
            //        PullBox(boxData_1, boxFront, AddedPosition); // Perform the pulling
                   
            //    }
            //    return;
            //}

            return;
        }

        //if (boxData_1.boxtype != "clingy")
        //{
            MoveBox(CurrentPosition, boxData_1, NewPosition);
        
        boxData_1.isMoving = true;
        if (Input.GetKeyDown(KeyCode.D))
        {
            boxData_1.movementDirection = new Vector2Int(1, 0);
        }
        if (Input.GetKeyDown(KeyCode.A))
        {
            boxData_1.movementDirection = new Vector2Int(-1, 0);
        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            boxData_1.movementDirection = new Vector2Int(0, -1);
        }
        if (Input.GetKeyDown(KeyCode.W))
        {
            boxData_1.movementDirection = new Vector2Int(0, 1);
        }

        // }

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

    //public void PullCheck(GameObject box, Vector2Int PullingDirection)
    //{
    //    Box_data boxData_1 = box.GetComponent<Box_data>();
    //    Vector2Int CurrentPosition = boxData_1.gridObject.gridPosition;

    //    // Calculate the position where the pulling block will be after being pulled
    //    Vector2Int PullingPosition = CurrentPosition + PullingDirection;

    //    // Make sure the pull position is within bounds
    //    if (PullingPosition.x < 1 || PullingPosition.x >= gridMaker.dimensions.x + 1 ||
    //        PullingPosition.y < 1 || PullingPosition.y >= gridMaker.dimensions.y + 1)
    //    {
    //        return; // Invalid position, exit the method
    //    }

    //    // Check if there is a pulling block at the PullingPosition
    //    Box_data pullingBlockData = null;
    //    if (boxDataDictionary.ContainsKey(PullingPosition))
    //    {
    //        pullingBlockData = boxDataDictionary[PullingPosition];
    //        boxData_1.lastPullingDirection = PullingDirection;
    //    }

    //    // Calculate the next position for the pulled block
    //    Vector2Int PulledBlockNewPosition = CurrentPosition + PullingDirection;

    //    // Ensure that the pulled block doesn't move out of bounds
    //    if (PulledBlockNewPosition.x < 1 || PulledBlockNewPosition.x >= gridMaker.dimensions.x + 1 ||
    //        PulledBlockNewPosition.y < 1 || PulledBlockNewPosition.y >= gridMaker.dimensions.y + 1)
    //    {
    //        return; // Invalid new position, exit the method
    //    }

    //    // Check if we can move the pulled block
    //    if (pullingBlockData != null)
    //    {
    //        // Only move the pulled block if the pulling block is at the pulling position
    //        if (pullingBlockData.gridObject.gridPosition == PullingPosition)
    //        {
    //            // Ensure the next position for the pulled block is free
    //            if (!boxDataDictionary.ContainsKey(PulledBlockNewPosition))
    //            {
    //                // Move the pulled block to the new position
    //                boxData_1.gridObject.gridPosition = PulledBlockNewPosition;

    //                // Update the dictionary
    //                boxDataDictionary.Remove(CurrentPosition);
    //                boxDataDictionary[PulledBlockNewPosition] = boxData_1;

    //                // Resetting this to allow future pulls
    //                boxData_1.has_moved = false;
    //            }
    //        }
    //    }
    //    else
    //    {
    //        // If no pulling block, continue pulling in the last pulling direction
    //        if (boxData_1.lastPullingDirection != Vector2Int.zero)
    //        {
    //            Vector2Int NewPosition = CurrentPosition + boxData_1.lastPullingDirection;

    //            // Move the box only if the last pulling direction matches the current pulling direction
    //            if (PullingDirection == boxData_1.lastPullingDirection)
    //            {
    //                // Ensure the next position for the pulled block is free
    //                if (!boxDataDictionary.ContainsKey(NewPosition))
    //                {
    //                    boxData_1.gridObject.gridPosition = NewPosition;

    //                    // Update the dictionary
    //                    boxDataDictionary.Remove(CurrentPosition);
    //                    boxDataDictionary[NewPosition] = boxData_1;

    //                    // Resetting this to allow future pulls
    //                    boxData_1.has_moved = false;
    //                }
    //            }
    //        }
    //    }

    //    print("Last: " + boxData_1.lastPullingDirection);
    //    print("Current Pulling Direction: " + PullingDirection);
    //}

    //public void CanPull()
    //{
    //    if (box_object.ContainsKey("clingy"))
    //    {
    //        GameObject clingy = box_object["clingy"];

    //        // Check for pulling in the desired direction based on player input
    //        Vector2Int[] directions = new Vector2Int[]
    //        {
    //        new Vector2Int(1, 0),   
    //        new Vector2Int(-1, 0),  
    //        new Vector2Int(0, 1),   
    //        new Vector2Int(0, -1)   
    //        };

            
    //        foreach (var direction in directions)
    //        {
    //            PullCheck(clingy, direction);
    //        }
    //    }
    //}

    
    void Update()
    {
        PlayerInput();

        //CanPull();
    }


    public void SomeSticky(GameObject box, Vector2Int PullingDirection)
    {
        Box_data boxData_1 = box.GetComponent<Box_data>();
        Vector2Int CurrentPosition = boxData_1.gridObject.gridPosition;


        // Calculate the position where the pulled block will be after being pulled
        Vector2Int PullingPosition = CurrentPosition + PullingDirection;

        // Make sure the pull position is within bounds
        if (PullingPosition.x < 1 || PullingPosition.x >= gridMaker.dimensions.x + 1 ||
            PullingPosition.y < 1 || PullingPosition.y >= gridMaker.dimensions.y + 1)
        {
            return;
        }

        // Check if there is a pulling block at the PullingPosition, but even if there isn't, we still handle the pulled block
        Box_data pullingBlockData = null;
        if (boxDataDictionary.ContainsKey(PullingPosition))
        {
            pullingBlockData = boxDataDictionary[PullingPosition];
            clingy_check = true;

            // If there's a pulling block, we update the last direction
            boxData_1.lastPullingDirection = PullingDirection;
        }

        // Calculate the next position for the pulled block
        Vector2Int PulledBlockNewPosition = CurrentPosition + PullingDirection;

        // Ensure that the pulled block doesn't move out of bounds
        if (PulledBlockNewPosition.x < 1 || PulledBlockNewPosition.x >= gridMaker.dimensions.x + 1 ||
            PulledBlockNewPosition.y < 1 || PulledBlockNewPosition.y >= gridMaker.dimensions.y + 1)
        {
            return;
        }

        // Check if we can move the pulled block
        if (pullingBlockData != null)
        {
            boxData_1.has_moved = false;
            // If there's a pulling block, verify it's moving in the expected direction
            Vector2Int PullingBlockExpectedPosition = PullingPosition + PullingDirection;

            if (pullingBlockData.gridObject.gridPosition == PullingBlockExpectedPosition)
            {

                if (boxDataDictionary.ContainsKey(CurrentPosition))
                {
                    boxDataDictionary.Remove(CurrentPosition);
                }

                boxData_1.gridObject.gridPosition = PulledBlockNewPosition;

                if (!boxDataDictionary.ContainsKey(PulledBlockNewPosition))
                {
                    boxDataDictionary.Add(PulledBlockNewPosition, boxData_1);
                }
            }
        }
        else
        {
            // If no pulling block, continue pulling in the last pulling direction
            if (boxData_1.lastPullingDirection != Vector2Int.zero && !boxData_1.has_moved && boxData_1.lastPullingDirection == PullingDirection)
            {
                PullingDirection = boxData_1.lastPullingDirection;
                Vector2Int NewPosition = CurrentPosition + PullingDirection;

                if (!boxDataDictionary.ContainsKey(NewPosition) &&
                    NewPosition.x >= 1 && NewPosition.x < gridMaker.dimensions.x + 1 &&
                    NewPosition.y >= 1 && NewPosition.y < gridMaker.dimensions.y + 1)
                {
                    boxData_1.gridObject.gridPosition = NewPosition;

                    if (boxDataDictionary.ContainsKey(CurrentPosition))
                    {
                        boxDataDictionary.Remove(CurrentPosition);
                    }
                    boxDataDictionary.Add(NewPosition, boxData_1);

                    boxData_1.has_moved = true;
                }
            }
        }
    }


}
