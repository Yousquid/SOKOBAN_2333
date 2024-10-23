using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Clingy : MonoBehaviour
{
    public GameManager_New gameManager_New;
    public int lrud;
    public bool pull = false;
    public float timer = 0;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        print(pull);
        PullBlcok();
    }

    void PullBlcok()
    {
        GameObject clingy_object = gameManager_New.box_object["clingy"];
        Box_data clingy_data = clingy_object.GetComponent<Box_data>();
        Vector2Int CurrentPosition = clingy_data.gridObject.gridPosition;

        GameObject player_ob = gameManager_New.box_object["player"];
        Box_data player_data = player_ob.GetComponent<Box_data>();

        int Player_Xposition = player_data.gridObject.gridPosition.x;
        int Player_Yposition = player_data.gridObject.gridPosition.y;

        int Clingy_Xposition = clingy_data.gridObject.gridPosition.x;
        int Clingy_Yposition = clingy_data.gridObject.gridPosition.y;

        // Horizontal Pull Logic
        if (Clingy_Yposition == Player_Yposition)
        {
            if (Clingy_Xposition - Player_Xposition == 1)
            {
                pull = true;
            }

            if (pull && Clingy_Xposition - Player_Xposition == 2)
            {
                clingy_data.gridObject.gridPosition.x -= 1;

                Vector2Int NewPosition = clingy_data.gridObject.gridPosition;
                gameManager_New.boxDataDictionary.Remove(CurrentPosition);
                gameManager_New.boxDataDictionary.Add(NewPosition, clingy_data);

                pull = false;
            }
            else if (Clingy_Xposition - Player_Xposition == -1)
            {
                pull = true;
            }

            if (pull && Clingy_Xposition - Player_Xposition == -2)
            {
                clingy_data.gridObject.gridPosition.x += 1;

                Vector2Int NewPosition = clingy_data.gridObject.gridPosition;
                gameManager_New.boxDataDictionary.Remove(CurrentPosition);
                gameManager_New.boxDataDictionary.Add(NewPosition, clingy_data);

                pull = false;
            }
        }

        // Vertical Pull Logic
        else if (Clingy_Xposition == Player_Xposition)
        {
            if (Clingy_Yposition - Player_Yposition == 1)
            {
                pull = true;
            }

            if (pull && Clingy_Yposition - Player_Yposition == 2)
            {
                clingy_data.gridObject.gridPosition.y -= 1;

                Vector2Int NewPosition = clingy_data.gridObject.gridPosition;
                gameManager_New.boxDataDictionary.Remove(CurrentPosition);
                gameManager_New.boxDataDictionary.Add(NewPosition, clingy_data);

                pull = false;
            }
            else if (Clingy_Yposition - Player_Yposition == -1)
            {
                pull = true;
            }

            if (pull && Clingy_Yposition - Player_Yposition == -2)
            {
                clingy_data.gridObject.gridPosition.y += 1;

                Vector2Int NewPosition = clingy_data.gridObject.gridPosition;
                gameManager_New.boxDataDictionary.Remove(CurrentPosition);
                gameManager_New.boxDataDictionary.Add(NewPosition, clingy_data);

                pull = false;
            }
        }
        else
        {
            // If the block is neither horizontally nor vertically aligned with the player, reset pull
            pull = false;
        }
    }



}
