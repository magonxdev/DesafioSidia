using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class BattleManager : MonoBehaviour
{

    //Singleton
    public static BattleManager instance;

    [SerializeField] private int NumberOfDiceRolls;
    [SerializeField] private int MaximumRounds;

    Player playerOne;
    Player playerTwo;

    List<int> DiceListPlayerOne;
    List<int> DiceListPlayerTwo;

    private int BattleRound;

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

    // Start is called before the first frame update
    void Start()
    {
        DiceListPlayerOne = new List<int>();
        DiceListPlayerTwo = new List<int>();

    }

    public void StartBattle(Player player1, Player player2)
    {
        playerOne = player1;
        playerTwo = player2;
        RollTheDices();
        NewBattleTurn();
    }

    void NewBattleTurn()
    {
        StartCoroutine(BattleTurn());
    }

    void RollTheDices()
    {
        
        //Gera 3 números aleatórios de dados

        for (int i  = 0; i < NumberOfDiceRolls; i++)
        {
            int DiceRoll = Random.Range(1, 7);
            DiceListPlayerOne.Add(DiceRoll);
        }

        for (int i = 0; i < NumberOfDiceRolls; i++)
        {
            int DiceRoll = Random.Range(1, 7);
            DiceListPlayerTwo.Add(DiceRoll);
        }

        //Organiza os números numa lista  a serem comparados em ordem

        DiceListPlayerOne = DiceListPlayerOne.OrderByDescending(n => n).ToList<int>();
        DiceListPlayerTwo = DiceListPlayerTwo.OrderByDescending(n => n).ToList<int>();


    }

    IEnumerator BattleTurn()
    {

        //Inicia uma rodada de batalha 
        Debug.Log("Nova rodada de batalha! Numero " + BattleRound + 1);

        //Checa e compara os dados lançados e aguarda alguns segundos
        Debug.Log("Jogador um rolou " + DiceListPlayerOne[BattleRound]);
        yield return new WaitForSeconds(1f);
        Debug.Log("Jogador dois rolou " + DiceListPlayerTwo[BattleRound]);
        yield return new WaitForSeconds(1f);

        //Compara os dados


        //Em caso do jogador um ganhar a rodada
        if (DiceListPlayerOne[BattleRound] > DiceListPlayerTwo[BattleRound])
        {
            Debug.Log("Jogador um ganhou a rodada com " + DiceListPlayerOne[BattleRound]);
            Debug.Log("Jogador um acerta o jogador dois batendo " + playerOne.GetPlayerAttack());

            playerTwo.ReceiveHit(playerOne.GetPlayerAttack());

        }

        //Em caso do jogador dois ganhar a rodada
        else if (DiceListPlayerOne[BattleRound] < DiceListPlayerTwo[BattleRound])
        {

            Debug.Log("Jogador dois ganhou a rodada com " + DiceListPlayerTwo[BattleRound]);
            Debug.Log("Jogador dois acerta o jogador um batendo " + playerTwo.GetPlayerAttack());

            playerOne.ReceiveHit(playerTwo.GetPlayerAttack());
        }

        //Em caso de empate
        else if (DiceListPlayerOne[BattleRound] == DiceListPlayerTwo[BattleRound])
        {
            Debug.Log("A rodada empatou, ninguém sofrerá hit " + DiceListPlayerOne[BattleRound]);
        }


        yield return new WaitForSeconds(1f);

        //Checamos pra ver se algum jogador morreu no processo

        if (!playerOne.IsAlive())
        {
            Debug.Log("Jogador um morreu");
            CleanBattleData();
            Destroy(playerOne.gameObject,2);
        } else if (!playerTwo.IsAlive())
        {
            Debug.Log("Jogador dois morreu");
            CleanBattleData();
            Destroy(playerTwo.gameObject, 2);
        } else
        {

            // Incrementa o contador de rodadas
            BattleRound++;


            //Inicia mais uma vez uma nova rodada, somente se não tiver passado do número maximo de rodadas na batalha

            if (BattleRound < MaximumRounds)
            {

                Debug.Log("Aguardando começar nova rodada... " + BattleRound);

                yield return new WaitForSeconds(2f);
                NewBattleTurn();
            }
            else
            {
                CleanBattleData();
            }
        }
    }

    void CleanBattleData()
    {
        BattleRound = 0;
        DiceListPlayerOne = new List<int>();
        DiceListPlayerTwo = new List<int>();

    }


}
