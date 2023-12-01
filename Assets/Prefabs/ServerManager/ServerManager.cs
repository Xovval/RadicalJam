using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization.Json;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.TextCore.Text;
using Newtonsoft.Json;

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

    public void callGetAllPlayers() {
        StartCoroutine(getAllPlayers());
    }


    public void callLoginPlayer(string playerCode, float playerLat, float playerLong){
        StartCoroutine(loginPlayer(playerCode, playerLat, playerLong));
    }

    public void callKillPlayer(string playerCode){

        StartCoroutine(killPlayer(playerCode));
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
        }
    }

    IEnumerator loginPlayer(string playerCode, float playerLat, float playerLong) {
        sentCallLoginPlayer = true;
        UnityWebRequest www = UnityWebRequest.Get(baseUrl + "player=" + playerCode + "playerLat=" + playerLat + "playerLong=" + playerLong);
        yield return www.SendWebRequest();
 
        if (www.result != UnityWebRequest.Result.Success) {
            Debug.Log(www.error);
        }
        else {
            // Show results as text
            Debug.Log(www.downloadHandler.text);
        }
    }

    IEnumerator killPlayer(string playerCode) {

        UnityWebRequest www = UnityWebRequest.Get(baseUrl);
        yield return www.SendWebRequest();
 
        if (www.result != UnityWebRequest.Result.Success) {
            Debug.Log(www.error);
        }
        else {
            // Show results as text
            Debug.Log(www.downloadHandler.text);
        }
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