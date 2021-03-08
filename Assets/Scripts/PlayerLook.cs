using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLook : MonoBehaviour
{

    [SerializeField] private GameObject player1;
    [SerializeField] private GameObject player2;

    public void playerLooks()
    {
        player1.transform.LookAt(player2.transform.position);
        player2.transform.LookAt(player1.transform.position);

    }


}
