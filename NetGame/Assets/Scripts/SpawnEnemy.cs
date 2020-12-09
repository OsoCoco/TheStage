using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class SpawnEnemy : NetworkBehaviour
{

    [SerializeField] NetworkManager manager;
    [SerializeField] GameObject enemyPrefab;


    //[ClientRpc]
    public void RpcSpawn()
    {
        Vector2 pos = new Vector2(Random.Range(-2, 2), Random.Range(2, 2));

        GameObject enemy = Instantiate(enemyPrefab, pos, Quaternion.identity);

        NetworkServer.Spawn(enemy);
    }

   
}


