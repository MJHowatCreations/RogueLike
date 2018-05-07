using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wall : MonoBehaviour {

    public Sprite dmgSprite; //Alternate sprite to display after Wall has been attacked by player.
    public int hp = 3; //hit points for the wall.

    private SpriteRenderer spriteRenderer; //Store a component reference to the attached SpriteRenderer.

    // Use this for initialization
    void Awake ()
    {
        //Get a component reference to the SpriteRenderer.
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public void DamageWall (int loss)
    {
        //Set spriteRenderer to the damaged wall sprite.
        spriteRenderer.sprite = dmgSprite;

        //Subtract loss from hit point total.
        hp -= loss;

        if (hp <= 0)
        {
            //Disable gameObject
            gameObject.SetActive(false);
        }
    }

}
