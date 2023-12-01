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
}