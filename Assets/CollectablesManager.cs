using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectablesManager : MonoBehaviour
{
    [SerializeField] private GridManager GridManager;
    [SerializeField] private GameObject CollectableItemPrefab;
    [SerializeField] private int CollectablesAmount;
    List<Vector3> PositionsToSpawn;

    private void Start()
    {
        PositionsToSpawn = new List<Vector3>();
        AddToSpawnPositionList();
    }

    private void AddToSpawnPositionList()
    {
        PositionsToSpawn = GridManager.ChooseRandomCellPositions(CollectablesAmount);
        SpawnCollectables();
    }

    public void SpawnCollectables()
    {

        for (int i = 0; i < CollectablesAmount; i++)
        {

            //Pode gerar entre dois tipos de itens diferentes
            int RandomCollectable = Random.Range(0, 2);

            //Vai spawnar um item de HP
            if (RandomCollectable == 0)
            {
                SpawnItem(PositionsToSpawn[i], AttributeType.ItemHP, Color.red);
            }

            //Vai spawnar um item de Attack
            else if (RandomCollectable == 1)
            {
                SpawnItem(PositionsToSpawn[i], AttributeType.ItemAttack, Color.cyan);
            }

        }
        

    }

    //Instancia o item a ser criado, adicionando dinamicamente um tipo de enumerador nele, o método COllectable e uma cor baseado no seu tipo
    private void SpawnItem (Vector3 positionToSpawn, AttributeType itemType, Color c)
    {

        GameObject item = Instantiate(CollectableItemPrefab, positionToSpawn, Quaternion.identity);
        Collectable collectable  = item.GetComponent<Collectable>();
        collectable.SetCollectableType(itemType);
        item.name = itemType.ToString();
        item.GetComponent<MeshRenderer>().material.color = c;

    }
    

}
