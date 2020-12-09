using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
using TMPro;

[AddComponentMenu("Network/NetworkManagerHUD")]
[RequireComponent(typeof(NetworkManager))]

public class HUDController : MonoBehaviour
{
    [SerializeField] NetworkManagerOverride manager;


    [SerializeField] public bool showGUI = true;
    [SerializeField] GameObject menu;
    [SerializeField] TMP_InputField adress;
    [SerializeField] Button connect;
    [SerializeField] Button create;

    [SerializeField] SpawnEnemy enemySpawn;
    private void Update()
    {
        if (!showGUI) return;
        menu.SetActive(showGUI);
    }


    public void CreateHost()
    {
        manager.StartHost();   
    }

    public void JoinHost()
    {
     
        
         manager.StartClient();

        enemySpawn.RpcSpawn();

        showGUI = false;
    }
    
}
