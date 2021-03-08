using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridManager : MonoBehaviour
{
    //Classe que cria um grid 16 x 16 (ou qualquer numero) de quadrados como um tabuleiro xadrez


    [SerializeField] private CollectablesManager collectablesManager;
    [SerializeField] private GameObject WhiteCell;
    [SerializeField] private GameObject BlackCell;
    [SerializeField] private int NumRows;
    [SerializeField] private int NumCols;

    private bool isWhite;
    private List<GameObject> GridCellsList;

    private void Awake()
    {

        GridCellsList = new List<GameObject>();
        CreateGridBase();
    }
        
    //Cria a base de celulas do grid

    private void CreateGridBase()
    {

        for (int i = 0; i < NumRows; i++)
        {
            for (int j = 0; j < NumCols; j++)
            {

                if (isWhite)
                {
                    GameObject CellInstantiated = Instantiate(WhiteCell, new Vector3(i, 0, j), Quaternion.identity, transform) as GameObject;
                    GridCellsList.Add(CellInstantiated);
                } else
                {
                    GameObject CellInstantiated = Instantiate(BlackCell, new Vector3(i, 0, j), Quaternion.identity, transform) as GameObject;
                    GridCellsList.Add(CellInstantiated);
                }

                isWhite = !isWhite;

            }

            isWhite = !isWhite;
        }

    }

    //Método para selecionar posições aleatórias no grid e retornar como uma lista. Usado para spawnar objetos

    public List<Vector3> ChooseRandomCellPositions(int amount)
    {

        List<int> tempChoose = new List<int>();
        List<Vector3> tempPosList = new List<Vector3>();

        for (int i = 0; i < amount; i++)
        {
            int RandomPos = Random.Range(0, GridCellsList.Count);

            if (tempChoose.Contains(RandomPos)) {
                i--;
            } else
            {
                tempChoose.Add(RandomPos);
                tempPosList.Add(GridCellsList[RandomPos].transform.position);
            }


        }

        return tempPosList;

    }

    public List<GameObject> GetCellList()
    {
        return GridCellsList;

    }

}
