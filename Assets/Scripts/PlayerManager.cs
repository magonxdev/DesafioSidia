using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerManager : MonoBehaviour
{

    [SerializeField] Player playerOne;
    [SerializeField] Player playerTwo;


    private void Start()
    {
        playerOne.DebugStats();
        playerTwo.DebugStats();

    }

}
