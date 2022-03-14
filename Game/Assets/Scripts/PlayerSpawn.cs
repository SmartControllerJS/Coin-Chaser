using System;
using System.Collections.Generic;
using UnityEngine;


public class ActiveState{
    public bool active;
}

public class PlayerSpawn : MonoBehaviour
{
    public GameObject Player1, Player2, Player3, Player4, Player5;
    private Dictionary<GameObject, ActiveState> activePlayers;

    void Start()
    {
        activePlayers = new Dictionary<GameObject, ActiveState>()
        {
            // Deactivate all players upon start
            {Player1, new ActiveState {active = false}},
            {Player2, new ActiveState {active = false}},
            {Player3, new ActiveState {active = false}},
            {Player4, new ActiveState {active = false}},
            {Player5, new ActiveState {active = false}}
        };
        
    }

    public void Update()
    {
        foreach(KeyValuePair<GameObject, ActiveState> entry in activePlayers)
        {
            if (entry.Value.active == true)
            {
                entry.Key.SetActive(true);
            } 
            else {
                entry.Key.SetActive(false);
            }
        }
    }


    private void SpawnPlayers(String message)
    {
        // If the message is a player name, activate (spawn) player
        foreach(KeyValuePair<GameObject, ActiveState> entry in activePlayers)
        {
            if (message == entry.Key.name)
            {
                activePlayers[entry.Key].active = true;
            }
        }
        
    }

    private void DespawnPlayer(String message)
    {
        // If the message is a player name, activate (spawn) player
        foreach(KeyValuePair<GameObject, ActiveState> entry in activePlayers)
        {
            if (message == entry.Key.name)
            {
                activePlayers[entry.Key].active = false;
            }
        }
    }
}
