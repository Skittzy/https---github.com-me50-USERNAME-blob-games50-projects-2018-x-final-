using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Mover
{
    // Exp
    public int xpValue = 1;

    // Logic
    public float triggerLenght = 1;
    public float chaseLength = 5;
    private bool chasing;
    private bool collidingWithPlayer;
    private Transform playerTransform;
    private Vector3 startingPosition;

    //Hitbox
    public ContactFilter2D filter;
    private BoxCollider2D hitbox;
    private Collider2D[] hits = new Collider2D[10];

    protected override void Start() {
        base.Start();
        playerTransform = GameObject.Find("Player").transform;
        startingPosition = transform.position;
        hitbox = transform.GetChild(0).GetComponent<BoxCollider2D>();
    }

    private void FixedUpdate() {
        //Is the player in range?
        if(Vector3.Distance(playerTransform.position, startingPosition) < chaseLength) {
            // if player is within chasing distance then start chasing him (chasing = true)
            if (Vector3.Distance(playerTransform.position, startingPosition) < triggerLenght)
                chasing = true;                

            if (chasing){
                if (!collidingWithPlayer)
                {
                    UpdateMotor((playerTransform.position - transform.position).normalized);
                }
            } else
            {
                //if not chasing go back to starting position
                UpdateMotor(startingPosition - transform.position);
            }
        } else {
            // If player is out of max chasing length return to start pos.
            UpdateMotor(startingPosition - transform.position);
            chasing = false;
        }

        //Check for overlaps (reusing the logic from collidable.cs)
        collidingWithPlayer = false;
        boxCollider.OverlapCollider(filter, hits);
        for (int i = 0; i < hits.Length; i++) {
            if (hits[i] == null)
                continue;

            if(hits[i].tag == "Fighter" && hits[i].name == "Player")
                collidingWithPlayer = true;

            // We also need to clear the array at the end
            hits[i] = null;
        }
    }

    protected override void Death() {
        Destroy(gameObject);
        GameManager.instance.ShowText("DEAD", 30, Color.magenta, transform.position, Vector3.zero, 0.7f);
        GameManager.instance.ShowText("+" + xpValue + " xp!", 25, Color.magenta, transform.position + new Vector3(0.0f, 0.06f, 0.0f), Vector3.up * 25, 1.0f);
        GameManager.instance.GrantXp(xpValue);
    }
}
