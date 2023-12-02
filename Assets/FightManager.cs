using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FightManager : MonoBehaviour
{

    public Sprite vampireSpritefront;
    public Sprite vampireSpriteback;
    public Sprite humanSpritefront;
    public Sprite humanSpriteback;

    public SpriteRenderer attackerSprite;
    public SpriteRenderer attackedSprite;

    // Start is called before the first frame update
    void Start()
    {
        setupFight(0,1);   
    }

    void setupFight(int attackerType, int attackedType) {
        if (attackerType == 0) {
            attackerSprite.sprite = humanSpriteback;
        }

        if (attackerType == 1) {
            attackerSprite.sprite = vampireSpriteback;
        }

        if (attackedType == 0) {
            attackedSprite.sprite = humanSpritefront;
        }

        if (attackedType == 1) {
            attackedSprite.sprite = vampireSpritefront;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
