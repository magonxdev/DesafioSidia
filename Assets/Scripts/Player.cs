using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
public class Player : MonoBehaviour
{

    [SerializeField] private Slider HPSlider;
    [SerializeField] private TMP_Text Message;
    [SerializeField] private Animator MessageAnimator;
    [SerializeField] private int PlayerHP;
    [SerializeField] private int PlayerAttack;
    [SerializeField] private int PlayerMovement;

    private bool isAlive;
    private int maxHP;

    public Player (int hp, int attack, int movement)
    {
        PlayerHP = hp;
        PlayerAttack = attack;
        PlayerMovement = movement;
        isAlive = true;

    }

    private void Start()
    {
        maxHP = PlayerHP;
        isAlive = true;
    }

    public int GetPlayerHP()
    {

        return PlayerHP;

    }

    public int GetPlayerAttack()
    {
        return PlayerAttack;
    }


    public int GetPlayerMovement()
    {
        return PlayerMovement;
    }

    public bool IsAlive()
    {
        return isAlive;
    }

    public void SetPlayerMessage(string msg, Color c)
    {
        Message.text = msg;
        Message.color = c;
        MessageAnimator.SetTrigger("TextGoingUp");
    }

    public void DebugStats()
    {

        Debug.Log("Player hp is " + PlayerHP);

        Debug.Log("Player attack is " + PlayerAttack);

        Debug.Log("Player movement is " + PlayerMovement);


    }


    public void ReceiveHit(int amount)
    {

        PlayerHP -= amount;

        if (PlayerHP <= 0)
        {
            PlayerHP = 0;
            isAlive = false;
        }
            

        UpdateHPBar();
        SetPlayerMessage("-" + amount, Color.red);

    }

    //Método para atualizar os slider de HP acima do jogador
    void UpdateHPBar()
    {
        HPSlider.value = PlayerHP / (float)maxHP;
    }

    public void HpPowerUp(int amount)
    {
        PlayerHP += amount;
        UpdateHPBar();
        SetPlayerMessage("+HP", Color.red);
    }


    public void AttackPowerUp(int amount)
    {
        PlayerAttack += amount;
        SetPlayerMessage("+ATTACK", Color.blue);
    }



    private void OnTriggerEnter(Collider other)
    {

        if (other.tag == "Player2")
        {

            BattleManager.instance.StartBattle(this, other.GetComponent<Player>());

        } else if (other.tag == "Collectable")
        {
            other.GetComponent<Collectable>().ItemCollected(this);
        }

    }


}
