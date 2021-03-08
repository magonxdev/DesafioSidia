using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostFollowMouse : MonoBehaviour
{

    public Material tintBlueMaterial;
    public Material tintRedMaterial;

    private Material lastSavedMaterial;
    private GameObject lastHitCell;
    private GameObject newHitCell;

    bool started;

    // Update is called once per frame


    //Método que cria um fantasma de clique para poder movimentar o jogador pelo tabuleiro

    void Update()
    {
        Vector3 mousePos = new Vector3(Input.mousePosition.x, 1, Input.mousePosition.z);

        Ray ray;
        RaycastHit hit;

        ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out hit)) {


            //Caso clique em uma célula

            if (hit.collider.gameObject.tag == "GridCell")
            {
                newHitCell = hit.transform.gameObject;

                if (lastHitCell == null)
                {
                    lastHitCell = hit.transform.gameObject;
                    lastSavedMaterial = lastHitCell.GetComponent<MeshRenderer>().material;
                    newHitCell = hit.transform.gameObject;
                    transform.position = new Vector3(hit.transform.position.x, 1, hit.transform.position.z);

                }
                else if (lastHitCell.transform.position != newHitCell.transform.position)
                {
                    transform.position = new Vector3(hit.transform.position.x, 1, hit.transform.position.z);
                    lastHitCell.GetComponent<MeshRenderer>().material = lastSavedMaterial;
                    MeshRenderer tempCellRenderer = newHitCell.GetComponent<MeshRenderer>();
                    lastSavedMaterial = tempCellRenderer.material;
                    tempCellRenderer.material = tintBlueMaterial;
                    lastHitCell = newHitCell;

                }
            }

        }
    }
}
