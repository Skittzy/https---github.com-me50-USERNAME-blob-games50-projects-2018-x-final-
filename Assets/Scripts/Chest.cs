using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chest : Collectable
{
    public Sprite emptyChest;
    public int coinAmount = 50;
    public int xpAmount = 5;

    protected override void OnCollect() {
        if (!collected) {
            FindObjectOfType<AudioManager>().Play("chest");
            collected = true;
            GetComponent<SpriteRenderer>().sprite = emptyChest;
            GameManager.instance.ShowText("+" + coinAmount + " coins!", 25, Color.yellow, transform.position, Vector3.up * 25, 1.0f);
            GameManager.instance.ShowText("+" + xpAmount + " xp!", 25, new Color(0.0f, 0.89f, 1.0f, 1.0f), transform.position + new Vector3(0.0f, -0.05f, 0.0f), Vector3.down * 25, 1.0f);

            GameManager.instance.GrantXp(xpAmount);
            GameManager.instance.coins += coinAmount;
        }
    }
}
