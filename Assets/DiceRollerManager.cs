using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DiceRollerManager : MonoBehaviour
{

    public static DiceRollerManager instance;

    [SerializeField] List<Sprite> diceImgSprites;
    [SerializeField] private Image DicePlayerOne;
    [SerializeField] private Image DicePlayerTwo;
    [SerializeField] private int NumberofDiceShakes;
    [SerializeField] private TMP_Text MessageText;
    [SerializeField] private GameObject PanelDiceRoller;
    [SerializeField] private AudioSource DiceRollSound;

    //Singleton
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

    //Método chamadado pello BattleManager para fazer a animação de dados durante o combate

    public void RollCombatDice(int DiceRollPlayerOne, int DiceRollPlayerTwo)
    {
        StartCoroutine(DiceRollAnimation(DiceRollPlayerOne, DiceRollPlayerTwo));
    }


        //Método por dar o feedback visual para o jogador dos dados rolados na tela
        IEnumerator DiceRollAnimation(int DiceRollPlayerOne, int DiceRollPlayerTwo)
    {

        SetMessageText("Rolando dados...");
        DiceRollSound.Play();
        yield return new WaitForSeconds(0.5f);

        for (int i = 0; i < NumberofDiceShakes; i++)
        {

            RollIndividualDice(DicePlayerOne);
            RollIndividualDice(DicePlayerTwo);
            yield return new WaitForSeconds(0.2f);

        }

        SetIndividualDice(DicePlayerOne,DiceRollPlayerOne);
        SetIndividualDice(DicePlayerTwo,DiceRollPlayerTwo);



    }

    //Chamado para rodar individualmente um único dado

    int RollIndividualDice(Image diceImg)
    {

        int diceRolled = Random.Range(0, diceImgSprites.Count);
        diceImg.sprite = diceImgSprites[diceRolled];

        return diceRolled+1;

    }

    //Chamado no final para setar o sprite do dado escolhido

    void SetIndividualDice(Image diceImg, int diceNumber)
    {
        diceImg.sprite = diceImgSprites[diceNumber-1];
    }

    public void SetMessageText(string msg)
    {
        MessageText.text = msg;
    }

    public void TurnOnOffPanelDiceRoller()
    {
        PanelDiceRoller.SetActive(!PanelDiceRoller.activeSelf);
    }


}
