using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
using System;
using System.Net;

public class NetworkManagerOverride : NetworkManager 
{
    public SpawnEnemy spawnEnemy;
    public string myIP = "189.213.87.113";

    public RawImage qrImage;
    public RawImage qrCamera;

    public string address;
    
    public void ToogleQrCode()
    {
        qrImage.gameObject.SetActive(!qrImage.gameObject.activeSelf);
    }

    public override void OnStartHost()
    {
        base.OnStartHost();
       
         address = $"{myIP} : {networkPort}";
    }

    public override void OnServerAddPlayer(NetworkConnection conn, short playerControllerId)
    {
        base.OnServerAddPlayer(conn, playerControllerId);

        Debug.LogError(numPlayers);

       if(numPlayers >= 2 )
        spawnEnemy.RpcSpawn();
            
    }
}
