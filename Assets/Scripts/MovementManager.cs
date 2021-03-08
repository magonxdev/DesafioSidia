using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementManager : MonoBehaviour
{
    public static MovementManager instance;

    [SerializeField] private Animator KnightAnimP1;
    [SerializeField] private Animator KnightAnimP2;
    [SerializeField] private Camera mainCamera;
    [SerializeField] private GameObject Player1;
    [SerializeField] private GameObject Player2;
    [SerializeField] private GameObject GhostPlayer;
    [SerializeField] private float MoveSpeed;

    private Animator KnightAnim;
    private GameObject Player;

    private bool startedGame;
    private bool isSelectingDestination;
    private bool needsToMove;
    private GameObject ghost;
    private Vector3 TargetDestinationToMove;


    private void Awake()
    {

        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(instance.gameObject);
        }

    }

    private void Update()
    {

        CreateGhostToFollowMouse();

        if (needsToMove)
        {
            PlayerMoving();
        }


    }

    private void Start()
    {
        ChangeActualPlayer();
    }

    void CreateGhostToFollowMouse()
    {

        //Somente deixa se movimentar se a batalha tiver começado
        if (!BattleManager.instance.GetBattleStarted() && startedGame)
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



    }

    private void MoveToDestination()
    {

        KnightAnim.SetTrigger("KnightRunning");
        needsToMove = true;
        TargetDestinationToMove = ghost.transform.position;
        KnightAnim.transform.LookAt(TargetDestinationToMove);

    }

    private void PlayerMoving()
    {
        Player.transform.position = Vector3.Lerp(Player.transform.position, TargetDestinationToMove, Time.deltaTime * MoveSpeed);

        if (Vector3.Distance(Player.transform.position,TargetDestinationToMove) <= 0.05f)
        {
            needsToMove = false;
            KnightAnim.SetTrigger("KnightArrived");
            TurnManager.instance.ChangeTurn();
            ChangeActualPlayer();
        }

    }

    private void ChangeActualPlayer()
    {

        if (TurnManager.instance.GetActualTurn() == TurnPlayer.PlayerOneTurn)
        {

            Player = Player1;
            KnightAnim = KnightAnimP1;
            CameraBattleChanger.instance.ChangeTargetFollowingP1();

        } else if (TurnManager.instance.GetActualTurn() == TurnPlayer.PlayerTwoTurn)
        {
            Player = Player2;
            KnightAnim = KnightAnimP2;
            CameraBattleChanger.instance.ChangeTargetFollowingP2();
        }

    }

    public void StartGame()
    {

        startedGame = true;

    }

    public void EndGame()
    {
        startedGame = false;
    }



}
