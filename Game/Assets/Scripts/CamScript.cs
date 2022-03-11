using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ActiveState{
    public bool active;
}

public class CamScript : MonoBehaviour
{
    public GameObject Player1, Player2, Player3, Player4, Player5;
    private Dictionary<GameObject, ActiveState> activePlayers;

    // Start is called before the first frame update
    void Start()
    {
        activePlayers = new Dictionary<GameObject, ActiveState>()
        {
            {Player1, new ActiveState {active = false}},
            {Player2, new ActiveState {active = false}},
            {Player3, new ActiveState {active = false}},
            {Player4, new ActiveState {active = false}},
            {Player5, new ActiveState {active = false}}
        };
        
    }

    // Update is called once per frame
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

        if (Input.GetButtonDown("Jump"))
        {
            SpawnPlayers("Player1");
            SpawnPlayers("Player2");
        }

        if (Input.GetButtonDown("Fire1"))
        {
            DespawnPlayer("Player2");
        }

    }


    private void SpawnPlayers(String message)
    {
        // if the message is a player name, activate (spawn) player
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
        // if the message is a player name, activate (spawn) player
        foreach(KeyValuePair<GameObject, ActiveState> entry in activePlayers)
        {
            if (message == entry.Key.name)
            {
                activePlayers[entry.Key].active = false;
            }
        }
    }
}
