using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnManager : MonoBehaviour
{

    //Singleton para acessar facilmente os turnos
    public static TurnManager instance;

    TurnPlayer actualTurn;

    private void Awake()
    {
        
        if (instance == null)
        {
            instance = this;
        } else
        {
            Destroy(instance.gameObject);
        }

    }

    private void Start()
    {

       actualTurn = TurnPlayer.PlayerOneTurn;

    }

    //Troca os turnos entre jogador 1 e 2
    public void ChangeTurn()
    {

        if (actualTurn == TurnPlayer.PlayerOneTurn)
        {
            actualTurn = TurnPlayer.PlayerTwoTurn;
        } else
        {
            actualTurn = TurnPlayer.PlayerOneTurn;
        }

    }

    public TurnPlayer GetActualTurn()
    {
        return actualTurn;

    }


}
