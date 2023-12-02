using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayersManager : MonoBehaviour {
    [SerializeField] private ServerManager serverManager;
    [SerializeField] private Player player;
    [SerializeField] private List<Player> players;
    private void Update()
    {
        if (!serverManager.sentCallGetAllPlayers) {
            serverManager.callGetAllPlayers();//Duplicate?
        }
    }
  
    public void SetPlayers(Player[] players)
    {
        this.players.Clear();
        foreach (Player player in players)
        {
            this.players.Add(player);
        }
    }
    
    public void SetPlayers(List<Player> players)
    {
        this.players = players;
    }

    public List<Player> GetPlayers()
    {
        return this.players;
    }

    public Player GetPlayer()
    {
        return this.player;
    }

    public List<Player> GetNearbyPlayers()
    {
        return this.players; // TODO Implement
    }

    public Player GetClosestPlayer()
    {
        return this.player; // TOOD Implement
    }

    public void CreatePlayerObject(Text code)
    {
        this.player.code = code.ToString();
        this.player.player_lat = 99;
        this.player.player_long = 99;
        this.player.alive = 0;
        this.player.type = 0;
        this.player.strength = 1;
        this.player.timestamp = DateTime.Now.ToString(); //just for local use
        
        // Call Servermanager to register
    }

    public void SetPlayer(Player player)
    {
        this.player = player; 
    }
}