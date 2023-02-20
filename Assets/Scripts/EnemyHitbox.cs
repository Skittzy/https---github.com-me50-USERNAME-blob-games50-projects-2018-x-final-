using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHitbox : Collidable
{
    //Damage
    public int damage;
    public float pushForce;
    private float cooldown = 1.0f;
    private float lastShout = - 1.0f;

    protected override void OnCollide(Collider2D coll) 
    {
        if (coll.tag == "Fighter" && coll.name == "Player")
        {
            if (Time.time - lastShout > cooldown)
            {
                lastShout = Time.time;
                if (GameManager.instance.player.isAlive)
                    FindObjectOfType<AudioManager>().Play("hurt");
            }

            //Create a new damage object, then send it to the player (same as weapon.cs)
            Damage dmg = new Damage
            {
                damageAmount = damage,
                origin = transform.position,
                pushForce = pushForce
            };
            
            coll.SendMessage("ReceiveDamage", dmg);
        }
    }
}
