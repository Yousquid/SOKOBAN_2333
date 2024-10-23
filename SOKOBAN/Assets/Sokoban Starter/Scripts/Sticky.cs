using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sticky : MonoBehaviour
{

    public GameManager_New gameManager_New;
    public int lrud;
    public bool pull = false;
    public GridMaker gridMaker;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        StickToAdjacentBlock();
    }

    public void StickyMove()
    {
        GameObject sticky = gameManager_New.box_object["sticky"];
        Box_data sticky_data = sticky.GetComponent<Box_data>();
        Vector2Int CurrentPosition = sticky_data.gridObject.gridPosition;

        Vector2Int Left = CurrentPosition + new Vector2Int(-1, 0);
        Vector2Int Right = CurrentPosition + new Vector2Int(1, 0);
        Vector2Int Up = CurrentPosition + new Vector2Int(0, 1);
        Vector2Int Down = CurrentPosition + new Vector2Int(0, -1);

        if (gameManager_New.boxDataDictionary.ContainsKey(Left))
        {
            Box_data sticked_block = gameManager_New.boxDataDictionary[Left];

            Vector2Int StickPosition = Left + new Vector2Int(1, 0);

            if (CurrentPosition != StickPosition)
            {
                sticked_block.gridObject.gridPosition = StickPosition;
            }


        }
        if (gameManager_New.boxDataDictionary.ContainsKey(Right))
        {
            Box_data sticked_block = gameManager_New.boxDataDictionary[Right];

            Vector2Int StickPosition = sticky_data.gridObject.gridPosition;
            sticky_data.gridObject.gridPosition = Right + new Vector2Int(-1, 0);


        }
        if (gameManager_New.boxDataDictionary.ContainsKey(Up))
        {
            Box_data sticked_block = gameManager_New.boxDataDictionary[Up];

            Vector2Int StickPosition = sticky_data.gridObject.gridPosition;
            sticky_data.gridObject.gridPosition = Up + new Vector2Int(0, -1);


        }
        if (gameManager_New.boxDataDictionary.ContainsKey(Down))
        {
            Box_data sticked_block = gameManager_New.boxDataDictionary[Down];

            Vector2Int StickPosition = sticky_data.gridObject.gridPosition;
            sticky_data.gridObject.gridPosition = Down + new Vector2Int(0, 1);


        }

    }

    public void StickToAdjacentBlock()
    {
        GameObject sticky = gameManager_New.box_object["sticky"];
        Box_data sticky_data = sticky.GetComponent<Box_data>();
        Vector2Int currentPosition = sticky_data.gridObject.gridPosition;

        // Define potential adjacent positions
        Vector2Int[] adjacentPositions = {
        currentPosition + new Vector2Int(-1, 0), // Left
        currentPosition + new Vector2Int(1, 0),  // Right
        currentPosition + new Vector2Int(0, 1),  // Up
        currentPosition + new Vector2Int(0, -1)   // Down
    };

        // Track if the sticky block has been moved
        bool moved = false;

        // Iterate over adjacent positions
        foreach (Vector2Int adjacentPosition in adjacentPositions)
        {
            // Check if there is a block at the adjacent position
            if (gameManager_New.boxDataDictionary.TryGetValue(adjacentPosition, out Box_data adjacentBlock))
            {
                Vector2Int newPosition = Vector2Int.zero;

                // Determine which side to stick to based on the adjacent block's position
                if (adjacentPosition.x < currentPosition.x) // Adjacent is on the left
                {
                    newPosition = adjacentBlock.gridObject.gridPosition + new Vector2Int(-1, 0);
                }
                else if (adjacentPosition.x > currentPosition.x) // Adjacent is on the right
                {
                    newPosition = adjacentBlock.gridObject.gridPosition + new Vector2Int(1, 0);
                }
                else if (adjacentPosition.y > currentPosition.y) // Adjacent is above
                {
                    newPosition = adjacentBlock.gridObject.gridPosition + new Vector2Int(0, 1);
                }
                else if (adjacentPosition.y < currentPosition.y) // Adjacent is below
                {
                    newPosition = adjacentBlock.gridObject.gridPosition + new Vector2Int(0, -1);
                }

                // Ensure the new position is within bounds and not occupied
                if (!gameManager_New.boxDataDictionary.ContainsKey(newPosition) &&
                    newPosition.x >= 1 && newPosition.x < gridMaker.dimensions.x + 1 &&
                    newPosition.y >= 1 && newPosition.y < gridMaker.dimensions.y + 1)
                {
                    // Update the sticky block's position
                    sticky_data.gridObject.gridPosition = newPosition;

                    // Update the dictionary
                    gameManager_New.boxDataDictionary.Remove(currentPosition);
                    gameManager_New.boxDataDictionary[newPosition] = sticky_data;

                    // Mark that the sticky block has moved
                    moved = true;
                    break; // Exit after moving to prevent multiple movements
                }
            }
        }

    }


    }
