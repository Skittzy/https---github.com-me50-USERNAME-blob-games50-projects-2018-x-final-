using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Mover
{
    public bool isAlive = true;
    public bool hasWon = false;
    public bool isWalking = false;

    protected override void ReceiveDamage(Damage dmg)
    {
        if (!isAlive || hasWon)
            return;
        
        base.ReceiveDamage(dmg);
        GameManager.instance.onHitpointChange();
    }

    protected override void Death()
    {
        isAlive = false;
        
        GameManager.instance.deathMenuAnimator.SetTrigger("Show");

        FindObjectOfType<AudioManager>().StopPlaying("bossBattleMusic");
        FindObjectOfType<AudioManager>().StopPlaying("ambience");
        FindObjectOfType<AudioManager>().Play("deathMenu");
    }

    private void FixedUpdate() {
        float x = Input.GetAxisRaw("Horizontal");
        float y = Input.GetAxisRaw("Vertical");

        // Flip Sprite whether we are going right or left
        if(x > 0) {
            transform.localScale = new Vector3(-1, 1, 1);
        } else if(x < 0) {
            transform.localScale = Vector3.one;
        }

        if (isAlive && !hasWon)
            UpdateMotor(new Vector3(x, y, 0));
    }

    public void OnLevelUp()
    {
        maxHitpoint = maxHitpoint + 1;
        hitpoint = maxHitpoint;
        GameManager.instance.onHitpointChange();
    }
    
    public void SetLevel(int level)
    {
        for (int i = 0; i < level; i++)
            OnLevelUp();
    }
}
