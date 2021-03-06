using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementManager : MonoBehaviour
{

    public Camera mainCamera;
    public GameObject Player;
    public GameObject GhostPlayer;
    public float MoveSpeed;


    private bool isSelectingDestination;
    private bool needsToMove;
    private GameObject ghost;
    private Vector3 TargetDestinationToMove;

    private void Update()
    {

        CreateGhostToFollowMouse();

        if (needsToMove)
        {
            PlayerMoving();
        }


    }

    void CreateGhostToFollowMouse()
    {

        if (Input.GetMouseButtonDown(0) && !isSelectingDestination)
        {
            isSelectingDestination = true;
            Vector3 mousePos = mainCamera.ScreenToWorldPoint(Input.mousePosition);
            ghost = Instantiate(GhostPlayer, mousePos, Quaternion.identity);

        }
        else if (Input.GetMouseButtonDown(0) && isSelectingDestination)
        {
            MoveToDestination();
            Destroy(ghost);
            isSelectingDestination = false;
        }




    }

    private void MoveToDestination()
    {

        needsToMove = true;
        TargetDestinationToMove = ghost.transform.position;

    }

    private void PlayerMoving()
    {
        Player.transform.position = Vector3.Lerp(Player.transform.position, TargetDestinationToMove, Time.deltaTime * MoveSpeed);

        if (Vector3.Distance(Player.transform.position,TargetDestinationToMove) <= 0.01f)
        {
            needsToMove = false;
        }

    }



}
