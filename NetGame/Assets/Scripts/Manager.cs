using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Manager : MonoBehaviour
{
   
    
    [SerializeField] GameObject[] players;
    [SerializeField] GameObject bossWin;
    [SerializeField] GameObject bossLevel;
    [SerializeField] GameObject playerWin;

    private void Awake()
    {
        players = GameObject.FindGameObjectsWithTag("Player");

        bossWin.SetActive(false);
        playerWin.SetActive(false);

        Time.timeScale = 1;
        

    }

    private void LateUpdate()
    {
        if(players.Length <= 0)
        {
            Time.timeScale = 0;
            bossWin.SetActive(true);
        }

        if(bossLevel == null)
        {
            Time.timeScale = 0;
            playerWin.SetActive(true);
        }
    }



}
