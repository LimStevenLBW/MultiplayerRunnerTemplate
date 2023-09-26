using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;

public class GameDirector : NetworkBehaviour
{
    private const int PLAYERS_REQUIRED_TO_START_GAME = 2;
    private int currentNumberOfPlayers;
    //private NetworkPlayer[] players = new NetworkPlayer[PLAYERS_REQUIRED_TO_START_GAME];

    [SerializeField] private NetworkPlayer player1Prefab;
    [SerializeField] private NetworkPlayer player2Prefab;

    public override void OnNetworkSpawn()
    {
        if (!IsServer) return;
        // Start listening 

        // Start listening for connection/disconnections
        if (NetworkManager.Singleton)
        {
            NetworkManager.Singleton.OnClientConnectedCallback += NewPlayerConnected;
            NetworkManager.Singleton.OnClientDisconnectCallback += PlayerDisconnected;
        }
        // Call connect for host
        //   NewPlayerConnected(OwnerClientId);
        base.OnNetworkSpawn();
    }

    private void NewPlayerConnected(ulong playerID)
    {
        currentNumberOfPlayers++;

        //Spawning is server authoritative
        if (IsServer)
        {
            if(currentNumberOfPlayers == 1)
            {
                var player1 = Instantiate(player1Prefab);
                player1.GetComponent<NetworkObject>().SpawnAsPlayerObject(playerID);
            }
            else if(currentNumberOfPlayers == PLAYERS_REQUIRED_TO_START_GAME)
            {
                var player2 = Instantiate(player2Prefab);
                player2.GetComponent<NetworkObject>().SpawnAsPlayerObject(playerID);
            }
        }


    }

    private void PlayerDisconnected(ulong playerID)
    {
        Debug.Log("Player Disconnected...");
    }


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
