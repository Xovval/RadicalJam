using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FightManager : MonoBehaviour
{
    
    public Player currentPlayer;
    
    public Player currentEnemy;

    public List<Player> nearbyPlayers;
    public PlayersManager playersManager;
    public Sprite vampireSpritefront;
    public Sprite vampireSpriteback;
    public Sprite humanSpritefront;
    public Sprite humanSpriteback;

    public SpriteRenderer attackerSprite;
    public SpriteRenderer attackedSprite;

    // Start is called before the first frame update
    void Start()
    {
        currentPlayer = playersManager.GetPlayer();

        //must be an enemy!!!!!!!
        currentEnemy = playersManager.GetClosestPlayer();

        nearbyPlayers = playersManager.GetNearbyPlayers();

        Debug.Log(nearbyPlayers.Count + " players take part in the current fight.");


        setupFight(currentPlayer, currentEnemy);   
    }

    void setupFight(Player currentPlayer, Player currentEnemy) {
        if (currentPlayer.type == 0) {
            attackerSprite.sprite = humanSpriteback;
        }

        if (currentPlayer.type == 1) {
            attackerSprite.sprite = vampireSpriteback;
        }

        if (currentEnemy.type == 0) {
            attackedSprite.sprite = humanSpritefront;
        }

        if (currentEnemy.type == 1) {
            attackedSprite.sprite = vampireSpritefront;
        }
    }

    //returns false if fight is lost and true if its won.
    public bool calculateFight() {
        int strengthPlayerTeam = 0;
        int strengthEnemyTeam = 0;

        foreach (var player in nearbyPlayers) {
            if (player.type == currentPlayer.type) {
                if (player.code != currentPlayer.code)
                    {
                        strengthPlayerTeam += player.strength;
                    }
            } else if (player.code != currentEnemy.code) {
                strengthEnemyTeam += player.strength;
            }
        }

        if (strengthPlayerTeam > strengthEnemyTeam) 
        {
            return fightWin();
        } else {
            return fightLose();
        }
    }

    public bool fightWin() {
        playersManager.killPlayer(currentEnemy);

        return true;
    }

    public bool fightLose(){
        
        if (currentPlayer.type == 0) {
            currentPlayer.type = 1;
            playersManager.SetPlayer(currentPlayer);

            Debug.Log("Oh no, you turned into a vampire :()");
        }
        
        if (currentPlayer.type == 1) {
            currentPlayer.alive = 0;
            playersManager.SetPlayer(currentPlayer);

            playersManager.killPlayer(currentPlayer);

            Debug.Log("Oh no, you died");

            //TODO: trigger some event that logs the player out

        }
        return false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
