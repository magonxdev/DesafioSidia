using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraBattleChanger : MonoBehaviour
{

    public static CameraBattleChanger instance;


    [SerializeField] private GameObject MenuCamera;
    [SerializeField] private GameObject BattleCamera;
    [SerializeField] private GameObject FollowCamera;
    [SerializeField] private GameObject Player1;
    [SerializeField] private GameObject Player2;



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

    public void ChangeToBattleCamera()
    {
        FollowCamera.SetActive(false);
        BattleCamera.SetActive(true);
    }

    public void ChangeToFollowCamera()
    {

        FollowCamera.SetActive(true);
        BattleCamera.SetActive(false);
    }

    public void ChangeTargetFollowingP1()
    {

        FollowCamera.GetComponent<CinemachineVirtualCamera>().Follow = Player1.transform;

    }


    public void ChangeTargetFollowingP2()
    {

        FollowCamera.GetComponent<CinemachineVirtualCamera>().Follow = Player2.transform;

    }

    public void StartGameCamera()
    {

        FollowCamera.SetActive(true);
        BattleCamera.SetActive(false);
        MenuCamera.SetActive(false);


    }


}
