using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;
using UnityEngine.Networking;
using Newtonsoft.Json;
using SK.GeolocatorWebGL;
using UnityEngine.UI;

/*


http://radicaljam.bplaced.net/index.php?getAllPlayers
 return: {{PLAYER},{PLAYER}....}

http://radicaljam.bplaced.net/index.php?player=CODE,playerLat=LAT,playerLong=LONG
 -> creates CODE if not exists and saves location | updates location
 return: true (player exists) | false (player not exists)

http://radicaljam.bplaced.net/index.php?killPlayer=CODE
 return: true | false



*/


public class ServerManager : MonoBehaviour {

    string baseUrl = "https://nowhere.gallery/radicaljam/API/index.php?";

    public bool sentCallGetAllPlayers = false;
    public bool sentCallLoginPlayer = false;

    public PlayersManager playersManager;
    public GeoLocationService geoManager;
    public SceneService sceneManager;

    public void callGetAllPlayers() {
        StartCoroutine(getAllPlayers());
    }


    public void callLoginPlayer(InputField textMesh)
    {
        string playerCode = textMesh ? textMesh.text.Trim() : "";
        playerCode = Regex.Replace(playerCode, @"\p{Co}+", string.Empty);
        
        if (playerCode.Length == 0 || playerCode == "Send")
        {
            const string glyphs = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789"; //add the characters you want
            int charAmount = 9; //set those to the minimum and maximum length of your string
            string myString = "";
            for(int i=0; i<charAmount; i++)
            {
                myString += glyphs[Random.Range(0, glyphs.Length)];
            }

            playerCode = myString;
            StartCoroutine(loginPlayer(playerCode, geoManager.latitude, geoManager.longitude, false));

        }
        else
        {
            StartCoroutine(loginPlayer(playerCode, geoManager.latitude, geoManager.longitude, true));

        }
        
        Debug.Log(playerCode);
        
    }

    public void callKillUser(string playerCode){

        StartCoroutine(killUser(playerCode));
    }
    
    public void callUpdateUser(Player player){

        StartCoroutine(updatePlayer(player));
    }

    IEnumerator getAllPlayers() {
        sentCallGetAllPlayers = true;
        string uri = "getAllPlayers";
        UnityWebRequest www = UnityWebRequest.Get(baseUrl + uri);
        yield return www.SendWebRequest();
 
        if (www.result != UnityWebRequest.Result.Success) {
            Debug.Log(www.error);
        }
        else {
            // Show results as text
            Player[] playerdto = JsonConvert.DeserializeObject<Player[]>(www.downloadHandler.text);
            playersManager.SetPlayers(playerdto);
            
            if (playersManager.GetPlayer() != null)// Should remove the own player from the list - we don't need to calculate the distance between the same player datas
            {
                playersManager.SetPlayers(removeUserFromPlayerListIfNecessary(playersManager.GetPlayer().code));

                
                Player player = findPlayerFromList(playersManager.GetPlayer().code);//When fetching every user, also update the player ...
                if (player != null)
                {
                    playersManager.SetPlayer(player); 
                }
                
            }
            sentCallGetAllPlayers = true;
        }
    }

    IEnumerator loginPlayer(string playerCode, double playerLat, double playerLong, bool recurring_player) {
        sentCallLoginPlayer = true;
        UnityWebRequest www;
        
        if (recurring_player)
        {
             www = UnityWebRequest.Get(baseUrl + "playerfetch" + "&player=" + playerCode + "&playerLat=" + playerLat + "&playerLong=" + playerLong);
        }
        else
        {
            www = UnityWebRequest.Get(baseUrl +"playercreate" + "&player=" + playerCode + "&playerLat=" + playerLat + "&playerLong=" + playerLong);

        }
        
        yield return www.SendWebRequest();
 
        if (www.result != UnityWebRequest.Result.Success) {
            Debug.Log(www.error);
        }
        else {
            // Show results as text
            Player player = JsonConvert.DeserializeObject<Player>(www.downloadHandler.text);
            playersManager.SetPlayer(player);
            
            playersManager.SetPlayers(removeUserFromPlayerListIfNecessary(player.code));

            Debug.Log(www.downloadHandler.text);
            sceneManager.LoadMainScene();
        }
    }

    IEnumerator killUser(string playerCode) {
        string uri = "killPlayer";
        UnityWebRequest www = UnityWebRequest.Get(baseUrl + uri + "=" + playerCode);
        yield return www.SendWebRequest();
 
        if (www.result != UnityWebRequest.Result.Success) {
            Debug.Log(www.error);
        }
        else {
            // Show results as text
            Debug.Log("Successfully deleted user '" + playerCode + "'");
        }
    }

    IEnumerator updatePlayer(Player player)
    {
        UnityWebRequest www = UnityWebRequest.Get(baseUrl + "playerupdate" + "&player=" + player.code + "&playerLat=" + player.player_lat + "&playerLong=" + player.player_long + "&strength=" + player.strength + "&alive=" + player.alive + "&type=" + player.type);
        yield return www.SendWebRequest();
 
        if (www.result != UnityWebRequest.Result.Success) {
            Debug.Log(www.error);
        }
        else {
            // Show results as text
            Debug.Log("Successfully updated user '" + player.code + "'");
            Player updatedPlayer = JsonConvert.DeserializeObject<Player>(www.downloadHandler.text);
            playersManager.SetPlayer(updatedPlayer);
            
            playersManager.SetPlayers(removeUserFromPlayerListIfNecessary(updatedPlayer.code));
        }
    }

    private List<Player> removeUserFromPlayerListIfNecessary(string playerCode)
    {
        List<Player> players = playersManager.GetPlayers();
        
        for (int i = 0; i < players.Count; i++)
        {
            if (players[i].code == playerCode)
            {
                players.RemoveAt(i);
            }
        }
        
        return players;
    }

    private Player findPlayerFromList(string playerCode)
    {
        List<Player> players = playersManager.GetPlayers();
        
        for (int i = 0; i < players.Count; i++)
        {
            if (players[i].code == playerCode)
            {
                return players[i];
            }
        }

        return null;
    }

    // Start is called before the first frame update
    void Start()
    {
        callGetAllPlayers();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}