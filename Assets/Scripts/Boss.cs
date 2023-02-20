using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : Enemy
{
    public float[] swordSpeed = {2.5f, -2.5f};
    public float distance = 0.45f;
    public Transform[] swords;


    private void Update() {
        for (int i = 0; i < swords.Length; i++)
        {
            swords[i].position = transform.position + new Vector3(-Mathf.Cos(Time.time * swordSpeed[i]) * distance, Mathf.Sin(Time.time * swordSpeed[i]) * distance, 0);
        }

        if (hitpoint == 0)
            GameManager.instance.bossHealthAnim.SetTrigger("Hide");
    }

    protected override void Death()
    {
        base.Death();

        FindObjectOfType<AudioManager>().Play("bossDeath");

        GameManager.instance.player.hasWon = true;
        GameManager.instance.winMenuAnimator.SetTrigger("Show");

        FindObjectOfType<AudioManager>().StopPlaying("bossBattleMusic");
        FindObjectOfType<AudioManager>().StopPlaying("ambience");
        FindObjectOfType<AudioManager>().Play("winScreen");
    }
}
