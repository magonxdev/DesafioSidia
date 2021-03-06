using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridManager : MonoBehaviour
{

    public GameObject WhiteCell;
    public GameObject BlackCell;

    public int NumRows;
    public int NumCols;
    private bool isWhite;


    void Start()
    {
        CreateGridBase();        
    }

    
    private void CreateGridBase()
    {

        for (int i = 0; i < NumRows; i++)
        {
            for (int j = 0; j < NumCols; j++)
            {

                if (isWhite)
                {
                    GameObject CellInstantiated = Instantiate(WhiteCell, new Vector3(i, 0, j), Quaternion.identity) as GameObject;
                } else
                {
                    GameObject CellInstantiated = Instantiate(BlackCell, new Vector3(i, 0, j), Quaternion.identity) as GameObject;
                }

                isWhite = !isWhite;

            }

            isWhite = !isWhite;
        }

    }

}
