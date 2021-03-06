using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Player : MonoBehaviour
{

    [SerializeField] private TMP_Text Message;
    [SerializeField] private Animator MessageAnimator;
    [SerializeField] private int PlayerHP;
    [SerializeField] private int PlayerAttack;
    [SerializeField] private int PlayerMovement;

    private bool isAlive;


    public Player (int hp, int attack, int movement)
    {
        PlayerHP = hp;
        PlayerAttack = attack;
        PlayerMovement = movement;
        isAlive = true;

    }

    private void Start()
    {
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

    public void SetPlayerMessage(string msg)
    {
        Message.text = msg;
        MessageAnimator.SetTrigger("TextGoingUp");
    }

    public void DebugStats()
    {

        Debug.Log("Player hp is " + PlayerHP);

        Debug.Log("Player attack is " + PlayerAttack);

        Debug.Log("Player movement is " + PlayerMovement);


    }

    private void OnTriggerEnter(Collider other)
    {
        
        if (other.tag == "Player2")
        {

            BattleManager.instance.StartBattle(this, other.GetComponent<Player>());

        }

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

    }

    void UpdateHPBar()
    {
        SetPlayerMessage("Received hit! HP is now " + PlayerHP);
    }



}
