using System;
using UnityEngine;
using UnityEngine.UI;

public class PlayersManager : MonoBehaviour {
    [SerializeField] private ServerManager serverManager;
    [SerializeField] private Player player;
    [SerializeField] private Player[] players;
    private void Update()
    {
        if (!serverManager.sentCallGetAllPlayers) {
            serverManager.callGetAllPlayers();//Duplicate?
        }
    }
  
    public void SetPlayers(Player[] players)
    {
        this.players = players;
    }

    public Player[] GetPlayers()
    {
        return this.players;
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