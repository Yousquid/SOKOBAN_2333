using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_movement : MonoBehaviour
{
    public GridObject gridOB;
    

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.D))
        {
            gridOB.gridPosition += new Vector2Int(1, 0);
        }

        if (Input.GetKeyDown(KeyCode.A))
        {
            gridOB.gridPosition += new Vector2Int(-1, 0);

            print("afafas");
        }

        if (Input.GetKeyDown(KeyCode.W))
        {
            gridOB.gridPosition += new Vector2Int(0, -1);

            print("afafas");
        }

        if (Input.GetKeyDown(KeyCode.S))
        {
            gridOB.gridPosition += new Vector2Int(0, 1);

            print("afafas");
        }

    }
}
