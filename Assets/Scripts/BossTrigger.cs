using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossTrigger : Collidable
{
    protected override void OnCollide(Collider2D coll) {
        if(coll.name == "Player") {
            //Show Boss Health bar
            GameManager.instance.onBossBattleEnter();
        }
    }
}
