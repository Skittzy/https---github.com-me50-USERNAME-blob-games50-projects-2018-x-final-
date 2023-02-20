using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NpcDialogue : Collidable
{
    public string message1;
    public string message2;
    public string message3;
    public string message4;
    public string message5;

    private float cooldown = 4.0f;
    private float lastShout = - 4.0f;
    private float count = 0;

    protected override void OnCollide(Collider2D coll) {
        if (Time.time - lastShout > cooldown)
        {
            count++;
            lastShout = Time.time;
            if (count == 1) {
                GameManager.instance.ShowText(message1, 25, Color.white, transform.position + new Vector3(0, 0.16f, 0), Vector3.zero, cooldown);
            } else if (count == 2) {
                GameManager.instance.ShowText(message2, 25, Color.white, transform.position + new Vector3(0, 0.16f, 0), Vector3.zero, cooldown);
            } else if (count == 3) {
                GameManager.instance.ShowText(message3, 25, Color.white, transform.position + new Vector3(0, 0.16f, 0), Vector3.zero, cooldown);
            } else if (count == 4) {
                GameManager.instance.ShowText(message4, 25, Color.white, transform.position + new Vector3(0, 0.16f, 0), Vector3.zero, cooldown);
            } else {
                GameManager.instance.ShowText(message5, 25, Color.white, transform.position + new Vector3(0, 0.16f, 0), Vector3.zero, cooldown);
            }
        }
    }
}
