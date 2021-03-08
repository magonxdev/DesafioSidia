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
    [SerializeField] private AudioSource HitSound;
    [SerializeField] private AudioSource BackgroundMusic;
    [SerializeField] private PlayerLook playerLook;
    [SerializeField] private AudioClip BattleMusic;
    [SerializeField] private AudioClip BGMusic;
    [SerializeField] private AudioClip EndMusic;

    Player playerOne;
    Player playerTwo;

    List<int> DiceListPlayerOne;
    List<int> DiceListPlayerTwo;

    private int BattleRound;
    private bool StartedBattle;

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

    public bool GetBattleStarted()
    {
        return StartedBattle;
    }

    // Start is called before the first frame update
    void Start()
    {
        DiceListPlayerOne = new List<int>();
        DiceListPlayerTwo = new List<int>();

    }

    public void StartBattle(Player player1, Player player2)
    {

        if (!StartedBattle)
        {
            StartedBattle = true;
            CameraBattleChanger.instance.ChangeToBattleCamera();
            BackgroundMusic.clip = BattleMusic;
            BackgroundMusic.Play();
            playerOne = player1;
            playerTwo = player2;
            playerOne.ResetMovingTriggers();
            playerTwo.ResetMovingTriggers();
            playerOne.FightIdleAnimation();
            playerTwo.FightIdleAnimation();
            RollTheDices();
            DiceRollerManager.instance.TurnOnOffPanelDiceRoller();
            NewBattleTurn();
            
        }
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

        playerLook.playerLooks();

        //Checa e compara os dados lançados e aguarda alguns segundos
        Debug.Log("Jogador um rolou " + DiceListPlayerOne[BattleRound]);
        yield return new WaitForSeconds(1f);
        Debug.Log("Jogador dois rolou " + DiceListPlayerTwo[BattleRound]);
        yield return new WaitForSeconds(1f);


        //Animação de rolagem de dados na tela
        DiceRollerManager.instance.RollCombatDice(DiceListPlayerOne[BattleRound], DiceListPlayerTwo[BattleRound]);

        yield return new WaitForSeconds(3f);

        //Compara os dados
        //Em caso do jogador um ganhar a rodada
        if (DiceListPlayerOne[BattleRound] > DiceListPlayerTwo[BattleRound])
        {

            DiceRollerManager.instance.SetMessageText("Jogador um ganhou a rodada");            
            Debug.Log("Jogador um ganhou a rodada com " + DiceListPlayerOne[BattleRound]);
            Debug.Log("Jogador um acerta o jogador dois batendo " + playerOne.GetPlayerAttack());

            playerTwo.ReceiveHit(playerOne);

            yield return new WaitForSeconds(0.3f);
            HitSound.Play();

        }

        //Em caso do jogador dois ganhar a rodada
        else if (DiceListPlayerOne[BattleRound] < DiceListPlayerTwo[BattleRound])
        {


            DiceRollerManager.instance.SetMessageText("Jogador dois ganhou a rodada");
            Debug.Log("Jogador dois ganhou a rodada com " + DiceListPlayerTwo[BattleRound]);
            Debug.Log("Jogador dois acerta o jogador um batendo " + playerTwo.GetPlayerAttack());

            playerOne.ReceiveHit(playerTwo);

            yield return new WaitForSeconds(0.3f);
            HitSound.Play();
        }

        //Em caso de empate
        else if (DiceListPlayerOne[BattleRound] == DiceListPlayerTwo[BattleRound])
        {

            DiceRollerManager.instance.SetMessageText("Empate");
            Debug.Log("A rodada empatou, ninguém sofrerá hit " + DiceListPlayerOne[BattleRound]);
        }


        yield return new WaitForSeconds(1f);

        //Checamos pra ver se algum jogador morreu no processo

        if (!playerOne.IsAlive())
        {

            DiceRollerManager.instance.SetMessageText("Jogador um morreu");
            Debug.Log("Jogador um morreu");
            CleanBattleData();
            Destroy(playerOne.gameObject,2);
            UIManager.instance.ShowGameOverPanel();
            UIManager.instance.ChangeWinnerText("PLAYER 2 WINS!");
            MovementManager.instance.EndGame();
            BackgroundMusic.clip = EndMusic;
            BackgroundMusic.Play();

        } else if (!playerTwo.IsAlive())
        {

            DiceRollerManager.instance.SetMessageText("Jogador dois morreu");
            Debug.Log("Jogador dois morreu");
            CleanBattleData();
            Destroy(playerTwo.gameObject, 2);
            UIManager.instance.ShowGameOverPanel();
            UIManager.instance.ChangeWinnerText("PLAYER 1 WINS!");
            MovementManager.instance.EndGame();
            BackgroundMusic.clip = EndMusic;
            BackgroundMusic.Play();
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
                EndBattleRound();
            }
        }
    }

    void EndBattleRound()
    {


        CleanBattleData();
        DiceRollerManager.instance.TurnOnOffPanelDiceRoller();
        StartedBattle = false;
        CameraBattleChanger.instance.ChangeToFollowCamera();
        BackgroundMusic.clip = BGMusic;
        BackgroundMusic.Play();
        playerOne.EndFightAnimation();
        playerTwo.EndFightAnimation();

    }

    void CleanBattleData()
    {
        BattleRound = 0;
        DiceListPlayerOne = new List<int>();
        DiceListPlayerTwo = new List<int>();

    }


}
