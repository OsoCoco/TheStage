using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class MinionSpawn : NetworkBehaviour
{
    [SerializeField] GameObject minionsPrefab;

    public void Spawn(Vector2 pos)
    {
        CmdSpawn(pos);
    }

    [Command]
     void CmdSpawn(Vector2 position)
    {
        RpcSpawnMinions(position);
    }
    [ClientRpc]
     void RpcSpawnMinions(Vector2 position)
    {
        if(isServer)
        {
            Vector2 random = new Vector2(Random.Range(-1, 1), Random.Range(-1, 1));
            GameObject Go = Instantiate(minionsPrefab, position + random, Quaternion.identity);

            NetworkServer.Spawn(Go);
        }
    }
}
