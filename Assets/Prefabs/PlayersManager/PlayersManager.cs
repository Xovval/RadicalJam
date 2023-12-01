using UnityEngine;

public class PlayersManager : MonoBehaviour {
    public ServerManager serverManager;

    public Player[] players;
    private void Update()
    {
        if (!serverManager.sentCallGetAllPlayers) {
            serverManager.callGetAllPlayers();
        }
        //Debug.Log(players.ToString());
    }


    public void SetPlayers(Player[] players)
    {
        this.players = players;
    }
}